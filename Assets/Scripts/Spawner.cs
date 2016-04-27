using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [Tooltip("What are we spawning?")]
    public GameObject toSpawn;
    [Tooltip("How often should we spawn things?")]
    public float spawnInterval = 3f;

   // [Tooltip("How many should spawn at once?")] public float spawnNum;
    private GameObject[] spawnPool;
    private SpawnNode[] spawnNodes;
    private float timer = 0f;
    private int poolIndex = 0;

	void Start ()
	{
	    spawnNodes = GetComponentsInChildren<SpawnNode>();
        // allocates enough for 3 times the number of spawnpoints.
        // may want to make this more dynamic later.
        spawnPool = new GameObject[spawnNodes.Length * 3];
	    for (int i = 0; i < spawnPool.Length; ++i)
	    {
	        spawnPool[i] = (GameObject) Instantiate(toSpawn, Vector3.zero, Quaternion.identity);
	        spawnPool[i].transform.parent = transform;
            spawnPool[i].SetActive(false);
	    }
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
        spawnNodes[i].Spawn(spawnPool[poolIndex++]);
        if (poolIndex >= spawnPool.Length)
            poolIndex = 0;
    }

	// Update is called once per frame
	void Update ()
    {
        UpdateTimers();
        SpawnStuff();
	}
}
