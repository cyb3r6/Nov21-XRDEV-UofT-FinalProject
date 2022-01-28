using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitSpawner : MonoBehaviour
{
    public GameObject correctPrefab;
    public GameObject correctPrefabPillar;
    public GameObject correctPrefabFireworks;
    public GameObject incorrectPrefab;
    //private GameManager gameManager;
      
    public void OnCircuit(CircuitContent.Circuit circuit)
    {
        correctPrefab = GameObject.Find("AlbertPrefab");
        correctPrefabPillar = GameObject.Find("PillarPrefab");
        incorrectPrefab = GameObject.Find("LooseCube");
        correctPrefabFireworks = GameObject.Find("Fireworks");

        if (circuit != null)
        {
            Debug.Log("albert ftw");
            //gameManager.stoptimer = true;
            Instantiate(correctPrefab, new Vector3(0, 10, -79), Quaternion.identity);
            Instantiate(correctPrefabPillar, new Vector3(0, 3.5f, -171), Quaternion.identity);
            Instantiate(correctPrefabFireworks, new Vector3(0, 10, -171), Quaternion.identity);
            Instantiate(correctPrefabFireworks, new Vector3(5, 10, -171), Quaternion.identity);
            Instantiate(correctPrefabFireworks, new Vector3(-10, 10, -171), Quaternion.identity);
        }
        else
        {
            Instantiate(incorrectPrefab, new Vector3(0, 5, -80), Quaternion.identity);
        }
    }
}
