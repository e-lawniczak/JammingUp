using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Tile
{
    private int x;
    private int y;
    private float cellSize;
    private ColorType type;
    private SpriteRenderer sprite;
    private Color color;

    public Tile(int x, int y, GameObject go){
        this.x = x;
        this.y = y;
        this.type = (ColorType)ColorHandler.ColorTypeArray.GetValue(UnityEngine.Random.Range(0, ColorHandler.ColorTypeArray.Length));
        
    }
}
