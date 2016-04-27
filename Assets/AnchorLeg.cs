using UnityEngine;
using System.Collections;

public class AnchorLeg : MonoBehaviour {
    Vector3 initPos;
	// Use this for initialization
	void Start () {
        initPos = transform.localPosition;
        initPos.y += transform.localScale.y;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 newPos = initPos;
        newPos.y = initPos.y - transform.localScale.y;
        transform.localPosition = newPos;
	}
}
