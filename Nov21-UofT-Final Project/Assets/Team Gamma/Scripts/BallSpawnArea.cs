using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawnArea : MonoBehaviour
{

    public GrabbableObject ballPrefab;
    public BoxCollider spawnArea;

    // Start is called before the first frame update
    void Start()
    {
        SpawnBall();
    }

    public void OnTargetHit()
    {
        // spawn another ball
        SpawnBall();
    }

    
    public void OnCollisionEnter()
    {
        // spawn another ball
        SpawnBall();
    }
    

    public void SpawnBall()
    {
        Instantiate(ballPrefab, GetRandomPosition(), ballPrefab.transform.rotation);
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
        float y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
        float z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

        return new Vector3(x, y, z);
    }

}
