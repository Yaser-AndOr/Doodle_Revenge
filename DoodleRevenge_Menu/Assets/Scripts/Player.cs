using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 4f;

    Animator anim;
    Rigidbody2D rb2d;
    Vector2 m;

    CircleCollider2D attackColl;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

        attackColl = transform.GetChild(0).GetComponent<CircleCollider2D>();

        attackColl.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        m = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")    
        );

        if(m != Vector2.zero)
        {
            anim.SetFloat("movX", m.x);
            anim.SetFloat("movY", m.y);
            anim.SetBool("Walk", true);
        }else
        {            
            anim.SetBool("Walk", false);
        }

        if(m != Vector2.zero) attackColl.offset = new Vector2(m.x/40,m.y/40);

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        bool attack = stateInfo.IsName("Player_Attack");

        if(Input.GetKeyDown("space") && !attack)
        {
            
            anim.SetTrigger("Attack");
            
           
        }
        

        if (attack)
        {
            float playBacktime = stateInfo.normalizedTime;            
            if (playBacktime > 0.33 && playBacktime < 0.66)
            {
                
                rb2d.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                attackColl.enabled = true;
            }else
            {
                rb2d.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
                attackColl.enabled = false;
            }
            if (playBacktime > 1)
            {
                anim.Rebind();
            }
        }
    }

    
    void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + m * speed * Time.deltaTime);

    }

}
