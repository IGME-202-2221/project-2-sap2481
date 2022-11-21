using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhysicsObject))]

public abstract class Agent : MonoBehaviour
{

    [SerializeField]
    private PhysicsObject physicsObject;

    AgentManager manager;
    protected List<Vector3> tempObList = new List<Vector3>();

    [SerializeField]
    float maxSpeed = 2f;

    [SerializeField]
    protected float avoidFutureTime = 5f;
    [SerializeField]
    protected float avoidRadius = 1f;

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

    /*protected void AvoidObstacle(Obstacle obstacle)
    {
        //Get a vector from this agent to the obstacle
        Vector3 toObstacle = obstacle.Position - physicsObject.Position;

        //Check if obstacle is behind agent
        float fwdToObstacleDot = Vector3.Dot(physicsObject.Direction, toObstacle);
        if (fwdToObstacleDot < 0)
        {
            return;
        }

        //Check if obstacle is too far to the left or right
        float rightToObstacleDot = Vector3.Dot(physicsObject.Right, toObstacle);
        if (Mathf.Abs(rightToObstacleDot) > physicsObject.radius + obstacle.Radius)
        {
            return;
        }

        //Check if obstacle is within vision range
        if (fwdToObstacleDot > avoidFutureTime)
        {
            return;
        }

        //Avoid the Obstacle
        Vector3 desiredVelocity;
        if (rightToObstacleDot > 0)
        {
            //If the obstacle is on the right, steer left
            desiredVelocity = physicsObject.Right * -maxSpeed;
        }
        else
        {
            //If the obstacle is on the left, steer right
            desiredVelocity = physicsObject.Right * maxSpeed;
        }

        //Create a weight based on how close we are to the obstacle
        float weight = avoidFutureTime / (fwdToObstacleDot * 0.1f);

        Vector3 steeringForce = (desiredVelocity - physicsObject.Velocity) * weight;

        totalSteeringForce += steeringForce;
    }

    protected void AvoidAllObstacles()
    {
        foreach (Obstacle obstacle in manager.obstacles) 
        {
            AvoidObstacle(obstacle);
        }
    }*/

    protected Vector3 AvoidObstacle()
    {        
        Vector3 avoidForce = Vector3.zero;
        Vector3 VToO = Vector3.zero;
        float rightDot, forwardDot;

        Vector3 futurePos = CalcFuturePosition(avoidFutureTime);
        float futureSqrDist = Vector3.SqrMagnitude(futurePos - physicsObject.Position);
        futureSqrDist += avoidRadius;

        tempObList.Clear();

        //Loop through all obstacles to find any in my way
        foreach(Obstacle obstacle in manager.obstacles)
        {   
            VToO = obstacle.Position - physicsObject.Position;
            forwardDot = Vector3.Dot(VToO, physicsObject.Velocity.normalized);
            float weight = avoidFutureTime / (forwardDot + 0.1f);

            //Check if it's in front of me
            if (forwardDot >= 0 && (forwardDot * forwardDot) <= futureSqrDist) //And not too far and front of me
            {
                rightDot = Vector3.Dot(VToO, transform.right);
                if (Mathf.Abs(rightDot) <= avoidRadius)
                {
                    tempObList.Add(obstacle.Position); //Move this line

                    if (rightDot < 0)
                    {
                        //Turn Right
                        avoidForce += transform.right * maxSpeed * weight;
                    }
                    else
                    {
                        //Turn Left
                        avoidForce += -transform.right * maxSpeed * weight;
                    }
                }
            }
        }
        return avoidForce;
    }

    private void OnDrawGizmosSelected()
    {
        //Draw lines to any obstacles to avoid
        Gizmos.color = Color.red;
        foreach(Vector3 obPos in tempObList)
        {
            Gizmos.DrawLine(transform.position, obPos);
        }

        //  Draw agent safe zone
        Gizmos.color = Color.green;
        Vector3 futurePos = CalcFuturePosition(avoidFutureTime);
        float futureDist = Vector3.Magnitude(futurePos - physicsObject.Position);
        futureDist += avoidRadius;

        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(new Vector3(0, futureDist / 2f, 0), new Vector3(avoidRadius * 2f, futureDist, avoidRadius));
        Gizmos.matrix = Matrix4x4.identity;
    }
}
