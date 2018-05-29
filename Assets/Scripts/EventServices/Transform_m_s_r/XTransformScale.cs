using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XTransformScale : MonoBehaviour
{
    [SerializeField]
    GameObject TransformScaleCanvas;
    [SerializeField]
    GameObject X_SHOWTAG;
    [SerializeField]
    GameObject Y_SHOWTAG;
    [SerializeField]
    GameObject Z_SHOWTAG;
    [SerializeField]
    GameObject THUMBDOWN;
    [SerializeField]
    GameObject THUMBUP;
    [SerializeField]
    GameObject X;
    [SerializeField]
    GameObject Y;
    [SerializeField]
    GameObject Z;
    private Vector3 default_vector3;
    // Use this for initialization
    void Start()
    {
        current_focusing_gameobject = null;
        default_vector3 = default(Vector3);
        start_colliding_point = default_vector3;
        transform_scale_using_v3 = default_vector3;
    }
    private Vector3 start_colliding_point;
    private GameObject current_focusing_gameobject;
    private Vector3 transform_scale_using_v3;
    // Update is called once per frame
    void Update()
    {
        XEnumFunctionStatus current_function_status
            = XFunctionInfor.getInstance().getCurrentFunctionStatus();
        if (current_function_status != XEnumFunctionStatus.SCALE)
        {
            TransformScaleCanvas.SetActive(false);
            return;
        }
        else
        {
            TransformScaleCanvas.SetActive(true);
        }
        GameObject current_pointing_object
            = XRayService.getInstance().getPointingObject();
        //render canvas
        THUMBDOWN.SetActive(false);
        THUMBUP.SetActive(false);
        if (current_focusing_gameobject == null)
        {
            THUMBDOWN.SetActive(true);
            //show panel to exit focusing mode
        }
        else
        {
            THUMBUP.SetActive(true);
            //show trigger to enter focusing mode
        }
        X_SHOWTAG.SetActive(false);
        Y_SHOWTAG.SetActive(false);
        Z_SHOWTAG.SetActive(false);
        //render canvas for special axis
        X.SetActive(false);
        Y.SetActive(false);
        Z.SetActive(false);
        GameObject special_axis_object_out = XSpecialAxisRayService.getInstance().getPointingObject();
        if (special_axis_object_out != null)
        {
            string current_special_axis_name = special_axis_object_out.name;
            if (current_special_axis_name.IndexOf("Y_Z") >= 0)
            {
                Y.SetActive(true);
            }
            else if (current_special_axis_name.IndexOf("X_Z") >= 0)
            {
                Z.SetActive(true);
            }
            else if (current_special_axis_name.IndexOf("Y_X") >= 0)
            {
                X.SetActive(true);
            }
        }
        if (current_pointing_object == null && current_focusing_gameobject == null)
        {
            return;
        }
        if (RightControllerDriver.getInstance() != null)
        {
            //trigger
            if (RightControllerDriver.getInstance().TriggerDown())
            {
                if (current_focusing_gameobject == null)
                {
                    current_focusing_gameobject = XGroupList.getInstance().getFatherFromGroupSon(current_pointing_object);
                    if(current_focusing_gameobject == null)
                    {
                        current_focusing_gameobject = current_pointing_object;
                    }
                    //spawn axises
                    GameObject XScaleAxis_X
                         = XPreSpawnObjectManager.getInstance()
                        .getXScaleAxis_X();
                    GameObject XScaleAxis_Y
                         = XPreSpawnObjectManager.getInstance()
                        .getXScaleAxis_Y();
                    GameObject XScaleAxis_Z
                         = XPreSpawnObjectManager.getInstance()
                        .getXScaleAxis_Z();
                    XScaleAxis_X.transform.position
                        = current_focusing_gameobject.transform.position;
                    XScaleAxis_Y.transform.position
                        = current_focusing_gameobject.transform.position;
                    XScaleAxis_Z.transform.position
                        = current_focusing_gameobject.transform.position;
                    Vector3 current_focusing_object_rotation = current_focusing_gameobject.transform.rotation.eulerAngles;
                    //XScaleAxis_X.transform.rotation = Quaternion.Euler(current_focusing_object_rotation.x, current_focusing_object_rotation.y+90.0f, current_focusing_object_rotation.z);
                    //XScaleAxis_Y.transform.rotation = Quaternion.Euler(current_focusing_object_rotation.x+90.0f, current_focusing_object_rotation.y, current_focusing_object_rotation.z);
                    //XScaleAxis_Z.transform.rotation = Quaternion.Euler(current_focusing_object_rotation.x, current_focusing_object_rotation.y, current_focusing_object_rotation.z);
                    XScaleAxis_X.SetActive(true);
                    XScaleAxis_Y.SetActive(true);
                    XScaleAxis_Z.SetActive(true);
                }
                else
                {
                    GameObject special_axis_object = XSpecialAxisRayService.getInstance().getPointingObject();
                    if (special_axis_object != null)
                    {
                        string current_special_axis_name = special_axis_object.name;
                        if (current_special_axis_name.IndexOf("Y_Z") >= 0)
                        {
                            transform_scale_using_v3.x = current_focusing_gameobject.transform.localScale.x;
                            transform_scale_using_v3.z = current_focusing_gameobject.transform.localScale.z;
                            transform_scale_using_v3.y = current_focusing_gameobject.transform.localScale.y;
                            if (transform_scale_using_v3.z > transform_scale_using_v3.y)
                            {
                                transform_scale_using_v3.y = transform_scale_using_v3.z;
                            }
                            else
                            {
                                transform_scale_using_v3.z = transform_scale_using_v3.y;
                            }
                            current_focusing_gameobject.transform.localScale = transform_scale_using_v3;
                        }
                        else if (current_special_axis_name.IndexOf("X_Z") >= 0)
                        {
                            transform_scale_using_v3.x = current_focusing_gameobject.transform.localScale.x;
                            transform_scale_using_v3.z = current_focusing_gameobject.transform.localScale.z;
                            transform_scale_using_v3.y = current_focusing_gameobject.transform.localScale.y;
                            if (transform_scale_using_v3.x > transform_scale_using_v3.z)
                            {
                                transform_scale_using_v3.z = transform_scale_using_v3.x;
                            }
                            else
                            {
                                transform_scale_using_v3.x = transform_scale_using_v3.z;
                            }
                            current_focusing_gameobject.transform.localScale = transform_scale_using_v3;
                        }
                        else if (current_special_axis_name.IndexOf("Y_X") >= 0)
                        {
                            transform_scale_using_v3.x = current_focusing_gameobject.transform.localScale.x;
                            transform_scale_using_v3.z = current_focusing_gameobject.transform.localScale.z;
                            transform_scale_using_v3.y = current_focusing_gameobject.transform.localScale.y;
                            if (transform_scale_using_v3.x > transform_scale_using_v3.y)
                            {
                                transform_scale_using_v3.y = transform_scale_using_v3.x;
                            }
                            else
                            {
                                transform_scale_using_v3.x = transform_scale_using_v3.y;
                            }
                            current_focusing_gameobject.transform.localScale = transform_scale_using_v3;
                        }
                    }
                    return;
                }
            }
            if (RightControllerDriver.getInstance().Triggering())
            {
                if (current_focusing_gameobject == null)
                {
                    return;
                }
                GameObject current_pointing_axis
    = XAxisRayService.getInstance().getPointingAxisGameObject();
                string current_axis_way = "N";
                //render canvas
                if (current_pointing_axis != null)
                {
                    string x_name
                        = XPreSpawnObjectManager.getInstance()
                        .getXScaleAxis_X().name;
                    string y_name
                        = XPreSpawnObjectManager.getInstance()
                        .getXScaleAxis_Y().name;
                    string z_name
                        = XPreSpawnObjectManager.getInstance()
                        .getXScaleAxis_Z().name;
                    string current_pointing_axis_name
                        = current_pointing_axis.name;
                    if (current_pointing_axis_name.Equals(x_name))
                    {
                        //show x
                        X_SHOWTAG.SetActive(true);
                        current_axis_way = "X";
                    }
                    else if (current_pointing_axis_name.Equals(y_name))
                    {
                        //show y
                        Y_SHOWTAG.SetActive(true);
                        current_axis_way = "Y";
                    }
                    else if (current_pointing_axis_name.Equals(z_name))
                    {
                        //show z
                        Z_SHOWTAG.SetActive(true);
                        current_axis_way = "Z";
                    }
                }
                else
                {
                    //show "no axis"
                    current_axis_way = "N";
                }
                //move object and axis
                Vector3 current_pointing_axis_point;
                XAxisRayService.getInstance()
                    .getPointingPoint(out current_pointing_axis_point);
                switch (current_axis_way)
                {
                    case "N":
                        {
                            start_colliding_point = default_vector3;
                            break;
                        }
                    case "X":
                        {
                            if (start_colliding_point.Equals(default_vector3))
                            {
                                start_colliding_point = current_pointing_axis_point;
                            }
                            else
                            {
                                float x_scaled = current_pointing_axis_point.x - start_colliding_point.x;
                                //move object
                                transform_scale_using_v3.x
                                    = current_focusing_gameobject.transform.localScale.x
                                    + x_scaled;
                                transform_scale_using_v3.y
                                    = current_focusing_gameobject.transform.localScale.y;
                                transform_scale_using_v3.z
                                    = current_focusing_gameobject.transform.localScale.z;
                                current_focusing_gameobject.transform.localScale
                                    = transform_scale_using_v3;
                                start_colliding_point = current_pointing_axis_point;
                                //move axis
                                GameObject XScaleAxis_X
                                     = XPreSpawnObjectManager.getInstance()
                                    .getXScaleAxis_X();
                                GameObject XScaleAxis_Y
                                     = XPreSpawnObjectManager.getInstance()
                                    .getXScaleAxis_Y();
                                GameObject XScaleAxis_Z
                                     = XPreSpawnObjectManager.getInstance()
                                    .getXScaleAxis_Z();
                                XScaleAxis_X.transform.position
                                    = current_focusing_gameobject.transform.position;
                                XScaleAxis_Y.transform.position
                                    = current_focusing_gameobject.transform.position;
                                XScaleAxis_Z.transform.position
                                    = current_focusing_gameobject.transform.position;
                            }
                            break;
                        }
                    case "Y":
                        {
                            if (start_colliding_point.Equals(default_vector3))
                            {
                                start_colliding_point = current_pointing_axis_point;
                            }
                            else
                            {
                                float y_scaled = current_pointing_axis_point.y - start_colliding_point.y;
                                //move object
                                transform_scale_using_v3.y
                                    = current_focusing_gameobject.transform.localScale.y
                                    + y_scaled;
                                transform_scale_using_v3.x
                                    = current_focusing_gameobject.transform.localScale.x;
                                transform_scale_using_v3.z
                                    = current_focusing_gameobject.transform.localScale.z;
                                current_focusing_gameobject.transform.localScale
                                    = transform_scale_using_v3;
                                start_colliding_point = current_pointing_axis_point;
                                //move axis
                                GameObject XScaleAxis_X
                                     = XPreSpawnObjectManager.getInstance()
                                    .getXScaleAxis_X();
                                GameObject XScaleAxis_Y
                                     = XPreSpawnObjectManager.getInstance()
                                    .getXScaleAxis_Y();
                                GameObject XScaleAxis_Z
                                     = XPreSpawnObjectManager.getInstance()
                                    .getXScaleAxis_Z();
                                XScaleAxis_X.transform.position
                                    = current_focusing_gameobject.transform.position;
                                XScaleAxis_Y.transform.position
                                    = current_focusing_gameobject.transform.position;
                                XScaleAxis_Z.transform.position
                                    = current_focusing_gameobject.transform.position;
                            }
                            break;
                        }
                    case "Z":
                        {
                            if (start_colliding_point.Equals(default_vector3))
                            {
                                start_colliding_point = current_pointing_axis_point;
                            }
                            else
                            {
                                float z_scaled = current_pointing_axis_point.z - start_colliding_point.z;
                                //move object
                                transform_scale_using_v3.z
                                    = current_focusing_gameobject.transform.localScale.z
                                    + z_scaled;
                                transform_scale_using_v3.x
                                    = current_focusing_gameobject.transform.localScale.x;
                                transform_scale_using_v3.y
                                    = current_focusing_gameobject.transform.localScale.y;
                                current_focusing_gameobject.transform.localScale
                                    = transform_scale_using_v3;
                                start_colliding_point = current_pointing_axis_point;
                                //move axis
                                GameObject XScaleAxis_X
                                     = XPreSpawnObjectManager.getInstance()
                                    .getXScaleAxis_X();
                                GameObject XScaleAxis_Y
                                     = XPreSpawnObjectManager.getInstance()
                                    .getXScaleAxis_Y();
                                GameObject XScaleAxis_Z
                                     = XPreSpawnObjectManager.getInstance()
                                    .getXScaleAxis_Z();
                                XScaleAxis_X.transform.position
                                    = current_focusing_gameobject.transform.position;
                                XScaleAxis_Y.transform.position
                                    = current_focusing_gameobject.transform.position;
                                XScaleAxis_Z.transform.position
                                    = current_focusing_gameobject.transform.position;
                            }
                            break;
                        }
                }
                return;
            }
            if (RightControllerDriver.getInstance().TriggerUp())
            {
                start_colliding_point = default_vector3;
                return;
            }
            //panel
            if (RightControllerDriver.getInstance().PanelDown())
            {
                current_focusing_gameobject = null;
                //delete axis
                GameObject XScaleAxis_X
                         = XPreSpawnObjectManager.getInstance()
                        .getXScaleAxis_X();
                GameObject XScaleAxis_Y
                     = XPreSpawnObjectManager.getInstance()
                    .getXScaleAxis_Y();
                GameObject XScaleAxis_Z
                     = XPreSpawnObjectManager.getInstance()
                    .getXScaleAxis_Z();
                XScaleAxis_X.SetActive(false);
                XScaleAxis_Y.SetActive(false);
                XScaleAxis_Z.SetActive(false);
            }
        }
    }
}