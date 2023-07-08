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

    // Start is called before the first frame update
    void Start()
    {
        //set Grid game object postion 
        transform.position = new Vector3(-width / 2 * cellSpacing, height / 2 * cellSpacing, 0);


        // create array of objects
        cells = new Tile[width, height];


        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                // instantiate cell prefab 


                cells[i, j] = new Tile(j, i, Instantiate(
                    prefab,
                    new Vector3(
                        transform.position.x + cellSpacing * i,
                        transform.position.y -1 - cellSpacing * j,
                        0
                    ),
                    Quaternion.identity
                ));

            }
        }
    }

    // Update is called once per frame
    void Update() { }
}
