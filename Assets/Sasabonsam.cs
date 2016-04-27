using UnityEngine;
using System.Collections;

public class Sasabonsam : MonoBehaviour {
    public Transform leftLeg;
    public Transform rightLeg;
    public Transform glue;
    [Tooltip("Time in seconds to descend fully")]
    public float descentTime = 3f;
    [Tooltip("Time in seconds for legs to stay in tree")]
    public float eatTime = 2f;
    public State state = State.Descending;
    public Transform gluedMmoatia;

    private float maxScale = 4.3f; //max scale leg needs to be to reach mmoatia
    private float minScale = .1f; //min scale leg needs to be to be hidden
    private Vector3 maxScaleV;
    private Vector3 minScaleV;
    private float timer = 0f;
    private float speed; //determined by descentTime and amount of scale needing to be covered

    void Start()
    {
        maxScaleV = leftLeg.localScale;
        maxScaleV.y = maxScale;
        minScaleV = leftLeg.localScale;
        minScaleV.y = minScale;
        speed = (maxScale - minScale) / descentTime;
    }

    public enum State
    {
        Descending,
        Waiting,
        Ascending,
        Eating
    }

	void UpdateState()
    {
        timer += Time.deltaTime;
        switch (state)
        {
            case State.Eating:
                if (timer >= eatTime)
                {
                    timer = 0f;
                    if(gluedMmoatia)
                    {
                        NavMeshAgent mmoatiaAgent = gluedMmoatia.GetComponent<NavMeshAgent>();
                        if (mmoatiaAgent)
                            mmoatiaAgent.enabled = true;
                        CollectHerbs mmoatiaHerbs = gluedMmoatia.GetComponent<CollectHerbs>();
                        if (mmoatiaHerbs)
                            mmoatiaHerbs.enabled = true;
                        gluedMmoatia.SetParent(null);
                        gluedMmoatia.gameObject.SetActive(false);
                        gluedMmoatia = null;
                    }
                    state = State.Descending;
                }
                break;
            case State.Descending:
                if (timer >= descentTime)
                {
                    timer = 0f;
                    leftLeg.localScale = maxScaleV;
                    rightLeg.localScale = maxScaleV;
                    state = State.Waiting;
                }
                break;
            case State.Ascending:
                if (timer >= descentTime)
                {
                    timer = 0f;
                    leftLeg.localScale = minScaleV;
                    rightLeg.localScale = minScaleV;
                    state = State.Eating;
                }
                break;
            case State.Waiting:
                if(gluedMmoatia)
                {
                    timer = 0f;
                    Animator mmoatiaAnim = gluedMmoatia.GetComponent<Animator>();
                    if(mmoatiaAnim)
                        mmoatiaAnim.Stop();
                    NavMeshAgent mmoatiaAgent = gluedMmoatia.GetComponent<NavMeshAgent>();
                    if (mmoatiaAgent)
                        mmoatiaAgent.enabled = false;
                    CollectHerbs mmoatiaHerbs = gluedMmoatia.GetComponent<CollectHerbs>();
                    if (mmoatiaHerbs)
                        mmoatiaHerbs.enabled = false;
                    gluedMmoatia.SetParent(glue);
                    state = State.Ascending;
                }
                break;
        }
    }

    Vector3 newScale;
    void UpdateScale()
    {
        switch(state)
        {
            case State.Waiting:
                break;
            case State.Eating:
                break;
            case State.Ascending:
                newScale = leftLeg.localScale;
                newScale.y -= Time.deltaTime * speed;
                leftLeg.localScale = newScale;
                rightLeg.localScale = newScale;
                break;
            case State.Descending:
                newScale = leftLeg.localScale;
                newScale.y += Time.deltaTime * speed;
                leftLeg.localScale = newScale;
                rightLeg.localScale = newScale;
                break;
        }
    }
	
	void Update ()
    {
        UpdateState();
        UpdateScale();
	}
}
