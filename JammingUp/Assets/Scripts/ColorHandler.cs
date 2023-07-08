using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorHandler 
{
    public static Dictionary<ColorType, Color> COLORS = new Dictionary<ColorType, Color>();
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