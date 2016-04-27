using UnityEngine;
using System.Collections;

public class SpawnNode : MonoBehaviour
{
    public void Spawn(GameObject spawn)
    {
        spawn.transform.position = transform.position;
        spawn.transform.rotation = transform.rotation;
        spawn.SetActive(true);
    }
}
