using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{

    public float speed = 10f;
    private Rigidbody rb;

    Vector3 direction;

    public LayerMask floorMask; // limit the ray just to hit the ground

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float Xaxis = Input.GetAxis("Horizontal");
        float Zaxis = Input.GetAxis("Vertical");

        direction = new Vector3(Xaxis, 0, Zaxis);

        

        //transform.Translate(direction * speed * Time.deltaTime);

        if (direction != Vector3.zero)
        {
            GetComponent<Animator>().SetBool("Running", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("Running", false);
        }

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + (direction * speed * Time.deltaTime)); // Move player from rigibody position

        Ray radius = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(radius.origin, radius.direction * 100, Color.red);

        RaycastHit impact; // check collision

        if(Physics.Raycast(radius, out impact, 100, floorMask))
        {
            Vector3 playerAimPosition = impact.point - transform.position;

            playerAimPosition.y = transform.position.y;

            Quaternion newRotation = Quaternion.LookRotation(playerAimPosition);

            rb.MoveRotation(newRotation);
        }
    }
}
