using UnityEngine;
using System.Collections;

public class MoveAround : MonoBehaviour
{
    public Transform[] waypoints;
    private int point = 0;
    private NavMeshAgent agent;
    private Animator anim;
    private Vector3 destination;
    private float delta = 0.1f;

	void Start ()
	{
	    agent = GetComponent<NavMeshAgent>();
	    anim = GetComponent<Animator>();
	    GoToNextDest();
	}

    void GoToNextDest()
    {
        if (waypoints.Length < 1)
            return;
        destination = waypoints[point++].position;
        agent.destination = destination;
        anim.Play("Running");
        
        if (point >= waypoints.Length)
            point = 0;
        ++count;
    }

    void Idle()
    {
        agent.destination = transform.position;
        anim.Play("Idle");
        Debug.Log("idling");
        anim.speed = 1;
        ++count;
    }

    private int count = 0;

	void Update ()
	{
	    if (count > 4)
	        return;
	    anim.speed = agent.velocity.magnitude/3;
	    if (count > 3)
	        Idle();
	    else if (Vector3.Distance(transform.position, destination) < delta)
	        GoToNextDest();
	}
}
