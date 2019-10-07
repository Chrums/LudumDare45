using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Dimension2DManager
{
    public enum Dimension
    {
        AxisX = 0,
        AxisY,
        AxisZ,
        AxisT,
    }

    public static Dimension HorizontalAxis = Dimension.AxisX;
    public static Dimension VerticalAxis = Dimension.AxisY;

    public static event Action<int, int> CurrentFrameChangeEvent = null;

    private static int currentFrame = 0;

    public static int CurrentFrame
    {
        get
        {
            return currentFrame;
        }

        set
        {
            CurrentFrameChangeEvent?.Invoke(currentFrame, value);
            currentFrame = value;
        }
    }

    public static void SetDimensions(Dimension horizontal, Dimension vertical)
    {
        HorizontalAxis = horizontal;
        VerticalAxis = vertical;
    }
}
