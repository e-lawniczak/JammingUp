using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Runtime;

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
        }
    }

    public ColorType GetCurrentState()
    {
        return currentType;
    }
}
