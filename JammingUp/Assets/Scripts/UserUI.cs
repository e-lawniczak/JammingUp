using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserUI : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject prefab;
    private float cellSpacing = 1.5f;
    PlayerState playerState;
    private Tile[] stateTiles = new Tile[4];

    private void Awake()
    {
        playerState = player.GetComponent<PlayerState>();
        transform.position = new Vector3(-350f, 300f, 0);
    }
    void Start()
    {
        for (int i = 0; i < stateTiles.Length; i++)
        {
            stateTiles[i] = new Tile(250, i, Instantiate(
                    prefab,
                    new Vector3(
                        transform.position.x + cellSpacing * i,
                        250,
                        0
                    ),
                    Quaternion.identity
                ));
            stateTiles[i].UpdateColor(playerState.GetStateOrder()[i]);
        }
    }

    void Update()
    {
        
    }
}
