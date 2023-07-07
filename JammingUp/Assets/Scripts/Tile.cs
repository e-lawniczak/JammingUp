using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum TileType{
    RED,
    YELLOW,
    BLUE,
    GREEN,
    WHITE
} 

public class Tile
{
    private int x;
    private int y;
    private float cellSize;
    private TileType type;
    private SpriteRenderer sprite;
    private Color color;

    public Tile(int x, int y, float cellSize){
        this.x = x;
        this.y = y;
        this.cellSize = cellSize;
        sprite = new SpriteRenderer();
        sprite.color = Color.blue;
    }
}
