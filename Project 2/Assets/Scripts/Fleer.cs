using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Fleer : Agent
{
    Vector3 mousePosition;
    Vector3 mouseForce;

    protected override void CalcSteeringForces()
    {
        mousePosition = Mouse.current.position.ReadValue();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;

        mouseForce = Flee(mousePosition);

        totalSteeringForce += mouseForce;
    }
}
