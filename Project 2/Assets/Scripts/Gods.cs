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
    bool manHasFire = false;

    private void Awake()
    {
        worldSize.y = Camera.main.orthographicSize;
        worldSize.x = Camera.main.aspect * worldSize.y;
    }

    protected override void CalcSteeringForces()
    {
        boundsForce = StayInBounds(worldSize, boundsTimeCheck); //Stay in Bounds
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
                SendMessageUpwards("FireGotByGods");
            }
        }
        else if (manHasFire) //Pursue
        {
            float sqrDist;
            gameObject.GetComponent<SpriteRenderer>().material.color = Color.yellow;

            sqrDist = 0;
            List<Man> menList = new List<Man>();
            Man[] men = FindObjectsOfType<Man>();
            for (int i = 0; i < men.Length; i++)
            {
                menList.Add(men[i]);
            }

            for (int i = 0; i < menList.Count; i++)
            {
                if (menList[i].HasFire == true)
                {
                    totalSteeringForce += Seek(menList[i].physicsObject.Position);
                    if (Collision(gameObject, menList[i].gameObject))
                    {
                        manager.Agents.Remove(menList[i]);
                        Destroy(menList[i].gameObject);
                        SendMessageUpwards("FireGone");
                    }
                    menList.Remove(menList[i]);
                }
            }
            for (int i = 0; i < menList.Count; i++)
            {
                sqrDist = Vector3.SqrMagnitude(physicsObject.Position - menList[i].physicsObject.Position);
                if (sqrDist != 0)
                {
                    totalSteeringForce += Flee(menList[i].physicsObject.Position) * (1f / sqrDist);
                }
            }

            sqrDist = 0f;
            Gods[] gods = FindObjectsOfType<Gods>();
            for (int i = 0; i < gods.Length; i++)
            {
                sqrDist = Vector3.SqrMagnitude(physicsObject.Position - gods[i].physicsObject.Position);
                if (sqrDist != 0)
                {
                    totalSteeringForce += Flee(gods[i].physicsObject.Position) * (.45f / sqrDist);
                }
            }
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
}
