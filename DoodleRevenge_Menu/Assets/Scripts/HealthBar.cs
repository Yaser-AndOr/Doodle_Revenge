using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{    
    public Text NumberHealth;

    public float hp;
    public float maxHp = 100;
    string hpN;


    private void Start()
    {
        hp = maxHp;
        
    }

    // Update is called once per frame
    void Update()
    {
        hpN = hp.ToString() + "/" + maxHp.ToString();
        hp = Mathf.Clamp(hp, 0, maxHp);
        NumberHealth.text = hpN;  
        gameObject.GetComponent<Image>().fillAmount = hp / maxHp;
        

    }

    public float take_Damage(float damage)
    {
        hp = hp - damage;

        return hp;
    }

   
}
