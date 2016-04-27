using UnityEngine;
using System.Collections;

public class DestroyAfterSeconds : MonoBehaviour {
    public float seconds = 3f;
	// Use this for initialization
	void Start () {
	
	}
    float timer = 0f;
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > seconds)
            Destroy(this.gameObject);
	}
}
