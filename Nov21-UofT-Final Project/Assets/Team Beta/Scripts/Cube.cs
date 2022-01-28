using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    // lit color of the cube
    [SerializeField] private Color litColor;
    // default, unlit color of the cube
    [SerializeField] private Color defaultColor;
    // cube's renderer
    [SerializeField] private Renderer renderer;

    // called on collision enter
    public delegate void OnCollision(GameObject cube);
    public event OnCollision OnCubeCollision;

    public void OnCollisionEnter(Collision collision)
    {
        if (OnCubeCollision != null)
        {
            OnCubeCollision(gameObject);
        }
    }

    public void LightUp()
    {
        StartCoroutine(FlashCube());
    }

    // Light up the cube in it's pre-defined color
    // Change the color back to the default after a brief pause
    private IEnumerator FlashCube()
    {
        renderer.material.color = litColor;

        yield return new WaitForSeconds(1.5f);

        renderer.material.color = defaultColor;
    }
}
