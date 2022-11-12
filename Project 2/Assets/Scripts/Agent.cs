using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhysicsObject))]

public abstract class Agent : MonoBehaviour
{

    [SerializeField]
    private PhysicsObject physicsObject;

    AgentManager manager;

    [SerializeField]
    float maxSpeed = 2f;

    [SerializeField]
    float maxForce = 2f;

    [SerializeField]
    bool useVelocity = true;

    protected Vector3 totalSteeringForce;

    // Start is called before the first frame update
    void Start()
    {
        physicsObject = GetComponent<PhysicsObject>();
    }

    public void Init(AgentManager manager)
    {
        this.manager = manager;
    }

    // Update is called once per frame
    void Update()
    {
        totalSteeringForce = Vector3.zero;
        
        CalcSteeringForces();

        //Limit total force
        totalSteeringForce = Vector3.ClampMagnitude(totalSteeringForce, maxForce);

        physicsObject.ApplyForce(totalSteeringForce);
    }

    protected abstract void CalcSteeringForces();

    public Vector3 Seek(Vector3 targetPosition)
    {
        Vector3 desiredVelocity = targetPosition - transform.position;

        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        Vector3 seekForce = desiredVelocity - physicsObject.Velocity;

        return seekForce;
    }

    public Vector3 Wander(float futureTime, float wanderRadius) //Pick a random point and seek it
    {
        Vector3 wanderPos = CalcFuturePosition(futureTime);

        float wanderAngle = Random.Range(0, 360);
        wanderPos.x += Mathf.Cos(wanderAngle * Mathf.Deg2Rad) * wanderRadius;
        wanderPos.y += Mathf.Sin(wanderAngle * Mathf.Deg2Rad) * wanderRadius;
        
        return Seek(wanderPos);
    }

    public Vector3 Flee(Vector3 targetPosition)
    {
        Vector3 desiredVelocity = transform.position - targetPosition;

        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        Vector3 fleeForce = desiredVelocity - physicsObject.Velocity;

        return fleeForce;
    }

    public Vector3 CalcFuturePosition(float time)
    {
        return physicsObject.Position + (physicsObject.Direction * maxSpeed) * time;
    }

    public Vector3 StayInBounds(Vector2 bounds, float time = 0f)
    {
        //Calc position to check
        //Vector3 position = transform.position;
        Vector3 position = CalcFuturePosition(time);

        //Check if out of bounds
         if (position.x >= bounds.x || position.x <= -bounds.x || position.y >= bounds.y || position.y <= -bounds.y)
        {
            return Seek(Vector3.zero);
            //You can try to alter the wanderAngle itself to not ride the border, BUT riding the border is okay for this exercise as long as they stay in bounds
        }
        else
        {
            return Vector3.zero;
        }
    }

    public Vector3 Separation()
    {
        Vector3 separateForce = Vector3.zero;
        float sqrDist;

        foreach(Agent other in manager.Agents)
        {
            sqrDist = Vector3.SqrMagnitude(physicsObject.Position - other.physicsObject.Position);

            if (sqrDist != 0)
            {
                separateForce += Flee(other.physicsObject.Position) * (1f / sqrDist);
            }
        }

        return separateForce;
    }
}
