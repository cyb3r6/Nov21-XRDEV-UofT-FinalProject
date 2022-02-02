using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGame : MonoBehaviour
{
    [SerializeField]
    private MazeRenderer mazeRenderer;

    public GameObject finishSphere;


    void Start()
    {
        var finish = Instantiate(finishSphere, PickRandomSpotOnBoard(), finishSphere.transform.rotation);
    }

    
    void Update()
    {
        
    }

    public Vector3 PickRandomSpotOnBoard()
    {
        return new Vector3(Random.Range(-mazeRenderer.width*0.5f, mazeRenderer.height*0.5f), 3f, Random.Range(-mazeRenderer.height * 0.5f, mazeRenderer.height * 0.5f));
    }
}
