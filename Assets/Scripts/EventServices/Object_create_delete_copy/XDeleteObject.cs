using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XDeleteObject : MonoBehaviour {
    [SerializeField]
    GameObject DeleteObjectMenuCanvas;
    [SerializeField]
    GameObject XDelete_Cross;
	// Use this for initialization
	void Start () {
        is_vibrate = false;
        StartCoroutine("ViveVibration");
	}
    private bool is_vibrate = true;
    IEnumerator ViveVibration()
    {
        while (true)
        {
            if (is_vibrate)
            {
                RightControllerDriver.getInstance().pulseDuringTime(500);
                yield return new WaitForSecondsRealtime(0.5f);
            }
            else
            {
                yield return new WaitForFixedUpdate();
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        XEnumFunctionStatus current_function_status
            = XFunctionInfor.getInstance().getCurrentFunctionStatus();
        if (current_function_status != XEnumFunctionStatus.DELETE)
        {
            DeleteObjectMenuCanvas.SetActive(false);
            return;
        }
        else
        {
            DeleteObjectMenuCanvas.SetActive(true);
        }
        GameObject pointing_object = XRayService.getInstance().getPointingObject();
        if(pointing_object == null)
        {
            XDelete_Cross.SetActive(false);
            is_vibrate = false;
            return;
        }
        else
        {
            is_vibrate = true;
            XDelete_Cross.SetActive(true);
        }
        //HTCVIve event handlers
        if(RightControllerDriver.getInstance() != null)
        {
            //trigger
            if (RightControllerDriver.getInstance().TriggerDown())
            {
                GameObject destroy_gameObject = XGroupList.getInstance().getFatherFromGroupSon(pointing_object);
                if (destroy_gameObject == null)
                {
                    destroy_gameObject = pointing_object;
                }
                Destroy(destroy_gameObject);
                return;
            }
        }
    }
}
