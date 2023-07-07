using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrid : MonoBehaviour
{
    public int width = 16;
    public int height = 16;
    public float step = 1f;
    private Grid grid;
    public GameObject prefab;
    public GameObject[,] cells;

    // Start is called before the first frame update
    void Start()
    {
        //set Grid game object postion 
        transform.position = new Vector3(-width / 2 * step, -height / 2 * step, 0);
        
        // create array of objects
        cells = new GameObject[width, height];

        
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                // instantiate cell prefab 
                cells[i, j] = (GameObject)Instantiate(
                    prefab,
                    new Vector3(
                        transform.position.x + step * i,
                        transform.position.y + step * j,
                        0
                    ),
                    Quaternion.identity
                );
                // set initial color of cell
                cells[i,j].transform.GetChild(0).GetComponent<SpriteRenderer>().color = j%2 == 0 ? i%2 == 0 ? Color.white : Color.black : i%2 == 0 ? Color.black : Color.white;
            }
        }
    }

    // Update is called once per frame
    void Update() { }
}
