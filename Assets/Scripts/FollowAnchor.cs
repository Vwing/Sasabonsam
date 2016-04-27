using UnityEngine;
using System.Collections;
using Valve.VR;

public class FollowAnchor : MonoBehaviour
{
    public Transform anchor;
    Rigidbody rb;
    GameObject model;
    Vector3 lastPos;
    Quaternion lastRot;
    Vector3 myVelocity;
    float magnitude;
    Vector3 axis;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        model = anchor.parent.Find("Model").gameObject;
        lastPos = transform.position;
        lastRot = transform.rotation;
    }
    void LateUpdate()
    {
        myVelocity = (transform.position - lastPos) / Time.deltaTime;
        lastPos = transform.position;
        Quaternion rotationDelta = transform.rotation * Quaternion.Inverse(lastRot);
        rotationDelta.ToAngleAxis(out magnitude, out axis);
        lastRot = transform.rotation;
    }
    bool wasPressed = false;
    void Update()
    {
        if (SteamVRInputHandler.LeftIsPressed)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            transform.position = anchor.position;
            transform.rotation = anchor.rotation;
            model.SetActive(false);
            wasPressed = true;
        }
        else if(wasPressed)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.velocity = myVelocity;
            rb.angularVelocity = axis * magnitude / Time.deltaTime;
            model.SetActive(true);
            wasPressed = false;
        }
    }
}
