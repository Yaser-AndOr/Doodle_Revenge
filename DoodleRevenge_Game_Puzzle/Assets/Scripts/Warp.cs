using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{

    public GameObject target;
    public GameObject DisableCamera;
    public GameObject EnableCamera;

    GameObject[] objByTags;
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

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player")
        {
            other.transform.position = target.transform.GetChild(0).transform.position; 
            DisableCamera.SetActive( false );
            EnableCamera.SetActive( true ); 

            objByTags = FindInActiveObjectsByTag("Respawn");
           
            
            foreach (GameObject respawn in objByTags)
            {
                print("hola");
                respawn.SetActive(true);
                
                respawn.GetComponent<Destroyable>().resset = true;
                
            }
        }      

        
    }
}
