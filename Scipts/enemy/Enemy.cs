using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    private StateMachine stateMachine;
    private NavMeshAgent agent;
    public NavMeshAgent Agent { get => agent; }
   
    [SerializeField]
    private string currentState;
    public Paths paths;
    // Start is called before the first frame update
    void Start()
    {
     stateMachine=GetComponent<StateMachine>();
        agent=GetComponent<NavMeshAgent>();
        stateMachine.Initialiaze();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
