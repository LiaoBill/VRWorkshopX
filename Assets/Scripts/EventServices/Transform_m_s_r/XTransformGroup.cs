using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XTransformGroup : MonoBehaviour {
    [SerializeField]
    GameObject TransformGroupCanvas;
    [SerializeField]
    GameObject GROUP_COUNT;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        XEnumFunctionStatus current_function_status
            = XFunctionInfor.getInstance().getCurrentFunctionStatus();
        if (current_function_status != XEnumFunctionStatus.GROUP)
        {
            TransformGroupCanvas.SetActive(false);
            return;
        }
        else
        {
            TransformGroupCanvas.SetActive(true);
        }
        if(RightControllerDriver.getInstance() != null)
        {
            if (RightControllerDriver.getInstance().GripDown())
            {
                XGroupList.getInstance().dismissList();
            }
        }
        GROUP_COUNT.GetComponent<Text>().text = "OBJECT COUNT\n:\n[" + XGroupList.getInstance().getListCount() + "]";
        GameObject current_pointing_object = XRayService.getInstance().getPointingObject();
        if (current_pointing_object == null)
        {
            return;
        }
        //htc handler
        if (RightControllerDriver.getInstance() != null)
        {
            if (RightControllerDriver.getInstance().TriggerDown())
            {
                XGroupList.getInstance().add(current_pointing_object);
            }
            if (RightControllerDriver.getInstance().Triggering())
            {
                XGroupList.getInstance().add(current_pointing_object);
            }
            if (RightControllerDriver.getInstance().PanelDown())
            {
                XGroupList.getInstance().deleteGameObject(current_pointing_object);
            }
            if (RightControllerDriver.getInstance().GripDown())
            {
                XGroupList.getInstance().dismissList();
            }
        }
    }
}
