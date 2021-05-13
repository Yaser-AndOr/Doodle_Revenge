using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Destroyable : MonoBehaviour
{
    public string destroyState;
    public string idleState;
    public float timeForDisable;
    public bool resset;
    

    Animator anim;
    public GameObject prefab;
    public GameObject HealthBar;
    
   

    private Sprite sprite;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        
    }



    IEnumerator OnTriggerEnter2D (Collider2D col)
    {
        if (col.tag == "Attack")
        {
            
            anim.Play(destroyState);
            yield return new WaitForSeconds(timeForDisable);
            foreach (Collider2D c in GetComponents<Collider2D>())
            {
                c.enabled = false;  
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName(destroyState) && stateInfo.normalizedTime >= 1)
        {     
            Destroy(gameObject);

            
               
            anim.Rebind();
            var clone = Instantiate(prefab);
            clone.name = prefab.name;
            
            clone.GetComponent<Collider2D>().enabled = true;
            clone.GetComponent<Animator>().enabled = true;
            clone.GetComponent<FixDepth>().enabled = true;
            clone.GetComponent<Destroyable>().enabled = true;
            clone.SetActive(false);
            anim.Play(idleState);

            if (resset == true)
            {
                resset = false;
                
            }
            
            print("Se creo uno nuevo " + stateInfo.normalizedTime);
            }
            
            
        }
    }

