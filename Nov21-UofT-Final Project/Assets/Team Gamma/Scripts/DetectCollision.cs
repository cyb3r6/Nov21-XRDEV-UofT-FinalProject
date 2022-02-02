using UnityEngine;
using System.Collections;
 
public class DetectCollision : MonoBehaviour
{

    public GrabbableObject ballPrefab;
    public BoxCollider spawnArea;
  
    public void OnCollisionEnter (Collision collision) 
    {
         Debug.Log ("Collision Detected");
 
         if (collision.gameObject.tag == "Shootable")
         {
             Instantiate(ballPrefab, GetRandomPosition(), ballPrefab.transform.rotation);
             Destroy(collision.gameObject);
         }
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
        float y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
        float z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

        return new Vector3(x, y, z);
    }

}