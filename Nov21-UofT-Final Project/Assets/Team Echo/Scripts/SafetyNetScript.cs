using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafetyNetScript : MonoBehaviour
{
    public Collider net;
    public Vector3 spawnPos;
    public Quaternion spawnRot;
    public GameObject spawnPoint;

    void Start()
    {
        net.isTrigger = true;
        spawnPos = spawnPoint.transform.position;
        spawnRot = spawnPoint.transform.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;

        if (obj.tag == "Player")
        {
            obj.transform.position = spawnPos;
            obj.transform.rotation = spawnRot;
        }
        else
        {
            Destroy(obj, 1);
        }
    }

}