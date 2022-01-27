using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitSpawner : MonoBehaviour
{
    public GameObject correctPrefab;
    public GameObject incorrectPrefab;


    public void OnCircuit(CircuitContent.Circuit circuit)
    {
        correctPrefab = GameObject.Find("WinCube");
        incorrectPrefab = GameObject.Find("LooseCube");

        if (circuit != null)
        {
            Instantiate(correctPrefab);
        }
        else
        {
            Instantiate(incorrectPrefab);
        }
    }
}
