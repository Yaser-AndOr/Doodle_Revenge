using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public GameObject follow;
	public Vector2 minCamPos, maxCamPos;
	public float smoothTime;
    private Vector2 velocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 position = transform.position;
        //position.x = follow.transform.position.x;
        //position.y = follow.transform.position.y;
        //transform.position = position;
    }

    void FixedUpdate () {
		//float posX = Mathf.SmoothDamp(transform.position.x,
		//	follow.transform.position.x, ref velocity.x, smoothTime);
		//float posY = Mathf.SmoothDamp(transform.position.y,
		//	follow.transform.position.y, ref velocity.y, smoothTime);

		//transform.position = new Vector2(
		//	Mathf.Clamp(posX, minCamPos.x, maxCamPos.x), 
		//	Mathf.Clamp(posY, minCamPos.y, maxCamPos.y));
	}
}
