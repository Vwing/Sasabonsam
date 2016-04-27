using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [Tooltip("How often should we spawn things?")]
    public float spawnInterval = 3f;
    private SpawnNode[] spawnNodes;
    private float timer = 0f;
	// Use this for initialization
	void Start ()
	{
	    spawnNodes = GetComponentsInChildren<SpawnNode>();
	}
	
    void UpdateTimers()
    {
        timer += Time.deltaTime;
        if (timer > spawnInterval)
            timer = 0f;
    }

    void SpawnStuff()
    {
        if (timer != 0)
            return;
        int i = Random.Range(0, spawnNodes.Length);
        spawnNodes[i].Spawn();
    }

	// Update is called once per frame
	void Update ()
    {
        UpdateTimers();
        SpawnStuff();
	}
}
