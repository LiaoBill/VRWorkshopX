using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XCopyObject : MonoBehaviour {
    [SerializeField]
    GameObject CopyObjectMenuCanvas;
    [SerializeField]
    GameObject XCopy_Copy;
    [SerializeField]
    GameObject ObjectSpawnerPoint;
    [SerializeField]
    Material XCopy_DECLARE;
    // Use this for initialization
    void Start () {
        copying_object = null;
        is_start_tracking = false;
        saving_origin_material = null;
        set_vector3_transform = default(Vector3);
    }
    private GameObject copying_object;
    private bool is_start_tracking;
    private Material saving_origin_material;
    private Vector3 set_vector3_transform;
    // Update is called once per frame
    void Update () {
        XEnumFunctionStatus current_function_status
            = XFunctionInfor.getInstance().getCurrentFunctionStatus();
        if (current_function_status != XEnumFunctionStatus.COPY)
        {
            CopyObjectMenuCanvas.SetActive(false);
            return;
        }
        else
        {
            CopyObjectMenuCanvas.SetActive(true);
        }
        GameObject pointing_object = XRayService.getInstance().getPointingObject();
        if(pointing_object == null)
        {
            XCopy_Copy.SetActive(false);
            return;
        }
        else
        {
            XCopy_Copy.SetActive(true);
        }
        
        //HTCVIve event handlers
        if (RightControllerDriver.getInstance() != null)
        {
            if (is_start_tracking)
            {
                if (copying_object != null)
                {
                    set_vector3_transform.x = ObjectSpawnerPoint.transform.position.x;
                    set_vector3_transform.y = copying_object.transform.position.y;
                    set_vector3_transform.z = ObjectSpawnerPoint.transform.position.z;
                    copying_object.transform.position = set_vector3_transform;
                }
            }
            //trigger
            if (RightControllerDriver.getInstance().TriggerDown())
            {
                if(copying_object == null)
                {
                    GameObject father_object = XGroupList.getInstance().getFatherFromGroupSon(pointing_object);
                    if (father_object != null)
                    {
                        copying_object = GameObject.Instantiate(father_object);
                    }
                    else
                    {
                        copying_object = GameObject.Instantiate(pointing_object);
                    }
                    copying_object.name = copying_object.name.Replace("(Clone)", "");
                    saving_origin_material = pointing_object.GetComponent<Renderer>().material;
                    copying_object.GetComponent<Renderer>().material = XCopy_DECLARE;
                    is_start_tracking = true;
                }
                else
                {
                    GameObject father_object = XGroupList.getInstance().getFatherFromGroupSon(pointing_object);
                    GameObject copied_gameobject;
                    if (father_object != null)
                    {
                        copied_gameobject = GameObject.Instantiate(father_object);
                    }
                    else
                    {
                        copied_gameobject = GameObject.Instantiate(pointing_object);
                    }
                    copied_gameobject.name = copied_gameobject.name.Replace("(Clone)", "");
                    set_vector3_transform.x = ObjectSpawnerPoint.transform.position.x;
                    set_vector3_transform.y = copied_gameobject.transform.position.y;
                    set_vector3_transform.z = ObjectSpawnerPoint.transform.position.z;
                    copied_gameobject.transform.position = set_vector3_transform;
                    copied_gameobject.GetComponent<Renderer>().material = saving_origin_material;
                    GameObject.Destroy(copying_object);
                    copying_object = null;
                    is_start_tracking = false;
                }
                return;
            }
        }
    }
}
