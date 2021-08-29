using System.Collections;
using UnityEngine;

public enum SearchType
{
    QuickSearch,
    LongSearch,
    MoveItem
}

[RequireComponent(typeof(SphereCollider))]
public class SearchZone : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private AgentManager agent;

    private SphereCollider sphereCollider;
    private Color color = Color.green;
    
    public SearchType currentSearchType = SearchType.QuickSearch;
    public Transform[] searchObjects;
    public int count;

    /// <summary> Used to disable search after a set amount of time </summary>
    private IEnumerator EndSearch()
    {
        // after 10 seconds of searching
        yield return new WaitForSeconds(10);

        // if the agent isnt null
        if(agent != null)
        {
            // stops the search and sets agent to null
            agent.searchArea = false;
            agent = null;
        }
    }

    private void OnTriggerEnter(Collider _other)
    {
        // when the agent enters the search zone
        if (_other.CompareTag("Enemy"))
        {   
            // set the agent to the one that entered
            agent = _other.GetComponent<AgentManager>();
            
            // set the agents search zone to this one
            agent.searchZone = GetComponent<SearchZone>();
            color = Color.red;

            // set the agent to searching and set turn on end search
            agent.searchArea = true;
            StartCoroutine(EndSearch());
        }
    }
    private void OnTriggerExit(Collider _other)
    {
        if (_other.CompareTag("Enemy")) 
        {
            color = Color.green;

            if(agent != null)
            {
                agent.searchArea = false;
                agent = null;
            }
        }
    }
    
    // Just for debugging purposes
    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        sphereCollider = GetComponent<SphereCollider>();
        
        Gizmos.DrawWireSphere(transform.position, 
            (sphereCollider.radius = radius));
    }
}
