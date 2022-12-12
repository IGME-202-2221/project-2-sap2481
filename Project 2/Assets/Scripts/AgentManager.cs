using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AgentManager : MonoBehaviour
{
    [SerializeField]
    Agent godPrefab;

    [SerializeField]
    Agent manPrefab;

    [SerializeField]
    GameObject firePrefab;

    [SerializeField]
    int agentSpawnCount;

    Camera cam;

    List<Agent> agents = new List<Agent>(); 

    bool fireOnField = false;

    public List<Agent> Agents
    {
        get { return agents; }
    }

    public List<Obstacle> obstacles = new List<Obstacle>();
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        for (int i = 0; i < agentSpawnCount; i++)
        {
            if (i % 2 == 0)
            {
                agents.Add(Instantiate(manPrefab, new Vector3(-5.5f, -3.5f, 0), Quaternion.identity, cam.transform));               
            }
            else
            {
                agents.Add(Instantiate(godPrefab, new Vector3(5.5f, 3.5f, 0), Quaternion.identity, cam.transform));
            }
            agents[i].Init(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = 0;

        if (Mouse.current.leftButton.wasPressedThisFrame && fireOnField == false)
        {
            Instantiate(firePrefab, mousePos, Quaternion.identity, cam.transform);
            fireOnField = true;
            BroadcastMessage("FireOnField");
        }
    }

    public void FireGotByGods()
    {
        BroadcastMessage("FireClaimed");
        fireOnField = false;
    }
    public void FireGotByMan()
    {
        BroadcastMessage("FireClaimedByMan");
    }
    public void FireGone()
    {
        BroadcastMessage("ResetState");
        fireOnField = false;
    }
}
