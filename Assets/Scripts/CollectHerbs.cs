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
        Finished,
        Collect
    }

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

    }

    void OnEnable()
    {
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

    void Collect()
    {
        agent.destination = transform.position;
        anim.Play("Collect");
        anim.speed = 1;
    }

    void Idle()
    {
        idleTimer += Time.deltaTime;
        agent.destination = transform.position;
        anim.Play("Idle");
        anim.speed = 1;
    }

    bool ReachedDestination()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                return true;
                //if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                //{
                //    return true;
                //}
            }
        }
        return false;
    }

    void UpdateState()
    {
        if (!ReachedDestination())
            return;
        if (state == State.Run)
            state = State.Collect;
        else if (state == State.Collect && AnimIsFinished())
        {
            state = State.Return;
        }
        else if (state == State.Return)
            state = State.Finished;
    }
    void EndLife()
    {
        gameObject.SetActive(false);
    }

    bool AnimIsFinished()
    {
        return (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0));
    }

    void SetAnimationSpeed()
    {
        if (state == State.Run)
            anim.speed = agent.velocity.sqrMagnitude / 4;
    }

    void Update()
    {
        UpdateState();
        SetAnimationSpeed();
        if (state == lastState)
            return;
        switch(state)
        {
            case State.Run:
                GoToHerbs();
                break;
            case State.Collect:
                Collect();
                break;
            case State.Return:
                GoToStart();
                break;
            case State.Finished:
                EndLife();
                break;
            case State.Idle:
                Idle();
                break;
        }
        lastState = state;
    }
}
