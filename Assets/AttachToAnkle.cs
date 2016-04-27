using UnityEngine;
using System.Collections;

public class AttachToAnkle : MonoBehaviour
{
    public Transform ankle;
    private Vector3 offset;
	// Use this for initialization
	void Start ()
	{
	    offset = transform.position - ankle.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
        transform.position = ankle.position + offset;
	}
}
