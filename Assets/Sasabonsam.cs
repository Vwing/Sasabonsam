using UnityEngine;
using System.Collections;

public class Sasabonsam : MonoBehaviour {
    public Transform leftLeg;
    public Transform rightLeg;
    [Tooltip("Time in seconds to descend fully")]
    public float descentTime = 3f;
    [Tooltip("Time in seconds for legs to stay in tree")]
    public float eatTime = 2f;
    public State state = State.Descending;

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
                //temp to see if works
                if (timer >= eatTime)
                {
                    timer = 0f;
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
