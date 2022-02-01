using UnityEngine;
using System.Collections;
 
public class StayOnPedestal : MonoBehaviour
{
    /// 1: Blue
    //  2: Green 
    //  3: Yellow 
    //  4: Red

    public bool sphere1Correct = false;
    public bool sphere2Correct = false;
    public bool sphere3Correct = false;
    public bool sphere4Correct = false;
    public int correctSphereValue;
    private int insertedSphereValue;
        float speed = 0.1f;

    public BoxCollider dropZone;
  
    public void OnTriggerStay(Collider sphere) 
    {
        float step = speed * Time.deltaTime;
        Vector3 newSpherePosition = sphere.transform.position;

        newSpherePosition = getNewSpherePosition();
        sphere.transform.position = Vector3.MoveTowards(sphere.transform.position, newSpherePosition, step);

        if (sphere.gameObject.tag == "SphereBlue")
        {
            insertedSphereValue = 1;
        }
        else if (sphere.gameObject.tag == "SphereGreen")
        {
            insertedSphereValue = 2;
        }
        else if (sphere.gameObject.tag == "SphereYellow")
        {
            insertedSphereValue = 3;
        }
        else if (sphere.gameObject.tag == "SphereRed")
        {
            insertedSphereValue = 4;
        }

        if (correctSphereValue == insertedSphereValue)
        {
            sphere1Correct = true;
            Debug.Log("First correct sphere placed. Keep trying!");

            switch(insertedSphereValue)
            {
                case 1:
                    Debug.Log("Blue sphere correctly placed");
                    sphere1Correct = true;
                    break;
                case 2:
                    Debug.Log("Green sphere correctly placed");
                    sphere2Correct = true;
                    break;
                case 3:
                    Debug.Log("Yellow sphere correctly placed");
                    sphere3Correct = true;
                    break;
                case 4:
                    Debug.Log("Red sphere correctly placed");
                    sphere4Correct = true;
                    break;
                default: 
                    break;
            }
        }

        if (sphere1Correct && sphere2Correct && sphere3Correct && sphere4Correct)
        {
            Debug.Log("Congratulations!! You win!");
        }
    }

    public void OnTriggerEnter(Collider sphere) 
    {
        sphere.GetComponent<Rigidbody>().useGravity = false;
    }

    public void OnTriggerExit(Collider sphere) 
    {
        sphere.GetComponent<Rigidbody>().useGravity = true;
    }

    private Vector3 getNewSpherePosition()
    {
        float x = dropZone.bounds.min.x + ((dropZone.bounds.max.x - dropZone.bounds.min.x) / 2);
        float y = dropZone.bounds.min.y + ((dropZone.bounds.max.y - dropZone.bounds.min.y) / 2);
        float z = dropZone.bounds.min.z + ((dropZone.bounds.max.z - dropZone.bounds.min.z) / 2);

        return new Vector3(x, y, z);
    }
}