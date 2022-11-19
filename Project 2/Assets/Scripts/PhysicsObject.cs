using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    Vector3 direction = Vector3.zero;               //Heading
    Vector3 velocity = Vector3.zero;                //Speed*
    Vector3 acceleration = Vector3.zero;            //Forces sum
    Vector3 position = Vector3.negativeInfinity;    //Where is it?

    public float radius = 1f;

    public Vector3 Direction
    {
        get { return direction; }
    }
    public Vector3 Velocity
    {
        get { return velocity; }
    }

    public Vector3 Acceleration
    {
        get { return acceleration; }
    }
    public Vector3 Position
    {
        get { return position; }
    }

    public Vector3 Right
    {
        get { return transform.right; }
    }

    [SerializeField]
    float mass = 1f;

    [SerializeField]
    bool useGravity = true;
    Vector3 gravity = Vector3.down;

    [SerializeField]
    bool useFriction;
    [SerializeField]
    float coefficientOfFriction = 0f;

    //Camera
    [SerializeField]
    Camera cam;

    [SerializeField, HideInInspector]
    float height;

    [SerializeField, HideInInspector]
    float width;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        cam = Camera.main;
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        //Use Gravity
        if (useGravity)
        {
            ApplyGravity();
        }
        if (useFriction)
        {
            ApplyFriction();
        }
        
        //Calculate the velocity for this frame
        velocity += acceleration * Time.deltaTime;

        position += velocity * Time.deltaTime;

        //Grab current direction from velocity
        direction = velocity.normalized;

        transform.position = position;

        //Zero out acceleration
        acceleration = Vector3.zero;

        //Bounce
        if (position.x < -(width / 2) + 1 || position.x > (width / 2) - 1)
        {
            velocity.x *= -1;
        }
        if (position.y < -(height / 2) + .5f || position.y > (height / 2) - .5f)
        {
            velocity.y *= -1;
        }

        //Rotate
        transform.rotation = Quaternion.LookRotation(Vector3.back, velocity);
    }

    public void ApplyForce(Vector3 force)
    {
        //F = MA --> A = F/M
        acceleration += (force / mass);
    }

    void ApplyGravity()
    {
        acceleration += gravity;
    }

    void ApplyFriction()
    {
        Vector3 friction = velocity * -1;
        friction.Normalize();
        friction = friction * coefficientOfFriction;
        ApplyForce(friction);
    }
}
