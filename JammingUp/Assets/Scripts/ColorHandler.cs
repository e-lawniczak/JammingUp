using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorHandler
{
    public static Dictionary<ColorType, Color> COLORS = new Dictionary<ColorType, Color>()
    {
        { ColorType.RED, Color.red },
        {ColorType.BLUE, Color.blue},
        {ColorType.YELLOW, Color.yellow},
        {ColorType.GREEN, Color.green},
        {ColorType.WHITE, Color.white},
    };
    public static Array ColorTypeArray = Enum.GetValues(typeof(ColorType));

    public static ColorType GetNextColor(ColorType currentColor)
    {
        Array colors = ColorTypeArray;
        int currentIndex = Array.IndexOf(colors, currentColor);
        int nextIndex = (currentIndex + 1) % colors.Length;
        return (ColorType)colors.GetValue(nextIndex);
    }

    public static ColorType GetNextColorExceptWhite(ColorType currentColor){
        ColorType nextColor = GetNextColor(currentColor);
        if(nextColor == ColorType.WHITE){
            return GetNextColor(ColorType.WHITE);
        }else{
            return nextColor;
        }
    }
}

public enum ColorType
{
    RED,
    YELLOW,
    BLUE,
    GREEN,
    WHITE
}