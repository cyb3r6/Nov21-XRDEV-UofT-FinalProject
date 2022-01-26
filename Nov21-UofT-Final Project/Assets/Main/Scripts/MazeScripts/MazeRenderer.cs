using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRenderer : MonoBehaviour
{
    [SerializeField]
    [Range(1, 50)]
    public int width = 10;

    [SerializeField]
    [Range(1, 50)]
    public int height = 10;

    [SerializeField]
    private float size = 1f;

    [SerializeField]
    private Transform wallPrefab;

    [SerializeField]
    private Transform floorPrefab;

    
    void Start()
    {
        var maze = MazeGenerator.Generate(width, height);
        DrawMaze(maze);
    }

    private void DrawMaze(WallState[,] maze)
    {
        var floor = Instantiate(floorPrefab, transform);
        floor.localScale = new Vector3(width, 1, height);

        for (int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                var cell = maze[i, j];
                var position = new Vector3(-width / 2 + i, 0, -height / 2 + j);
                
                if (cell.HasFlag(WallState.Up))
                {
                    var topwall = Instantiate(wallPrefab, transform) as Transform;
                    topwall.position = position + new Vector3(0, 0, size / 2);
                    topwall.localScale = new Vector3(size, topwall.localScale.y, topwall.localScale.z);
                }
                if (cell.HasFlag(WallState.Left))
                {
                    var leftwall = Instantiate(wallPrefab, transform) as Transform;
                    leftwall.position = position + new Vector3(-size / 2, 0, 0);
                    leftwall.localScale = new Vector3(size, leftwall.localScale.y, leftwall.localScale.z);
                    leftwall.eulerAngles = new Vector3(0, 90, 0);
                }

                if(i == width - 1)
                {
                    if (cell.HasFlag(WallState.Right))
                    {
                        var rightwall = Instantiate(wallPrefab, transform) as Transform;
                        rightwall.position = position + new Vector3(+size / 2, 0, 0);
                        rightwall.localScale = new Vector3(size, rightwall.localScale.y, rightwall.localScale.z);
                        rightwall.eulerAngles = new Vector3(0, 90, 0);
                    }
                }

                if(j == 0)
                {
                    if (cell.HasFlag(WallState.Down))
                    {
                        var bottomwall = Instantiate(wallPrefab, transform) as Transform;
                        bottomwall.position = position + new Vector3(0, 0, -size / 2);
                        bottomwall.localScale = new Vector3(size, bottomwall.localScale.y, bottomwall.localScale.z);
                    }
                }
            }
        }

    }
}
