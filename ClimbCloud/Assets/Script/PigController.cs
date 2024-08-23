using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PigController : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb2D;
    public GameObject KingGo;
    public int lookAt;
    public bool hit;
    public bool isHit;
    public float hitTime;
    public int hp;
    public int maxHp;
    public int kingAttackPower;
    public bool isDead;
    public float deadTime;
    public bool die;

    void Start()
    {
        hp = 30;
        maxHp = hp;

        Debug.Log($"몬스터의 체력 : {hp}/{maxHp}");
    }


    void Update()
    {
        Vector2 kingPosition = KingGo.transform.position;
        Vector2 pigPosition = this.transform.position;
        float distance = Mathf.Abs((kingPosition - pigPosition).magnitude);
        hit = KingGo.GetComponent<KingController>().attack == true && distance < 3f;
        kingAttackPower = KingGo.GetComponent<KingController>().attackPower;

        if (!isDead)
        {
            if (!isHit)
            {
                animator.SetInteger("State", 0);
            }

            if (kingPosition.x > pigPosition.x)
            {
                lookAt = -1;
            }
            else
            {
                lookAt = 1;
            }

            this.transform.localScale = new Vector3(lookAt, 1, 1);

            if (hit && !isHit)
            {


                isHit = true;
            }



            if (isHit)
            {
                animator.SetInteger("State", 1);
                //넉백 구현
                this.rb2D.AddForce(new Vector2(lookAt * 5, 10));

                hitTime += Time.deltaTime;
                if (hitTime > 0.2f)
                {
                    isHit = false;
                    hitTime = 0;
                }
            }

            if (hit && hp <= kingAttackPower)
            {
                animator.SetInteger("State", 2);

                hp = 0;
                Debug.Log($"몬스터가 사망하였습니다.");
                GetComponent<CapsuleCollider2D>().excludeLayers = LayerMask.GetMask("King");
                die = true;
                
            }

            if (die)
            {
                deadTime += Time.deltaTime;
                if (deadTime > 0.6f)
                {
                    //Destroy(this.gameObject);
                    isDead = true;
                    return;
                }
            }

            if (hit && hp > kingAttackPower)
            {
                hp -= kingAttackPower;
                Debug.Log($"몬스터가 피해를 받았습니다. 남은 체력 : {hp}/{maxHp}");
            }
        }
    }
}
    