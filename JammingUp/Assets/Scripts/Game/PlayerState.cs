using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Runtime;

public class PlayerState : MonoBehaviour
{
    private TileType[] stateOrder =
    {
        TileType.RED,
        TileType.YELLOW,
        TileType.BLUE,
        TileType.GREEN
    };
    private int currentState = 0;
    public TileType currentType = 0;

    private void Awake()
    {
        ColorHandler.COLORS.Add(TileType.RED, Color.red);
        ColorHandler.COLORS.Add(TileType.BLUE, Color.blue);
        ColorHandler.COLORS.Add(TileType.YELLOW, Color.yellow);
        ColorHandler.COLORS.Add(TileType.GREEN, Color.green);
    }

    // Start is called before the first frame update
    void Start()
    {
        stateOrder = stateOrder.OrderBy(e => Random.Range(0f, 100f)).ToArray();
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = ColorHandler.COLORS[
            stateOrder[currentState]
        ];
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentState = (currentState + 1) % stateOrder.Length;
            currentType = stateOrder[currentState];
        }
    }
}
