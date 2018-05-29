using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightControllerDriver : MonoBehaviour {

    private static RightControllerDriver thisInstance;

    private void Awake()
    {
        thisInstance = this;
    }

    public static RightControllerDriver getInstance()
    {
        return thisInstance;
    }

    public bool AppMenuDown()
    {
        if (!GetComponent<ControllerDriver>().isReady())
        {
            return false;
        }
        if (GetComponent<ControllerDriver>().getDevice().GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool SystemDown()
    {
        if (!GetComponent<ControllerDriver>().isReady())
        {
            return false;
        }
        if (GetComponent<ControllerDriver>().getDevice().GetPressDown(SteamVR_Controller.ButtonMask.System))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool PanelUp()
    {
        if (!GetComponent<ControllerDriver>().isReady())
        {
            return false;
        }
        if (GetComponent<ControllerDriver>().getDevice().GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool PanelDown()
    {
        if (!GetComponent<ControllerDriver>().isReady())
        {
            return false;
        }
        if (GetComponent<ControllerDriver>().getDevice().GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public bool Paneling()
    {
        if (!GetComponent<ControllerDriver>().isReady())
        {
            return false;
        }
        if (GetComponent<ControllerDriver>().getDevice().GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public float PanelTouchingAngle()
    {
        if (!GetComponent<ControllerDriver>().isReady())
        {
            return 720.0f;
        }
        var device = GetComponent<ControllerDriver>().getDevice();
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Vector2 temp = device.GetAxis();
            float angle = getVector2Angle(new Vector2(1, 0), temp);
            return angle;
        }
        else
        {
            return 720.0f;
        }
    }

    public bool PanelUpTouching()
    {
        if (!GetComponent<ControllerDriver>().isReady())
        {
            return false;
        }
        var device = GetComponent<ControllerDriver>().getDevice();
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Vector2 temp = device.GetAxis();
            float angle = getVector2Angle(new Vector2(1, 0), temp);
            if (angle < -45 && angle > -135)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public bool PanelLeftTouching()
    {
        if (!GetComponent<ControllerDriver>().isReady())
        {
            return false;
        }
        var device = GetComponent<ControllerDriver>().getDevice();
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Vector2 temp = device.GetAxis();
            float angle = getVector2Angle(new Vector2(1, 0), temp);
            if ((angle < 180 && angle > 135) || (angle < -135 && angle > -180))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public bool PanelRightTouching()
    {
        if (!GetComponent<ControllerDriver>().isReady())
        {
            return false;
        }
        var device = GetComponent<ControllerDriver>().getDevice();
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Vector2 temp = device.GetAxis();
            float angle = getVector2Angle(new Vector2(1, 0), temp);
            if ((angle > 0 && angle < 45) || (angle > -45 && angle < 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public bool PanelDownTouching()
    {
        if (!GetComponent<ControllerDriver>().isReady())
        {
            return false;
        }
        var device = GetComponent<ControllerDriver>().getDevice();
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Vector2 temp = device.GetAxis();
            float angle = getVector2Angle(new Vector2(1, 0), temp);
            if (angle > 45 && angle < 135)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public bool PanelUpDown()
    {
        if (!GetComponent<ControllerDriver>().isReady())
        {
            return false;
        }
        var device = GetComponent<ControllerDriver>().getDevice();
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Vector2 temp = device.GetAxis();
            float angle = getVector2Angle(new Vector2(1, 0), temp);
            if (angle < -45 && angle > -135)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public bool PanelLeftDown()
    {
        if (!GetComponent<ControllerDriver>().isReady())
        {
            return false;
        }
        var device = GetComponent<ControllerDriver>().getDevice();
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Vector2 temp = device.GetAxis();
            float angle = getVector2Angle(new Vector2(1, 0), temp);
            if ((angle < 180 && angle > 135) || (angle < -135 && angle > -180))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public bool PanelRightDown()
    {
        if (!GetComponent<ControllerDriver>().isReady())
        {
            return false;
        }
        var device = GetComponent<ControllerDriver>().getDevice();
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Vector2 temp = device.GetAxis();
            float angle = getVector2Angle(new Vector2(1, 0), temp);
            if ((angle > 0 && angle < 45) || (angle > -45 && angle < 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public bool PanelDownDown()
    {
        if (!GetComponent<ControllerDriver>().isReady())
        {
            return false;
        }
        var device = GetComponent<ControllerDriver>().getDevice();
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Vector2 temp = device.GetAxis();
            float angle = getVector2Angle(new Vector2(1, 0), temp);
            if (angle > 45 && angle < 135)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public bool TriggerDown()
    {
        if (!GetComponent<ControllerDriver>().isReady())
        {
            return false;
        }
        var device = GetComponent<ControllerDriver>().getDevice();
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool TriggerUp()
    {
        if (!GetComponent<ControllerDriver>().isReady())
        {
            return false;
        }
        var device = GetComponent<ControllerDriver>().getDevice();
        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool Triggering()
    {
        if (!GetComponent<ControllerDriver>().isReady())
        {
            return false;
        }
        var device = GetComponent<ControllerDriver>().getDevice();
        if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool Griping()
    {
        if (!GetComponent<ControllerDriver>().isReady())
        {
            return false;
        }
        var device = GetComponent<ControllerDriver>().getDevice();
        if (device.GetPress(SteamVR_Controller.ButtonMask.Grip))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool GripDown()
    {
        if (!GetComponent<ControllerDriver>().isReady())
        {
            return false;
        }
        var device = GetComponent<ControllerDriver>().getDevice();
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void pulseDuringTime(ushort time)
    {
        if (!GetComponent<ControllerDriver>().isReady())
        {
            return;
        }
        var device = GetComponent<ControllerDriver>().getDevice();
        device.TriggerHapticPulse(time);
    }

    private float getVector2Angle(Vector2 from, Vector2 to)
    {
        float angle;
        Vector3 cross = Vector3.Cross(from, to);
        angle = Vector2.Angle(from, to);
        return cross.z > 0 ? -angle : angle;
    }
}
