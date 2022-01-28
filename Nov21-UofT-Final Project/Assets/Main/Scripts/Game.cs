using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private Vector2Int boardSize = new Vector2Int(11,11);

    [SerializeField]
    private GameBoard board = default;



    // Start is called before the first frame update
    void Start()
    {
        board.Initialized(boardSize);
    }

    private void OnValidate()
    {
        if(boardSize.x < 2)
        {
            boardSize.x = 2;
        }
        if(boardSize.y < 2)
        {
            boardSize.y = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
