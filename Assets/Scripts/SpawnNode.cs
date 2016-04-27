using UnityEngine;
using System.Collections;

public class SpawnNode : MonoBehaviour
{
    public GameObject mySpawn;
    private Quaternion rot;

	// Use this for initialization
	void Start ()
	{
	    transform.LookAt(Vector3.zero);
	}

    public void Spawn()
    {
        Instantiate(mySpawn, transform.position, Quaternion.identity);
    }
}
