using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading;

public class Tile
{
    private int x;
    private int y;
    private float cellSize;
    private ColorType type;
    private Color color;
    private GameObject tileObject;

    public Tile(int x, int y, GameObject go){
        this.x = y;
        this.y = x;
        this.type = (ColorType)ColorHandler.ColorTypeArray.GetValue(UnityEngine.Random.Range(0, ColorHandler.ColorTypeArray.Length-1));
        tileObject = go;
        color = ColorHandler.COLORS[this.type];
        tileObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = ColorHandler.COLORS[this.type];
        if(this.x == 8 && this.y == 15)
            tileObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = ColorHandler.COLORS[ColorType.WHITE];

    }

    public ColorType GetColor()
    {
        return type;
    }
    public GameObject GetGameObject()
    {
        return tileObject;
    }
    public void UpdateColor(ColorType newType)
    {
        var newColor = ColorHandler.COLORS[newType];
        type = newType;
        color = newColor;
        tileObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = newColor;
    }
}
