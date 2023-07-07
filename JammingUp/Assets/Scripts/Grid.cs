using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private int w;
    private int h;
    private float cellSize;
    private Tile[,] gridArray;

    public Grid(int width, int height, float cellSize)
    {
        this.w = width;
        this.h = height;
        this.cellSize = cellSize;

        gridArray = new Tile[width, height];

        for (int i = 0; i < gridArray.GetLength(0); i++)
        {
            for (int j = 0; j < gridArray.GetLength(1); j++)
            {
                gridArray[i, j] = new Tile(0, 0, cellSize);
            }
        }
    }

    public void RenderGrid()
    {
        for (int i = 0; i < gridArray.GetLength(0); i++)
        {
            for (int j = 0; j < gridArray.GetLength(1); j++)
            {
                gridArray[i, j] = new Tile(i, j, cellSize);
            }
        }
    }
}
