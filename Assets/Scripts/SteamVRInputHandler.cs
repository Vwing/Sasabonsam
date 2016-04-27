using UnityEngine;
using Valve.VR;

public class SteamVRInputHandler : MonoBehaviour
{
    public static bool LeftIsPressed;
    public static bool RightIsPressed;
    SteamVR vr;

    uint left = 0;
    uint right = 0;


    public void OnEnable()
    {
        SteamVR_Utils.Event.Listen("device_connected", OnDeviceConnected);
    }
    public void OnDisable()
    {
        SteamVR_Utils.Event.Remove("device_connected", OnDeviceConnected);
    }

    private void OnDeviceConnected(params object[] args)
    {
        var i = (int)args[0];
        var connected = (bool)args[1];

        vr = SteamVR.instance;
        var isController = (vr.hmd.GetTrackedDeviceClass((uint)i) == ETrackedDeviceClass.Controller);
        if (isController)
        {
            if (left == 0)
            {
                left = (uint)i;
                Debug.Log("Left connected = " + i);
            }
            else if (right == 0)
            {
                right = (uint)i;
                Debug.Log("Right connected = " + i);
            }
        }
    }

    public void Update()
    {
        vr = SteamVR.instance;

        LeftIsPressed = GetIsPressed(vr, left);
        RightIsPressed = GetIsPressed(vr, right);
    }


    bool GetIsPressed(SteamVR vr, uint controller)
    {
        if (controller == 0)
            return false;
        var state = new VRControllerState_t();
        var success = vr.hmd.GetControllerState(controller, ref state);
        return (state.ulButtonPressed & SteamVR_Controller.ButtonMask.Trigger) != 0;
    }
}