using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsManager : Manager<PhysicsManager>
{
    // Variables for controlling for physics of objects
    [SerializeField]
    private float gravity = 0.1f;
    [SerializeField]
    private float worldWidth = 480;
    [SerializeField]
    private float worldHeight = 300;
    [SerializeField]
    private float horizontalOffset = 55;
    [SerializeField]
    private float verticalOffset = 100;

    public float getGravity()
    {
        return gravity;
    }

    public float checkHorizontalOffset()
    {
        return worldWidth - horizontalOffset;
    }

    public float checkVerticalOffset()
    {
        return worldHeight - verticalOffset;
    }
}
