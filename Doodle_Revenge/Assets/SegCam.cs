using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegCam : MonoBehaviour
{
    public GameObject seguir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float PosX = seguir.transform.position.x;
        float PosY = seguir.transform.position.y;
        transform.position = new Vector3(PosX, PosY, transform.position.z);
    }
}
