using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Seeker : Agent
{
    Vector3 mousePosition;
    Vector3 mouseForce;

    protected override void CalcSteeringForces()
    {
        mousePosition = Mouse.current.position.ReadValue();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;

        mouseForce = Seek(mousePosition);

        totalSteeringForce += mouseForce;
    }
}
