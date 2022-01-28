using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Drag : MonoBehaviour
{
    public static event Action PuzzleDone = delegate { };

    [SerializeField]
    private Transform standPosition;

    private Vector3 initialPosition;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PuzzleDone();
    }
}
