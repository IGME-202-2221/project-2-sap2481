using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TagStates
{
    NotIt,
    It,
    Counting
}

public class TagPlayer : Agent
{
    TagStates currentState = TagStates.NotIt;

    [SerializeField]
    float countTime = 10f;
    float timer;

    [SerializeField]
    SpriteRenderer spriteRenderer;
    
    protected override void CalcSteeringForces()
    {
        switch (currentState)
        {
            case TagStates.NotIt:
                //Flee It Agent
                //totalSteeringForce += Flee(manager.Agents[manager.ItAgentIndex].transform.position);
                break;
            case TagStates.It:
                //See nearest agent (not self)
                //totalSteeringForce += Seek(manager.Agents[index].transform.position);

                /*if (agentDist <= radius)
                {
                    manager.TagPlayer(index);
                    ChangeStateTo(TagStates.NotIt);
                }*/
                break;
            case TagStates.Counting:
                //count down
                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    ChangeStateTo(TagStates.It);
                }
                break;
        }
    }

    public void ChangeStateTo(TagStates newState)
    {
        switch (newState)
        {
            case TagStates.NotIt:
                //Make a new IT in manager
                break;
            case TagStates.It:
                //
                break;
            case TagStates.Counting:
                //Setup a count timer
                timer = countTime;
                break;
        }
    }
}
