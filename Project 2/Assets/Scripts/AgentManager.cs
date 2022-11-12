using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    [SerializeField]
    Agent agentPrefab;

    [SerializeField]
    int agentSpawnCount;

    List<Agent> agents = new List<Agent>();

    public List<Agent> Agents
    {
        get { return agents; }
    }

    int itAgentIndex;
    public int ItAgentIndex
    {
        get { return itAgentIndex; }
    }

    public List<Sprite> sprites = new List<Sprite>();
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < agentSpawnCount; i++)
        {
            agents.Add(Instantiate(agentPrefab));

            agents[i].Init(this);

            //((TagPlayer)agents[i]).ChangeStateTo(TagStates.NotIt);
        }

        if (agents.Count > 0)
        {
            TagPlayer(0);
        }
    }

    public void TagPlayer(int itPlayerIndex)
    {
        itAgentIndex = itPlayerIndex;

        //((TagPlayer)agents[0]).ChangeStateTo(TagStates.It);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
