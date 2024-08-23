using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KingController : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb2D;
    public GameObject PigGo;
    public float moveSpeed;
    public float jumpForce;
    public int jumpCount;
    public float attackTime;
    public bool isAttack;
    public bool hit;
    public bool isHit;
    public bool isNB;   //넉백
    public float hitTime;
    public int lookAt;
    public int attackPower;
    public bool attack;

    void Start()
    {
        rb2D = this.gameObject.GetComponent<Rigidbody2D>();
        moveSpeed = 3f;
        jumpForce = 250f;
        jumpCount = 0;
        hitTime = 0;
        attackPower = 10;

        Debug.Log($"왕의 공격력 : {attackPower}");
    }

    public void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        attack = Input.GetKeyDown(KeyCode.KeypadEnter);
        bool jump = Input.GetKeyDown(KeyCode.Space);

        Vector2 kingPosition = this.transform.position;
        Vector2 pigPosition = PigGo.transform.position;
        float distance = Mathf.Abs((kingPosition - pigPosition).magnitude);
        hit = distance < 1.2f;


        //무조건 -1, 0, 1
        //0이 아니면? 이동 하고 있슴

        if (this.transform.position.y < -3.9 && !attack && !isHit)
        {
            jumpCount = 0;
            if (h == 0)
            {
                //멈춰 있는거임
                animator.SetInteger("State", 0);
            }
            else if (h == 1 || h == -1)
            {
                this.transform.localScale = new Vector3(h, 1, 1);
                //이동중임
                animator.SetInteger("State", 1);

                Vector2 velocity = new Vector2(h * moveSpeed, 0);
                rb2D.velocity = velocity;
            }
        }


        if (jump && jumpCount < 1 && !isHit)
        {
            //2단 점프 구현
            this.rb2D.AddForce(new Vector2(0, 1 * jumpForce));
            if (this.transform.position.y >= -3.9)
            {
                jumpCount++;
            }

        }

        if (attack && !isHit)
        {
            isAttack = true;
        }

        if (isAttack)
        {
            if (this.transform.position.y <= -3.9)
            {
                this.rb2D.velocity = new Vector2(0, 0);
            }
            animator.SetInteger("State", 2);
            attackTime += Time.deltaTime;
            if (attackTime > 0.429f)
            {
                Debug.Log("Attack finished");
                isAttack = false;
                attackTime = 0;
            }
        }

        if (kingPosition.x > pigPosition.x)
        {
            lookAt = 1;
        }
        else
        {
            lookAt = -1;
        }

        if (hit && !isHit)
        {
            isHit = true;
            isNB = true;
        }

        if (isNB)   //넉백
        {
            this.rb2D.velocity = new Vector2(0, 0);
            animator.SetInteger("State", 3);
            //넉백 구현
            this.rb2D.AddForce(new Vector2(lookAt * 200, 100));
            isNB = false;
        }

        if (isHit)  //공격당할 경우 1초동안 행동 불가
        {          
            hitTime += Time.deltaTime;
            if (hitTime > 1f)
            {
                isHit = false;
                hitTime = 0;
            }
        }

    }
}
