using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
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
    public float defaultMoveTick = 3f;
    public float moveTick;
    private int counter = 0;
    private int counterTreshold = 5;
    private float moveTickDelta = -.1f;
    private float lowestMoveTick = 1f;

    // handling moving player
    [SerializeField] GameObject playerObj;
    PlayerController playerController;
    PlayerState playerState;

    private void Awake()
    {
        playerController = playerObj.GetComponent<PlayerController>();
        playerState = playerObj.GetComponent<PlayerState>();
    }


    // Start is called before the first frame update
    void Start()
    {
        //set Grid game object postion 
        transform.position = new Vector3(-width / 2 * cellSpacing, height / 2 * cellSpacing, 0);
        moveTick = defaultMoveTick;

        // create array of objects
        cells = new Tile[width, height];
        prefab.name = "cell";

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                // instantiate cell prefab 


                cells[i, j] = new Tile(i, j, Instantiate(
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
            if (!playerController.hasPlayerStarted)
            {
                timer = 0f;
                return;
            }
            moveGrid();
            if (playerController.getCurrentY() < 15)
            {
                playerController.forcePLayerMovement(0f, -1f);
            }
            else
            {
                playerController.gameOver();
            }
            timer = 0f;
            counter++;

        }
        if (counter >= counterTreshold && moveTick >= lowestMoveTick)
        {
            moveTick += moveTickDelta;
            counter = 0;
        }
    }

    public void resetMoveTick(){
        moveTick = defaultMoveTick;
    }

    public void reshuffle(){
        var playerColorState = playerState.GetCurrentState();

        ColorType nextColor = playerState.GetNextPlayerColor(playerColorState);

        foreach(List<Tile> row in getAllRowsAbovePlayer()){
            foreach(Tile tile in row){
                tile.UpdateColor(nextColor);
            }
            nextColor = playerState.GetNextPlayerColor(nextColor);
        }
    }

    public List<Tile> getAllTilesAbovePlayer(){
        List<Tile> listOfTiles = new List<Tile>();
        for(int y=playerController.getCurrentY(); y >= 0; y--){
            listOfTiles.Add(cells[playerController.getCurrentX(), y]);
        }
        return listOfTiles;
    }

    public List<List<Tile>> getAllRowsAbovePlayer(){
        List<List<Tile>> listOfRows = new List<List<Tile>>();
        int playersY = playerController.getCurrentY();
        for(int y=playersY; y >= 0; y--){
            listOfRows.Add(new List<Tile>());
            for(int x=0; x < cells.GetLength(0); x++){
                listOfRows[playersY - y].Add(cells[x, y]);
            }
        }
        return listOfRows;
    }

    // TODO: swapping rows
    private void moveGrid()
    {
        for (int i = cells.GetLength(0) - 1; i > 0; i--)
        {
            moveGrid_swap(i - 1, i);
         }
         for (int i = 0; i < cells.GetLength(1); i++)
         {
             Destroy(cells[i, 0].GetGameObject());
            cells[i, 0] = new Tile(i, 0, Instantiate(
                    prefab,
                    new Vector3(
                        transform.position.x + cellSpacing * i,
                        transform.position.y - 1 - cellSpacing * 0,
                        0
                    ),
                    Quaternion.identity
                ));
            cells[i, 0].UpdateColor(cells[i, 0].GetColor());
        }
    }
    private void moveGrid_swap(int rowIndexA, int rowIndexB)
    {
        int n = cells.GetLength(1);

        for (int j = 0; j < n; j++)
        {
            cells[j, rowIndexB].AssignNewTile(cells[j, rowIndexA]);
        }
    }
    public float GetMoveTick()
    {
        return moveTick;
    }
}
