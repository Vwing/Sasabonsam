using UnityEngine;
using System.Collections;
using Valve.VR;
using System;

public class ResizeToPlayArea : MonoBehaviour
{
    SteamVR_PlayArea playArea;
    Vector3 scale;

	void Start ()
    {
        playArea = transform.parent.GetComponent<SteamVR_PlayArea>();
        Vector3[] vertices = playArea.vertices;
        float x = Math.Abs(vertices[0].x * 2);
        float y = transform.localScale.y;
        float z = Math.Abs(vertices[0].z * 2);
        transform.localScale = new Vector3(x/10,y,z/10);
    }
}