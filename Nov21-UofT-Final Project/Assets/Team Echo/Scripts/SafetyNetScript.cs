using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafetyNetScript : MonoBehaviour
{
    public Collider net;
    public Vector3 spawnPos;
    public Quaternion spawnRot;
    public GameObject spawnPoint;

    public GameObject rockSpawn;
    public Vector3 rockSpawnPos;
    public Quaternion rockSpawnRot;

    public GameObject rock2Spawn;
    public Vector3 rock2SpawnPos;
    public Quaternion rock2SpawnRot;

    void Start()
    {
        net.isTrigger = true;
        spawnPos = spawnPoint.transform.position;
        spawnRot = spawnPoint.transform.rotation;

        rockSpawnPos = rockSpawn.transform.position;
        rockSpawnRot = rockSpawn.transform.rotation;

        rock2SpawnPos = rock2Spawn.transform.position;
        rock2SpawnRot = rock2Spawn.transform.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;

        if (obj.tag == "Player")  
        {
            obj.transform.position = spawnPos;
            obj.transform.rotation = spawnRot;
        }
        else if (obj.tag == "Rock")
        {
            obj.transform.position = rockSpawnPos;
            obj.transform.rotation = rockSpawnRot;
        }
        else if (obj.tag == "Rock2")
        {
            obj.transform.position = rock2SpawnPos;
            obj.transform.rotation = rock2SpawnRot;
        }
        else
        {
            Destroy(obj, 1);
        }
    }

}