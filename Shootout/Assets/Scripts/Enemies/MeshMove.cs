using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeshMove : MonoBehaviour {

    public Transform goal;
    public NavMeshAgent agent;
    public GameObject player1;

    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1");
        GetComponent<MeshMove>().goal = player1.transform;

        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
    }
    // Update is called once per frame
    void Update () {
        agent.destination = goal.position;

    }
}
