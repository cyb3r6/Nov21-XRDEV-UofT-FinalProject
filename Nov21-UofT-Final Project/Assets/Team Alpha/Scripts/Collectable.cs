using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]

public class MemoryCore : MonoBehaviour
{
    BoxCollider boxCollider;
    MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            boxCollider.enabled = false;
            meshRenderer.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
