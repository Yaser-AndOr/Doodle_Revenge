using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Cinemachine;

public class Warp : MonoBehaviour
{

    public GameObject target;
    public GameObject DisableCamera;
    
    public GameObject Area;
    public Collider2D cameraCollider;
    public string Area_Name;

    GameObject[] objByTags;
    
    bool start = false;

    bool isFadeIn = false;

    float alpha = 0;

    float fadeTime = 1f;

    void Awake()
    {     
       Area = GameObject.FindGameObjectWithTag("Area");
    }
    void Start()
    {
        
    }
    void Update()
    {

    }

    GameObject[] FindInActiveObjectsByTag(string tag)
    {
        List<GameObject> validTransforms = new List<GameObject>();
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].gameObject.CompareTag(tag))
                {
                    validTransforms.Add(objs[i].gameObject);
                }
            }
        }
        return validTransforms.ToArray();
    }

    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Animator>().enabled = false;
            other.GetComponent<Player>().enabled = false;
            FadeIn();

            yield return new WaitForSeconds(fadeTime);
        
       
       
        
       
            DisableCamera.GetComponent<CinemachineConfiner>().m_BoundingShape2D = null;
            DisableCamera.GetComponent<CinemachineConfiner>().m_BoundingShape2D = cameraCollider;
            other.transform.position = target.transform.GetChild(0).transform.position; 
            
            

            objByTags = FindInActiveObjectsByTag("Respawn");
           
            
            foreach (GameObject respawn in objByTags)
            {
                print("hola");
                respawn.SetActive(true);
                
                respawn.GetComponent<Destroyable>().resset = true;
                
            }

            FadeOut();

            other.GetComponent<Animator>().enabled = true;
            other.GetComponent<Player>().enabled = true;
            StartCoroutine(Area.GetComponent<Area>().ShowArea(Area_Name));

        }

                 
    }

    void OnGUI ()
    {
        if (!start){
            return;
        }

        GUI.color = new Color(GUI.color.r,GUI.color.g,GUI.color.b, alpha);

        Texture2D tex;
        tex = new Texture2D(1,1);
        tex.SetPixel(0 , 0,Color.black);
        tex.Apply();

        GUI.DrawTexture(new Rect(0,0 , Screen.width,Screen.height), tex);

        if (isFadeIn)
        {
            alpha = Mathf.Lerp(alpha,1.1f, fadeTime * Time.deltaTime);
        }else
        {
            alpha = Mathf.Lerp(alpha,-0.1f, fadeTime * Time.deltaTime);
            if (alpha < 0) start = false;
            
        }
    }

    void FadeIn()
    {
        start = true;
        isFadeIn  = true;
    }

    void FadeOut()
    {
        
        isFadeIn  = false;
    }
}
