using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Flags]
public enum WallState
{
                    // 0000 -> no walls
                    // 1111 -> left right up and down walls
    Left = 1,       // 0001
    Right = 2,      // 0010
    Up = 4,         // 0100
    Down = 8,       // 1000
    Visited = 128,  // 1000 0000
    End,
    Start
}

public struct Position
{
    public int X;
    public int Y;
}

public struct Neighbour
{
    public Position position;
    public WallState sharedWall;
}

public static class MazeGenerator
{
    private static WallState GetOpposteWall(WallState wall)
    {
        switch (wall)
        {
            case WallState.Right:
                return WallState.Left;
            case WallState.Left:
                return WallState.Right;
            case WallState.Up:
                return WallState.Down;
            case WallState.Down:
                return WallState.Up;
            default:
                return WallState.Left;
        }
    }

    public static WallState[,] Generate(int width, int height)
    {
        WallState[,] maze = new WallState[width, height];

        WallState initial = WallState.Right | WallState.Left | WallState.Up | WallState.Down;

        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                maze[i, j] = initial;       // 1111
            }
        }

        return ApplyRecursiveBacktracker(maze, width, height);
    }

    private static WallState[,] ApplyRecursiveBacktracker(WallState[,] maze, int width, int height)
    {
        var range = new System.Random();
        var positionStack = new Stack<Position>();
        var position = new Position { X = range.Next(0, width), Y = range.Next(0, height) };

        maze[position.X, position.Y] |= WallState.Visited;      // 1000 1111
        positionStack.Push(position);

        while(positionStack.Count > 0)
        {
            var current = positionStack.Pop();

            // get unvisited neighbour
            var neighbours = GetUnvisitedNeighbours(current, maze, width, height);

            if(neighbours.Count > 0)
            {
                positionStack.Push(current);

                var randomIndex = range.Next(0, neighbours.Count);
                var randomNeighbour = neighbours[randomIndex];

                var nPosition = randomNeighbour.position;
                maze[current.X, current.Y] &= ~randomNeighbour.sharedWall;
                maze[nPosition.X, nPosition.Y] &= ~GetOpposteWall(randomNeighbour.sharedWall);
                maze[nPosition.X, nPosition.Y] |= WallState.Visited;

                positionStack.Push(nPosition);

            }
        }

        return maze;
    }


    private static List<Neighbour> GetUnvisitedNeighbours(Position p, WallState[,] maze, int width, int height)
    {
        var list = new List<Neighbour>();

        // left
        if(p.X > 0)
        {
            if(!maze[p.X -1, p.Y].HasFlag(WallState.Visited))
            {
                list.Add(new Neighbour
                {
                    position = new Position
                    {
                        X = p.X - 1,
                        Y = p.Y
                    },
                    sharedWall = WallState.Left
                }) ;
            }
        }

        // right
        if(p.X < width - 1)
        {
            if (!maze[p.X + 1, p.Y].HasFlag(WallState.Visited))
            {
                list.Add(new Neighbour
                {
                    position = new Position
                    {
                        X = p.X + 1,
                        Y = p.Y
                    },
                    sharedWall = WallState.Right
                });
            }   
        }

        // up
        if (p.Y < height - 1)
        {
            if (!maze[p.X, p.Y +1].HasFlag(WallState.Visited))
            {
                list.Add(new Neighbour
                {
                    position = new Position
                    {
                        X = p.X,
                        Y = p.Y+1
                    },
                    sharedWall = WallState.Up
                });
            }
        }

        // down
        if (p.Y > 0 )
        {
            if (!maze[p.X, p.Y - 1].HasFlag(WallState.Visited))
            {
                list.Add(new Neighbour
                {
                    position = new Position
                    {
                        X = p.X,
                        Y = p.Y - 1
                    },
                    sharedWall = WallState.Down
                });
            }
        }

        return list;
    }

}
