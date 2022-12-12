using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Man : Agent
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
    bool manHasFire = false;
    bool hasFire = false;

    public bool HasFire
    {
        get { return hasFire; }
    }

    private void Awake()
    {
        worldSize.y = Camera.main.orthographicSize;
        worldSize.x = Camera.main.aspect * worldSize.y;
    }

    protected override void CalcSteeringForces()
    {
        boundsForce = StayInBounds(worldSize, boundsTimeCheck); //Stay In Bounds
        totalSteeringForce += boundsForce * boundsWeight;
        totalSteeringForce += AvoidObstacle(); //Avoid Obstacles

        if (fireOnField == true) //Seek
        {
            gameObject.GetComponent<SpriteRenderer>().material.color = Color.cyan;
            totalSteeringForce += Separation(0.5f);

            totalSteeringForce += SeekFire();
            if (Collision(gameObject, GameObject.FindGameObjectWithTag("Fire")))
            {
                Destroy(GameObject.FindGameObjectWithTag("Fire"));
                fireOnField = false;
                hasFire = true;
                SendMessageUpwards("FireGotByMan");
                StartCoroutine(TurnRed());
            }
        }
        else if (hasFire == true) //Flee
        {
            Gods[] gods = FindObjectsOfType<Gods>();
            for (int i = 0; i < gods.Length; i++)
            {
                totalSteeringForce += Flee(gods[i].physicsObject.Position);
            }
        }
        else if (manHasFire == true) //Flock
        {
            gameObject.GetComponent<SpriteRenderer>().material.color = Color.green;
            Flock();
        }
        else //Wander
        {
            gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
            wanderForce = Wander(futureTime, wanderRadius);
            totalSteeringForce += wanderForce;
            totalSteeringForce += Separation();
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
    public void FireClaimedByMan()
    {
        manHasFire = true;
        fireOnField = false;
    }
    public void ResetState()
    {
        manHasFire = false;
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

    IEnumerator TurnRed()
    {
        gameObject.GetComponent<SpriteRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(10f);
        gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
        hasFire = false;
        SendMessageUpwards("FireGone");
    }
}
