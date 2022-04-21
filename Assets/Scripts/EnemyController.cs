using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    public GameObject player;
    public float speed = 5;
    Animator anim;
    private Moviment movimentEnemy;

    // Start is called before the first frame update
    void Start()
    {
       
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        int zombieTypeGenerator = Random.Range(1, 28); // Random int between 1 and 27
        transform.GetChild(zombieTypeGenerator).gameObject.SetActive(true); // Enter on zombie, get child, return to gameobject and active
        movimentEnemy = GetComponent<Moviment>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 direction = player.transform.position - transform.position;

        movimentEnemy.RotationCaracter(direction);

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if(distance > 2.5)
        {

            movimentEnemy.MovimentCaracter(direction, speed);

         

            anim.SetBool("isAttacking", false);
        }
        else
        {
            anim.SetBool("isAttacking", true);
        }

    }

    void HitingPlayer()
    {
        int damageRandom = Random.Range(20, 30);
        player.GetComponent<PlayerController>().TakeDamage(damageRandom);
    }
}
