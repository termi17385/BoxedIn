using System;
using BoxedIn.testing;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Index))]
[RequireComponent(typeof(NavMeshAgent))]
public class AgentManager : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [Header("Sets the Agents field of view")]
    [SerializeField] private float lineDist; 
    [SerializeField] private float lineOffsetDist; 
    
    private float speed;
    private float searchProgress = 5;
    [FormerlySerializedAs("_angle")] public float angle;
    [NonSerialized] public SearchZone searchZone;
    public Vector3 PathTarget => agent.steeringTarget;
    
    [Header("Debugging")]
    public bool searchArea = false;
    public bool targetSpotted;
    [NonSerialized] public Transform waypoint;
    public Transform searchTarget;
    [NonSerialized] public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;

        speed = 3.5f;
    }
    private void Update()
    {
        AgentSight();
    }
    /// <summary> Handles the search when the agent is in a search zone making
    /// it look at key objects </summary>
    /// 
    public void Search()
    {
        if (searchArea)
        {
            // checks to see what search type
            if (searchZone.currentSearchType == SearchType.QuickSearch)
            {
                // when its a quick search scans given objects in the search array
                searchTarget = searchZone.searchObjects[searchZone.count];
                // looks at the current object for a set time
                LookAtTarget(searchTarget.position);
                searchProgress -= 2 * Time.deltaTime;
                if (searchProgress <= 0)
                {
                    // after a set time increases the search count
                    searchZone.count++;
                    
                    // resets the  search count if gone past the length of the array
                    if (searchZone.count >= searchZone.searchObjects.Length)
                        searchZone.count = 0;
                    
                    // resets the timer
                    searchProgress = 5;
                }
            }
        }
    }
    /// <summary> Handles looking for the player and only chasing if there is clear line of sight </summary>
    private void AgentSight()
    {
        var dir = target.position - transform.position;
        var sightAngle = Vector3.Angle(dir, transform.forward);
        this.angle = sightAngle;
        
        //13.63 50.12032
        // if the angle is less than or equal to 50 then see if there is line of sight
        if (sightAngle <= 50.12032f)
        {
            RaycastHit hit;
            if (Physics.Linecast(transform.position, target.position, out hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.DrawLine(transform.position, target.position, Color.green);
                    targetSpotted = true;
                }
                else
                {
                    Debug.DrawLine(transform.position, target.position, Color.red);
                    targetSpotted = false;
                }
            }
        }
        else targetSpotted = false;
        Debugging();
    }
    /// <summary> Used to debug the agents sight lines </summary>
    private void Debugging()
    {
        float distance = lineDist;                         // used to make it go further out
        float offsetDistance = lineOffsetDist;             // used to spread out the lines
        Vector3 offset = transform.right * offsetDistance; // sets the offset
        Vector3 forward = transform.forward * distance;    // sets the direction

        Debug.DrawLine(transform.position, transform.position + forward - offset, Color.blue); // left
        Debug.DrawLine(transform.position, transform.position + forward, Color.blue);          // middle
        Debug.DrawLine(transform.position, transform.position + forward + offset, Color.blue); // right
    }
    /// <summary> Makes the agent look towards the path they are taking </summary>
    public void LookAtTarget(Vector3 _target)
    {
        var position = transform.position;
        var position1 = _target;
     
        // gets the normalised direction
        Vector3 direction = (position1 - position).normalized;
        // sets the look rotation
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        // makes the agent rotate to look in that direction
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 5 * Time.deltaTime);
    }
    /// <summary> Moves the agent towards a waypoint or a target </summary>
    public void SetAgentDestination(Transform _move)
    {
        var position = _move.position;
        agent.SetDestination(position);
        agent.speed = speed;
    }

    private void OnCollisionEnter(Collision _other)
    {
        if(_other.gameObject.CompareTag("Player"))
        {
            var player = _other.gameObject.GetComponent<PlayerController>();
            player.PlayerCapture();
        }
    }
}
