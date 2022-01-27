using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitSpawner : MonoBehaviour
{
    public GameObject correctPrefab;
    public GameObject incorrectPrefab;
    //[SerializeField]
    //public GameManager gameManager;
     
    public void OnCircuit(CircuitContent.Circuit circuit)
    {
        correctPrefab = GameObject.Find("AlbertPrefab");
        incorrectPrefab = GameObject.Find("LooseCube");

        if (circuit != null)
        {
            Debug.Log("albert");
            // gameManager.stoptimer = true;
            Instantiate(correctPrefab, new Vector3(0, 5, -80), Quaternion.identity);
        }
        else
        {
            Instantiate(incorrectPrefab, new Vector3(0, 5, -80), Quaternion.identity);
        }
    }
}
