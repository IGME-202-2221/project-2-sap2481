using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gods : Agent
{
    [SerializeField]
    float futureTime = 2f;

    [SerializeField]
    float wanderRadius = 2f;

    [SerializeField]
    Vector2 worldSize;

    Vector3 boundsForce;
    Vector3 wanderForce;

    [SerializeField]
    float boundsTimeCheck;

    [SerializeField]
    float boundsWeight = 1f;

    private void Awake()
    {
        worldSize.y = Camera.main.orthographicSize;
        worldSize.x = Camera.main.aspect * worldSize.y;
    }

    protected override void CalcSteeringForces()
    {
        wanderForce = Wander(futureTime, wanderRadius);
        totalSteeringForce += wanderForce;

        boundsForce = StayInBounds(worldSize, boundsTimeCheck);
        totalSteeringForce += boundsForce * boundsWeight;

        totalSteeringForce += Separation();

        totalSteeringForce += AvoidObstacle();
    }
}
