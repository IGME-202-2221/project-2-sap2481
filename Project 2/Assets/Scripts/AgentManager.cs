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

    public List<Obstacle> obstacles = new List<Obstacle>();
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < agentSpawnCount; i++)
        {
            agents.Add(Instantiate(agentPrefab));

            agents[i].Init(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
