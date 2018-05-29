using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerDriver : MonoBehaviour{
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device thisDevice;

    private bool is_ready;

    public void deviceInit()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
    }

    private void Awake()
    {
        deviceInit();
        is_ready = false;
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        thisDevice = SteamVR_Controller.Input((int)trackedObject.index);
        if (thisDevice != null)
        {
            is_ready = true;
        }
        else
        {
            is_ready = false;
        }
    }
    public bool isReady()
    {
        return is_ready;
    }
    public SteamVR_Controller.Device getDevice()
    {
        return thisDevice;
    }


}
