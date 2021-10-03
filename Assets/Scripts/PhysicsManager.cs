using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsManager : Manager<PhysicsManager>
{
    // Variables for controlling for physics of objects
    [SerializeField]
    private float gravity = 0.1f;

    public float getGravity()
    {
        return gravity;
    }
}
