using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyramidControl : MonoBehaviour
{
    public static int slotsOccupied;
    [SerializeField]
    private Transform[] rings;

    [SerializeField]
    private GameObject winSign;

    [SerializeField]
    private GameObject wrongSign;

    [SerializeField]
    private GameObject doorOpenerStand;

    // Start is called before the first frame update
    void Start()
    {
        Drag.PuzzleDone += CheckResults;
        slotsOccupied = 0;
        winSign.SetActive(false);
        wrongSign.SetActive(false);
        doorOpenerStand.SetActive(false);
    }

    public void CheckResults()
    {
        if ( 2.18f < rings[0].position.y && 2.21f > rings[0].position.y &&
             2.21f < rings[1].position.y && 2.31f > rings[1].position.y &&
             2.39f < rings[2].position.y && 2.44f > rings[2].position.y &&
             2.45f < rings[3].position.y && 2.49f > rings[3].position.y
            )
        {
            winSign.SetActive(true);
            doorOpenerStand.SetActive(true);
            wrongSign.SetActive(false);


 
        }
        else
        {
            wrongSign.SetActive(true);
            winSign.SetActive(false);
            doorOpenerStand.SetActive(false);
            Invoke("ReloadGame", 1f);
        }
    }
    private void ReloadGame()
    {
        Drag.PuzzleDone -= CheckResults;
        // Need to place rings in their original position 

    }

    // Update is called once per frame
    void Update()
    {
        CheckResults();
    }
}
