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

    bool fireOnField = false;

    private void Awake()
    {
        worldSize.y = Camera.main.orthographicSize;
        worldSize.x = Camera.main.aspect * worldSize.y;
    }

    protected override void CalcSteeringForces()
    {

        boundsForce = StayInBounds(worldSize, boundsTimeCheck);
        totalSteeringForce += boundsForce * boundsWeight;

        totalSteeringForce += Separation();

        if (fireOnField == true)
        {
            totalSteeringForce += SeekFire();
        }
        else
        {
            wanderForce = Wander(futureTime, wanderRadius);
            totalSteeringForce += wanderForce;
        }

        totalSteeringForce += AvoidObstacle();

        if (fireOnField == true)
        {
            if (Collision(gameObject, GameObject.FindGameObjectWithTag("Fire")))
            {
                Destroy(GameObject.FindGameObjectWithTag("Fire"));
                fireOnField = false;
                SendMessageUpwards("FireClaimed");
            }
        }
    }
    public void FireOnField()
    {
        fireOnField = true;
    }
    public void FireClaimed()
    {
        fireOnField = false;
    }

    bool Collision(GameObject vehicle, GameObject obstacle)
    {
        bool isColliding;

        if (vehicle.GetComponent<SpriteRenderer>().bounds.min.x < obstacle.GetComponent<SpriteRenderer>().bounds.max.x &&
            vehicle.GetComponent<SpriteRenderer>().bounds.max.x > obstacle.GetComponent<SpriteRenderer>().bounds.min.x &&
            vehicle.GetComponent<SpriteRenderer>().bounds.min.y < obstacle.GetComponent<SpriteRenderer>().bounds.max.y &&
            vehicle.GetComponent<SpriteRenderer>().bounds.max.y > obstacle.GetComponent<SpriteRenderer>().bounds.min.y)
        {
            isColliding = true;
        }
        else
        {
            isColliding = false;
        }

        return isColliding;
    }
}
