using UnityEngine;
using System.Collections;

public class AnchorLeg : MonoBehaviour
{
    public Sasabonsam sasabonsam;
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
