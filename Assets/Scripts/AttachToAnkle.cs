using UnityEngine;
using System.Collections;

public class AttachToAnkle : MonoBehaviour
{
    public Transform ankle;
    public Sasabonsam sasabonsam;
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

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Weapon"))
        {
            ContactPoint[] cps = other.contacts;
            Vector3 avg = Vector3.zero;
            for (int i = 0; i < cps.Length; ++i)
                avg += cps[i].point;
            avg /= cps.Length;
            sasabonsam.Hit(avg);
        }
    }
}
