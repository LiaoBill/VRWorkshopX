using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XMaterial2DGiving : MonoBehaviour {
    [SerializeField]
    GameObject Material2DCanvas;
    [SerializeField]
    Material WoodMaterial;
    [SerializeField]
    Material RockMaterial;
    [SerializeField]
    Material XCreate_;
    [SerializeField]
    GameObject Diamond;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        XEnumFunctionStatus current_function_status
            = XFunctionInfor.getInstance().getCurrentFunctionStatus();
        if (current_function_status != XEnumFunctionStatus.MATERIAL2D)
        {
            Material2DCanvas.SetActive(false);
            return;
        }
        else
        {
            Material2DCanvas.SetActive(true);
        }
        GameObject current_pointing_object = XRayService.getInstance().getPointingObject();
        if (current_pointing_object == null)
        {
            Diamond.SetActive(false);
            return;
        }
        else
        {
            Diamond.SetActive(true);
        }

        if (RightControllerDriver.getInstance() != null)
        {
            if (RightControllerDriver.getInstance().TriggerDown())
            {
                //wood
                current_pointing_object.GetComponent<Renderer>().material = WoodMaterial;
                return;
            }
            if (RightControllerDriver.getInstance().PanelDown())
            {
                //rock
                current_pointing_object.GetComponent<Renderer>().material = RockMaterial;
                return;
            }
            if (RightControllerDriver.getInstance().GripDown())
            {
                current_pointing_object.GetComponent<Renderer>().material = XCreate_;
                return;
            }
        }
    }
}
