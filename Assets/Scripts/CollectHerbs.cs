using UnityEngine;
using System.Collections;

public class CollectHerbs : MonoBehaviour
{
    public float collectDuration = 3f;
    public State state;
    State lastState;
    private NavMeshAgent agent;
    private Animator anim;
    Vector3 start;
    float idleTimer = 0f;

    public enum State
    {
        Run,
        Idle,
        Return,
        Finished
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        start = transform.position;
        lastState = State.Idle;
        state = State.Run;
        GoToHerbs();
    }

    void GoToHerbs()
    {
        agent.destination = Vector3.zero;
        anim.Play("Running");
    }

    void GoToStart()
    {
        agent.destination = start;
        anim.Play("Running");
    }

    void Idle()
    {
        idleTimer += Time.deltaTime;
        //agent.destination = transform.position;
        anim.Play("Idle");
        anim.speed = 1;
    }

    void UpdateTimers()
    {
        if (idleTimer > 3f)
            idleTimer = 0f;
        else if (idleTimer > 0)
            idleTimer += Time.deltaTime;
    }

    bool ReachedDestination()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }
        return false;
    }

    void UpdateState()
    {
        if (!ReachedDestination())
            return;
        if (state == State.Run)
            state = State.Idle;
        else if (state == State.Idle && idleTimer > collectDuration)
        {
            idleTimer = 0f;
            state = State.Return;
        }
        else if (state == State.Return)
            state = State.Finished;
    }
    void EndLife()
    {
        Destroy(this.gameObject);
    }
    void Update()
    {
        UpdateState();
        if (state == lastState && state != State.Idle)
            return;
        switch(state)
        {
            case State.Run:
                GoToHerbs();
                break;
            case State.Idle:
                Idle();
                break;
            case State.Return:
                GoToStart();
                break;
            case State.Finished:
                EndLife();
                break;
        }
        lastState = state;
    }
}
