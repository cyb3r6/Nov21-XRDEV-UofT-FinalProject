using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTile : MonoBehaviour
{
    [SerializeField]
    private Transform arrow = default;

    private GameTile north, south, east, west;


    public static void MakeEastWestNeighbors(GameTile east, GameTile west)
    {
        Debug.Assert(west.east == null && east.west == null, "Redefine neighbors");

        west.east = east;
        east.west = west;
    }

    public static void MakeNorthSouthNeighbors(GameTile nore, GameTile south)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
