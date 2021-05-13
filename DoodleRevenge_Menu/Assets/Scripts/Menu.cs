using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    bool transicion = false;
    public void nivel1()
    {
        if (transicion == false)
        {
            transicion = false;
            SceneManager.LoadScene(1);
        }
        else if (transicion == true)
        {
            transicion = true;
        }
    }
    public void nivel2()
    {
        if (transicion == false)
        {
            transicion = false;
            SceneManager.LoadScene(2);
        }
        else if (transicion == true)
        {
            transicion = true;
        }
    }
    //public void nivel3()
    //{
        //if (transicion == false)
        //{
            //transicion = false;
            //SceneManager.LoadScene(3);
        //}
        //else if (transicion == true)
       // {
          //  transicion = true;
       // }/
    //}
}
