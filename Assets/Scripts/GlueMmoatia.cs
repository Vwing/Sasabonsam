using UnityEngine;
using System.Collections;

public class GlueMmoatia : MonoBehaviour {
    public Sasabonsam sasabonsam;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mmoatia"))
        {
            if(!sasabonsam.gluedMmoatia)
                sasabonsam.gluedMmoatia = other.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Mmoatia"))
        {
            if (sasabonsam.gluedMmoatia == other.transform)
                sasabonsam.gluedMmoatia = null;
        }
    }
}