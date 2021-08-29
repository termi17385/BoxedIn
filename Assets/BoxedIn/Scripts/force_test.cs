using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class force_test : MonoBehaviour
{
    public float force;

    private Vector3 forcePoint;
    private Collider cube;
    private Rigidbody cubeRigid;

    // Start is called before the first frame update
    void Start()
    {
        cubeRigid = GetComponent<Rigidbody>();
        cube = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //forcePoint = this.transform.position + new Vector3(-0.51f, 0.4f, 0);p
            forcePoint = this.transform.position + new Vector3(-((cube.bounds.size.x/2) + 0.01f), (cube.bounds.size.y/2) - (cube.bounds.size.y * 0.1f), 0);
            cubeRigid.AddForceAtPosition(new Vector3(-force, 0, 0), forcePoint);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            forcePoint = this.transform.position + new Vector3((cube.bounds.size.x / 2) + 0.01f, (cube.bounds.size.y / 2) - (cube.bounds.size.y * 0.1f), 0);
            cubeRigid.AddForceAtPosition(new Vector3(force, 0, 0), forcePoint);
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            forcePoint = this.transform.position + new Vector3(0, (cube.bounds.size.y / 2) - (cube.bounds.size.y * 0.1f), -((cube.bounds.size.z / 2) + 0.01f));
            cubeRigid.AddForceAtPosition(new Vector3(0, 0, force), forcePoint);
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            forcePoint = this.transform.position + new Vector3(0, (cube.bounds.size.y / 2) - (cube.bounds.size.y * 0.1f), ((cube.bounds.size.z / 2) + 0.01f));
            cubeRigid.AddForceAtPosition(new Vector3(0, 0, -force), forcePoint);
        }
    }
}