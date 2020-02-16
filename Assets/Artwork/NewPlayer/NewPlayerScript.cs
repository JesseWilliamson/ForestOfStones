using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerScript : MonoBehaviour
{
    public float speed;
    float input;

    Rigidbody2D rb;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        anim = GetComponent<Animator>();

    }

    void Update()
    {


        if (input != 0){
            anim.SetBool("IsRunning", true);
        } else {
            anim.SetBool("IsRunning", false);
        }


        print (input);
    }


    
    void FixedUpdate()
    {
        input = Input.GetAxisRaw("Horizontal");


        rb.velocity = new Vector2(input * speed, rb.velocity.y);
    }
}
