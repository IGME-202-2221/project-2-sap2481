using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    [SerializeField]
    Agent godPrefab;

    [SerializeField]
    Agent manPrefab;

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
            if (i % 2 == 0)
            {
                agents.Add(Instantiate(manPrefab, new Vector3(-5, -3, 0), Quaternion.identity));               
            }
            else
            {
                agents.Add(Instantiate(godPrefab, new Vector3(5, 3, 0), Quaternion.identity));
            }
            agents[i].Init(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
