using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixDepth : MonoBehaviour
{
    public bool fixFrame;
    SpriteRenderer spr;

    // Start is called before the first frame update
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        spr.sortingLayerName = "Player";
        spr.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);
    }

    // Update is called once per frame
    void Update()
    {
        if (fixFrame)
        {
            spr.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);
        }
    }
}
