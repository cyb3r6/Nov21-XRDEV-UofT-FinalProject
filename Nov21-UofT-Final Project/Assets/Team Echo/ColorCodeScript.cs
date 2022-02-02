using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCodeScript : MonoBehaviour
{
    private int Code;
    public GameObject Door;
    void Start()
    {
        Code = 0; 
    }

    public void Blue()
    {
        if(Code == 0)
        {
            Code++;
            Debug.Log("Blue Pushed");
        }

        else if(Code != 0)
        {
            Code = 0;
        }
    }

    public void Purple()
    {
        if (Code == 1)
        {
            Code++;
            Debug.Log("Purple Pushed");
        }

        else if (Code != 1)
        {
            Code = 0;
        }
    }

    public void Orange()
    {
        if (Code == 2)
        {
            Code++;
            Debug.Log("Orange Pushed");
        }

        else if (Code != 2)
        {
            Code = 0;
        }
    }

    public void Yellow()
    {
        if (Code == 3)
        {
            Code++;
            Debug.Log("Yellow Pushed");
        }

        else if (Code != 3)
        {
            Code = 0;
        }
    }

    public void Update()
    {
        if (Code == 4)
        {
            Door.SetActive(false);
        }
    }

}
