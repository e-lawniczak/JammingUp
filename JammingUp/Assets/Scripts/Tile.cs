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
        this.UpdateColor(getRandomColorType());
        tileObject = go;
        if(this.x == 8 && this.y == 15)
            this.UpdateColor(ColorType.WHITE);

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
        type = newType;

        var newColor = ColorHandler.COLORS[newType];
        color = newColor;
        
        tileObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = newColor;
    }

    public ColorType getRandomColorType(){
        return (ColorType)ColorHandler.ColorTypeArray.GetValue(UnityEngine.Random.Range(0, ColorHandler.ColorTypeArray.Length-1));
    }
}
