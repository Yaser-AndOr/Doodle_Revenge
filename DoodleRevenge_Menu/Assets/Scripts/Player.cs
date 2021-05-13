using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 4f;
    public Animator game_over;
    Animator anim;
    Rigidbody2D rb2d;
    Vector2 m;

    public GameObject Area;
    public Image hp;

    CircleCollider2D attackColl;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

        Area = GameObject.FindGameObjectWithTag("Area");

        attackColl = transform.GetChild(0).GetComponent<CircleCollider2D>();

        attackColl.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        Animations();

        Attack();
    }
        
    void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + m * speed * Time.deltaTime);

    }

    void Movement()
    {
        m = new Vector2(
                    Input.GetAxisRaw("Horizontal"),
                    Input.GetAxisRaw("Vertical")    
                );
    }

    void Animations()
    {
        if(m != Vector2.zero)
        {
            anim.SetFloat("movX", m.x);
            anim.SetFloat("movY", m.y);
            anim.SetBool("Walk", true);
            
        }else
        {            
            anim.SetBool("Walk", false);
        }

       

        if (hp.GetComponent<HealthBar>().take_Damage(0) <= 0)
        {
            rb2d.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            anim.Play("Player_Dead");
            StartCoroutine(ShowGameOver());

        }
        else
        {
            rb2d.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
          
        }
    }

    public IEnumerator ShowGameOver()
    {

        game_over.Play("Show");
        Area.transform.GetChild(0).GetComponent<Text>().text = "Game Over";        
        Area.transform.GetChild(1).GetComponent<Text>().text = "Game Over";
        game_over.Play("fadeOut");
        yield return new WaitForSeconds(5f);
        
        SceneManager.LoadScene(0);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // Si chocamos contra el jugador o un ataque la borramos
        if (col.transform.tag == "EnemyD")
        {

            hp.GetComponent<HealthBar>().take_Damage(4f);            


        }

        if (col.transform.tag == "Puz")
        {

            SceneManager.LoadScene(3);


        }
    }
    void Attack()
    {
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
}
