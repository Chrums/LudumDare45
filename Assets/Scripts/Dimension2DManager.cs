using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dimension2DManager : MonoBehaviour
{
    public enum Dimension
    {
        AxisX = 0,
        AxisY,
        AxisZ,
        AxisT,
    }

    public Dimension HorizontalAxis = Dimension.AxisX;
    public Dimension VerticalAxis = Dimension.AxisY;

    void SetDimensions(Dimension horizontal, Dimension vertical)
    {
        HorizontalAxis = horizontal;
        VerticalAxis = vertical;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
