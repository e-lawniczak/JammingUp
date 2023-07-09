using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public Transform movePoint;
    private bool axisXInUse = false;
    private bool axisYInUse = false;

    // handle grid interaction
    [SerializeField] int currentX;
    [SerializeField] int currentY;
    [SerializeField] GameObject map;
    MapController mapController;
    PlayerState playerState;
    [SerializeField] ColorType state;
    private Tile currentTile;
    private Tile prevTile;

    public bool hasPlayerStarted = false;
    private int resetMoveTickEveryXGold = 20;
    private int reshuffleEveryXGold = 10;





    private void Awake()
    {
        mapController = map.GetComponent<MapController>();
        playerState = gameObject.GetComponent<PlayerState>();
    }
    void Start()
    {
        movePoint.parent = null;
        currentX = 8;
        currentY = 15;
        prevTile = null;
        state = playerState.GetCurrentState();
    }

    // Update is called once per frame
    void Update()
    {
        state = playerState.GetCurrentState();
        movePlayer();
        calculateMovePoint();
        handleTiles();

    }
    private void handleTiles()
    {
        if (mapController.cells[currentX, currentY] == null) return;
        currentTile = mapController.cells[currentX, currentY];
         
          if (prevTile != null && currentTile != prevTile)
           {
              if (currentTile.GetColor() != ColorType.WHITE)
            {
                handleGold(currentTile);
                calculateScore();
                currentTile.UpdateColor(ColorType.WHITE);
                playerState.hasChanged = false;
                playerState.prevType = playerState.GetCurrentState();
            }
            // change player state
            prevTile = currentTile;
            FindObjectOfType<AudioManager>().Play("PlayerMove");
        }
    }

    private void handleGold(Tile currentTile){
        if(currentTile.getGold()){
            playerState.onGotGold();
            FindObjectOfType<AudioManager>().Play("GoldAcquired");
            checkGoldStatus();
        }
    }

    private void checkGoldStatus(){
      
        if(playerState.gold % resetMoveTickEveryXGold == 0 && playerState.gold % reshuffleEveryXGold != 0){
            mapController.resetMoveTick();
            FindObjectOfType<AudioManager>().Play("Slowdown");
        }else if (playerState.gold % reshuffleEveryXGold == 0){
            mapController.reshuffle();
            FindObjectOfType<AudioManager>().Play("Reshuffle");
        }
    }

    private void calculateScore()
    {
        playerState.CalculateScore();

    }
    public void forcePLayerMovement(float horizontal, float vertical)
    {
        // since horizontal is either + or - we can just add cuz array indexes are correct
        currentX += (int)horizontal; 


        // since array indees are reverted we can subtract both and it will be correctly moved
        currentY -= (int)vertical;


        movePoint.position += new Vector3(horizontal, vertical, 0);
        transform.position = movePoint.position;
        handleTiles();
    }
    private void movePlayer()
    {
        // more fluid movement
        //transform.position = Vector3.MoveTowards(
        //    transform.position,
        //    movePoint.position,
        //    moveSpeed * Time.deltaTime
        //);

        // grid-snapping feel movement
        transform.position = movePoint.position;
    }

    private void calculateMovePoint()
    {
        float inputH = UnityEngine.Input.GetAxisRaw("Horizontal");
        float inputV = UnityEngine.Input.GetAxisRaw("Vertical");

        if (Vector3.Distance(transform.position, movePoint.position) >= .05f)
            // if the player didnt' reach the point yet - prevent moving 
            return;


        if (Mathf.Abs(inputH) == 1f)
        {
            // if player is about to get out of the grid - prevent moving
            if (checkOutOfBounds(inputH, true))
                return;
            // if player is about to move to wrong color tile
            if (checkMoveState(inputH, true))
                return;
            // detect arrow left or arrow right
            if (!axisXInUse)
            {
                movePoint.position += new Vector3(inputH, 0f, 0f);
                // prevent hold down button movement
                axisXInUse = true;

                // modify currentX based on input
                currentX += (int)inputH;

                // assign new prev tile
                prevTile = currentTile;

                if (!hasPlayerStarted) hasPlayerStarted = true;
            }
        }
        else if (Mathf.Abs(inputV) == 1f)
        {
            // if player is about to get out of the grid - prevent moving
            if (checkOutOfBounds(inputV, false))
                return;
            // if player is about to move to wrong color tile
            if (checkMoveState(inputV, false))
                return;
            if (!axisYInUse)
            {
                // detect arrow up or arrow down
                movePoint.position += new Vector3(0f, inputV, 0f);
                // prevent hold down button movement
                axisYInUse = true;

                // modify currentY based on input "-" because of how arrays work
                currentY -= (int)inputV;

                // assign new prev tile
                prevTile = currentTile;

                if (!hasPlayerStarted) hasPlayerStarted = true;

            }
        }

        //unlock movement after ButtonUpEvent
        if (Mathf.Abs(inputH) <= 0.15f)
        {
            axisXInUse = false;

        }
        if (Mathf.Abs(inputV) <= 0.15f)
        {
            axisYInUse = false;

        }
    }

    /// <summary>
    /// Checks wether the current state is correct related to the next tile we want to go to.
    /// Returns true if next tile color doesn't match player state color
    /// Returns false if next tile matches the color
    /// </summary>
    /// <param name="input"></param>
    /// <param name="isHorizontal"></param>
    /// <returns></returns>
    private bool checkMoveState(float input, bool isHorizontal)
    {
        Tile nextTile;
        if (isHorizontal)
        {
            if (input < 0f)
            {
                // move left
                nextTile = mapController.cells[currentX - 1, currentY];
                return checkMoveState_checkState(nextTile);
            }
            else if (input > 0f)
            {
                //move right
                nextTile = mapController.cells[currentX + 1, currentY];
                return checkMoveState_checkState(nextTile);
            }
        }
        else
        {
            if (input < 0f)
            {
                //move down
                nextTile = mapController.cells[currentX, currentY + 1];
                return checkMoveState_checkState(nextTile);
            }
            else if (input > 0f)
            {
                //move up
                nextTile = mapController.cells[currentX, currentY - 1];
                return checkMoveState_checkState(nextTile);
            }
        }
        return false;
    }
    private bool checkMoveState_checkState(Tile nextTile)
    {
        if (nextTile.GetColor() == ColorType.WHITE)
            return false;
        if (nextTile.GetColor() != state)
            return true;
        return false;
    }
    /// <summary>
    /// Checks wether or not next move position is out of bounds for game grid. Returns true should next tile be out of bounds and fals if everything is fine
    /// </summary>
    /// <param name="input"></param>
    /// <param name="isHorizontal"></param>
    /// <returns></returns>
    private bool checkOutOfBounds(float input, bool isHorizontal)
    {
        if (isHorizontal)
        {
            if (input < 0f && currentX == 0)
                return true;
            else if (input > 0f && currentX == 15)
                return true;
        }
        else
        {
            if (input < 0f && currentY == 15)
                return true;
            else if (input > 0f && currentY == 0)
                return true;
        }
        return false;
    }



    public int getCurrentX()
    {
        return currentX;
    }
    public int getCurrentY()
    {
        return currentY;
    }

    internal void gameOver()
    {
        PlayerPrefs.SetInt("score", playerState.score);
        PlayerPrefs.SetInt("maxCombo", playerState.maxCombo);
        SceneManager.LoadScene("GameOver");
    }
}
