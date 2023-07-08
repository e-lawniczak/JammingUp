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

}
public enum ColorType
{
    RED,
    YELLOW,
    BLUE,
    GREEN,
    WHITE
}