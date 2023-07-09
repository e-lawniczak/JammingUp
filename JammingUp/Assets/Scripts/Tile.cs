using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;

public class Tile : MonoBehaviour
{
    public int x { get; set;}
    public int y { get; set; }
    private float cellSize;
    private ColorType type;
    private Color color;
    private GameObject tileObject;

    public Tile(int x, int y, GameObject go){
        this.x = y;
        this.y = x;
        tileObject = go;
        UpdateColor(getRandomColorType());
        if(this.x == 8 && this.y == 15)
            UpdateColor(ColorType.WHITE);

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


    public void AssignNewTile(Tile tile, GameObject go)
    {
        Destroy(this.tileObject);
        this.tileObject = go;
        this.color = tile.color;
        this.type = tile.type;
        tileObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = tile.color;
    }
    
}
