using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{

    public Target targetPrefab;
    public BoxCollider spawnArea;

    // Start is called before the first frame update
    void Start()
    {
        SpawnTarget();
    }

    public void OnTargetHit()
    {
        // spawn another Target
        SpawnTarget();
    }

    public void SpawnTarget()
    {
        var newTarget = Instantiate(targetPrefab, GetRandomPosition(), targetPrefab.transform.rotation);

        newTarget.game = this;

    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
        float y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
        float z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

        return new Vector3(x, y, z);
    }

}
