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
    private Dictionary<TileType, Color> colors = new Dictionary<TileType, Color>();
    private int currentState = 0;
    public TileType currentType = 0;

    // Start is called before the first frame update
    void Start()
    {
        stateOrder = stateOrder.OrderBy(e => Random.Range(0f, 100f)).ToArray();
        colors.Add(TileType.RED, Color.red);
        colors.Add(TileType.BLUE, Color.blue);
        colors.Add(TileType.YELLOW, Color.yellow);
        colors.Add(TileType.GREEN, Color.green);
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = colors[
            stateOrder[currentState]
        ];
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentState = (currentState + 1) % stateOrder.Length;
            currentType = stateOrder[currentState];
        }
    }
}
