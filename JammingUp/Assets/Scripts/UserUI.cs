using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserUI : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject prefab;
    private float cellSpacing = 1.5f;
    PlayerState playerState;
    private GameObject[] stateTiles = new GameObject[4];
    private float baseStateOrderDisplayY = 6.5f;
    private GameObject currentState;

    private void Awake()
    {
        playerState = player.GetComponent<PlayerState>();
        prefab.name = "state order cell";
    }
    void Start()
    {
        for (int i = 0; i < stateTiles.Length; i++)
        {
            stateTiles[i] = Instantiate(
                    prefab,
                    new Vector3(
                        -20f + (cellSpacing * i),
                        baseStateOrderDisplayY,
                        0
                    ),
                    Quaternion.identity
                );
            stateTiles[i].transform.GetChild(0).GetComponent<SpriteRenderer>().color = ColorHandler.COLORS[playerState.GetStateOrder()[i]];
        }
        currentState = stateTiles[0];
    }

    void Update()
    {
        for (int i = 0; i < stateTiles.Length; i++)
        {
            if(i != playerState.GetCurrentStateInt())
                stateTiles[i].transform.position = new Vector3(-20f + (cellSpacing * i), baseStateOrderDisplayY,0);
            else
                stateTiles[i].transform.position = new Vector3(-20f + (cellSpacing * i), baseStateOrderDisplayY + 0.5f, 0);

        }
    }
}
