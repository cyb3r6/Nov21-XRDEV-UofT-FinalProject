using UnityEngine;
using System.Collections;
 
public class StayOnPedestal : MonoBehaviour
{
    public bool Sphere1Correct = false;
  
    public void OnTriggerStay(Collider sphere) 
    {
         if (sphere.gameObject.tag == "BlueSphere")
         {
            Sphere1Correct = true;
            Debug.Log("Correct!");
            Debug.Log(Sphere1Correct);
         }
    }

}