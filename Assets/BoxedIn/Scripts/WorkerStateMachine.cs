using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public enum States
{
    Patrol,
    Search,
    Chase,
    Attack
}

public delegate void StateDelegate();
[RequireComponent(typeof(AgentManager))]
public class WorkerStateMachine : MonoBehaviour
{
    private Dictionary<States, StateDelegate> states = new Dictionary<States, StateDelegate>();
    [SerializeField] private States currentState = States.Patrol;
    [SerializeField] private AgentManager agent;
    public DetectionHandler detect;
    public void ChangeStates(States _newStates) => currentState = _newStates;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<AgentManager>();
        
        states.Add(States.Patrol, delegate { agent.LookAtTarget(agent.PathTarget); 
        agent.SetAgentDestination(agent.waypoint); });
        
        states.Add(States.Chase, delegate { agent.LookAtTarget(agent.PathTarget);
        agent.SetAgentDestination(agent.target); }); 
        
        //agent.LookAtTarget(agent.searchTarget.position);
        states.Add(States.Search, () => agent.Search());
    }

    // Update is called once per frame
    void Update()
    {
        // These two lines are used to run the state machine
        // it works by attempting to retrieve the relevant function for the current state.
        // then running the function if it successfully found it 
        
        if(states.TryGetValue(currentState, out StateDelegate state)) state.Invoke();
        else Debug.Log($"No State Was Set For {currentState}.");


        // place holder for handling swapping between states
        if (agent.searchArea)
        {
            currentState = States.Search;
            detect.PlayerUndetected(this.gameObject.name);
        }
        else if (agent.targetSpotted)
        {
            currentState = States.Chase;
            detect.PlayerDetected(this.gameObject.name);
        }
        else
        {
            currentState = States.Patrol;
            detect.PlayerUndetected(this.gameObject.name);
        }
    }
}
