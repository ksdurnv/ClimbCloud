using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Animator animator;

    private float moveForce = 1f;
    private float jumpForce = 300f;

    private void Start()
    {
        rb2D = this.gameObject.GetComponent<Rigidbody2D>();
        animator = this.gameObject.GetComponent<Animator>();

    }    

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
            this.rb2D.AddForce(new Vector2(-1,0) * moveForce);

            this.animator.SetInteger("State", 1);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.localScale = new Vector3(1, 1, 1);
            this.rb2D.AddForce(new Vector2(1, 0) * moveForce);

            this.animator.SetInteger("State", 1);
        }
        else
        {
            this.animator.SetInteger("State", 0);
        }



        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.rb2D.AddForce(new Vector2(0, 1) * jumpForce);
        }
    }
}
