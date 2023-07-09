using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Runtime;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class PlayerState : MonoBehaviour
{
    private ColorType[] stateOrder =
    {
        ColorType.RED,
        ColorType.YELLOW,
        ColorType.BLUE,
        ColorType.GREEN
    };
    private int currentState = 0;
    [SerializeField] ColorType currentType;
    public ColorType prevType;
    public bool hasChanged { get; set; } = false;
    public int comboCount { get; set; } = 0;
    public int maxCombo { get; set; } = 0;
    public int score { get; set; } = 0;

    // Start is called before the first frame update
    void Start()
    {
        stateOrder = stateOrder.OrderBy(e => Random.Range(0f, 100f)).ToArray();
        currentType = stateOrder[currentState];
    }

    // Update is called once per frame
    void Update()
    {
        // color the player 
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = ColorHandler.COLORS[stateOrder[currentState]];

        // listen for color change input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentState = (currentState + 1) % stateOrder.Length;
            currentType = stateOrder[currentState];
            if (prevType != currentType)
            {
                hasChanged = true;
            }
            else
            {
                hasChanged = false;
            }
        }
       
    }

    internal void CalculateScore()
    {
        score++;
        if (hasChanged) // add bonus points for using different state
        {
            comboCount++;
            score += comboCount;
            if (comboCount > maxCombo)
            {
                maxCombo = comboCount;
            }
        }
        else
        {
            comboCount = 0;
        }
    }

    public ColorType GetCurrentState()
    {
        return currentType;
    }
    public int GetCurrentStateInt()
    {
        return currentState;
    }
    public ColorType[] GetStateOrder()
    {
        return stateOrder;
    }
}
