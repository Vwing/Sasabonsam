using UnityEngine;
using System.Collections;

public class StayFixed : MonoBehaviour {
    Vector3 initPos;
	// Use this for initialization
	void Start () {
        initPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = initPos;
	}
}
