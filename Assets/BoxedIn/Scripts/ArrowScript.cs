using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{

    public Transform destination;
    public Transform player;
    public float rotationSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.position;
        
        Vector3 dir = destination.position - this.transform.position;
        float angle = Vector3.SignedAngle(this.transform.right, dir, this.transform.up);
        this.transform.Rotate(this.transform.up, angle * Time.deltaTime * rotationSpeed) ;
        Debug.Log(angle + ", " + player.position + ", " + destination.position);
        
    }
}
