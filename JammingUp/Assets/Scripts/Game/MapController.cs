using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public int width = 16;
    public int height = 16;
    public float cellSpacing = 1f;
    private Grid grid;
    public GameObject prefab;
    public Tile[,] cells;

    // moving tiles
    private float timer = 0f;
    private float moveTick = 2f;


    // Start is called before the first frame update
    void Start()
    {
        //set Grid game object postion 
        transform.position = new Vector3(-width / 2 * cellSpacing, height / 2 * cellSpacing, 0);


        // create array of objects
        cells = new Tile[width, height];
        prefab.name = "cell";

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                // instantiate cell prefab 


                cells[i, j] = new Tile(j, i, Instantiate(
                    prefab,
                    new Vector3(
                        transform.position.x + cellSpacing * i,
                        transform.position.y - 1 - cellSpacing * j,
                        0
                    ),
                    Quaternion.identity
                ));

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= moveTick)
        {
            moveGrid();
            timer = 0f;
        }
    }

    // TODO: swapping rows
    private void moveGrid()
    {
        for (int i = 0; i < cells.GetLength(0); i++)
        {

        }
    }
    private void moveGrid_swap(int rowIndexA, int rowIndexB)
    {
        //int n = cells.GetLength(1);
        //Tile[] tmpArray = new Tile[cells.GetLength(1)];
        //for (int i = 0; i < n; i++)
        //    tmpArray[i] = cells[rowIndexA, i];

        //for (int j = 0;j < n; j++)
        //{
        //    cells[rowIndexB, j] = tmpArray[j];
        //}
    }
}
