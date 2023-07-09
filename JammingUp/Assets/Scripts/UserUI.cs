using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserUI : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject prefab;
    private float cellSpacing = 1.5f;
    PlayerState playerState;
    private GameObject[] stateTiles = new GameObject[4];
    private float baseStateOrderDisplayY = 5f;
    private float displayYOffet = 0.5f;
    private float baseStateOrderDisplayX = -22f;
    private GameObject currentState;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI goldText;

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
                        baseStateOrderDisplayX + (cellSpacing * i),
                        baseStateOrderDisplayY,
                        0
                    ),
                    Quaternion.identity
                );
            stateTiles[i].transform.GetChild(0).GetComponent<SpriteRenderer>().color = ColorHandler.COLORS[playerState.GetStateOrder()[i]];
        }
        currentState = stateTiles[0];
        scoreText = gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        goldText = gameObject.transform.GetChild(3).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        for (int i = 0; i < stateTiles.Length; i++)
        {
            if(i != playerState.GetCurrentStateInt())
                stateTiles[i].transform.position = new Vector3(baseStateOrderDisplayX + (cellSpacing * i), baseStateOrderDisplayY,0);
            else
                stateTiles[i].transform.position = new Vector3(baseStateOrderDisplayX + (cellSpacing * i), baseStateOrderDisplayY + displayYOffet, 0);
        }

        string scoreTemplate = playerState.comboCount > 1 ? "Points: {0}\nCombo: {1}" : "Points: {0}";
        scoreText.text = string.Format(scoreTemplate, playerState.score, playerState.comboCount);

        string goldTemplate = "Gold: {0}";
        goldText.text = string.Format(goldTemplate, playerState.gold);
    }
}
