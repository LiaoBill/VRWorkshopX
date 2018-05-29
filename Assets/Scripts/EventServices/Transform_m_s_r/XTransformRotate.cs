using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XTransformRotate : MonoBehaviour {
    [SerializeField]
    GameObject TransformRotateCanvas;
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
    GameObject LEFT;
    [SerializeField]
    GameObject RIGHT;
    [SerializeField]
    GameObject BACK;
    private Vector3 default_vector3;
    // Use this for initialization
    void Start () {
        current_focusing_gameobject = null;
        default_vector3 = default(Vector3);
        start_colliding_point = default_vector3;
        transform_rotate_using_v3 = default_vector3;
    }
    private Vector3 start_colliding_point;
    private GameObject current_focusing_gameobject;
    private Vector3 transform_rotate_using_v3;
    // Update is called once per frame
    void Update () {
        XEnumFunctionStatus current_function_status
            = XFunctionInfor.getInstance().getCurrentFunctionStatus();
        if (current_function_status != XEnumFunctionStatus.ROTATE)
        {
            TransformRotateCanvas.SetActive(false);
            return;
        }
        else
        {
            TransformRotateCanvas.SetActive(true);
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
        LEFT.SetActive(false);
        RIGHT.SetActive(false);
        BACK.SetActive(false);
        GameObject special_axis_object_out = XSpecialAxisRayService.getInstance().getPointingObject();
        if (special_axis_object_out != null)
        {
            string current_special_axis_name = special_axis_object_out.name;
            if (current_special_axis_name.IndexOf("XLEFT") >= 0)
            {
                LEFT.GetComponent<SpriteRenderer>().color = Color.red;
                LEFT.SetActive(true);
            }
            else if (current_special_axis_name.IndexOf("XRIGHT") >= 0)
            {
                RIGHT.GetComponent<SpriteRenderer>().color = Color.red;
                RIGHT.SetActive(true);
            }
            else if (current_special_axis_name.IndexOf("XBACK") >= 0)
            {
                BACK.GetComponent<SpriteRenderer>().color = Color.red;
                BACK.SetActive(true);
            }
            else if (current_special_axis_name.IndexOf("YLEFT") >= 0)
            {
                LEFT.GetComponent<SpriteRenderer>().color = Color.green;
                LEFT.SetActive(true);
            }
            else if (current_special_axis_name.IndexOf("YRIGHT") >= 0)
            {
                RIGHT.GetComponent<SpriteRenderer>().color = Color.green;
                RIGHT.SetActive(true);
            }
            else if (current_special_axis_name.IndexOf("YBACK") >= 0)
            {
                BACK.GetComponent<SpriteRenderer>().color = Color.green;
                BACK.SetActive(true);
            }
            else if (current_special_axis_name.IndexOf("ZLEFT") >= 0)
            {
                LEFT.GetComponent<SpriteRenderer>().color = Color.blue;
                LEFT.SetActive(true);
            }
            else if (current_special_axis_name.IndexOf("ZRIGHT") >= 0)
            {
                RIGHT.GetComponent<SpriteRenderer>().color = Color.blue;
                RIGHT.SetActive(true);
            }
            else if (current_special_axis_name.IndexOf("ZBACK") >= 0)
            {
                BACK.GetComponent<SpriteRenderer>().color = Color.blue;
                BACK.SetActive(true);
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
                    if (current_focusing_gameobject == null)
                    {
                        current_focusing_gameobject = current_pointing_object;
                    }
                    //spawn axises
                    GameObject XRotateAxis_X
                         = XPreSpawnObjectManager.getInstance()
                        .getXRotateAxis_X();
                    GameObject XRotateAxis_Y
                         = XPreSpawnObjectManager.getInstance()
                        .getXRotateAxis_Y();
                    GameObject XRotateAxis_Z
                         = XPreSpawnObjectManager.getInstance()
                        .getXRotateAxis_Z();
                    //set position
                    XRotateAxis_X.transform.position
                        = current_focusing_gameobject.transform.position;
                    XRotateAxis_Y.transform.position
                        = current_focusing_gameobject.transform.position;
                    XRotateAxis_Z.transform.position
                        = current_focusing_gameobject.transform.position;
                    //set scale
                    float scale_factor_x = current_focusing_gameobject.transform.localScale.x / 0.1f;
                    float scale_factor_y = current_focusing_gameobject.transform.localScale.y / 0.1f;
                    float scale_factor_z = current_focusing_gameobject.transform.localScale.z / 0.1f;
                    if (scale_factor_x > scale_factor_y)
                    {
                        if (scale_factor_x > scale_factor_z)
                        {
                            //x
                            transform_rotate_using_v3.x = current_focusing_gameobject.transform.localScale.x / 0.1f;
                            transform_rotate_using_v3.y = transform_rotate_using_v3.x / 10.0f;
                            transform_rotate_using_v3.z = transform_rotate_using_v3.x;
                            XRotateAxis_X.transform.localScale
                                = transform_rotate_using_v3;
                            XRotateAxis_Y.transform.localScale
                                = transform_rotate_using_v3;
                            XRotateAxis_Z.transform.localScale
                                = transform_rotate_using_v3;
                        }
                        else
                        {
                            //z
                            transform_rotate_using_v3.z = current_focusing_gameobject.transform.localScale.z / 0.1f;
                            transform_rotate_using_v3.x = transform_rotate_using_v3.z;
                            transform_rotate_using_v3.y = transform_rotate_using_v3.z / 10.0f;
                            XRotateAxis_X.transform.localScale
                                = transform_rotate_using_v3;
                            XRotateAxis_Y.transform.localScale
                                = transform_rotate_using_v3;
                            XRotateAxis_Z.transform.localScale
                                = transform_rotate_using_v3;
                        }
                    }
                    else
                    {
                        if (scale_factor_y > scale_factor_z)
                        {
                            //y
                            transform_rotate_using_v3.y = current_focusing_gameobject.transform.localScale.y / 0.1f;
                            transform_rotate_using_v3.x = transform_rotate_using_v3.y;
                            transform_rotate_using_v3.z = transform_rotate_using_v3.y;
                            transform_rotate_using_v3.y /= 10.0f;
                            XRotateAxis_X.transform.localScale
                                = transform_rotate_using_v3;
                            XRotateAxis_Y.transform.localScale
                                = transform_rotate_using_v3;
                            XRotateAxis_Z.transform.localScale
                                = transform_rotate_using_v3;
                        }
                        else
                        {
                            //z
                            transform_rotate_using_v3.z = current_focusing_gameobject.transform.localScale.z / 0.1f;
                            transform_rotate_using_v3.x = transform_rotate_using_v3.z;
                            transform_rotate_using_v3.y = transform_rotate_using_v3.z / 10.0f;
                            XRotateAxis_X.transform.localScale
                                = transform_rotate_using_v3;
                            XRotateAxis_Y.transform.localScale
                                = transform_rotate_using_v3;
                            XRotateAxis_Z.transform.localScale
                                = transform_rotate_using_v3;
                        }
                    }
                    XRotateAxis_X.SetActive(true);
                    XRotateAxis_Y.SetActive(true);
                    XRotateAxis_Z.SetActive(true);
                }
                else
                {
                    GameObject special_axis_object = XSpecialAxisRayService.getInstance().getPointingObject();
                    if(special_axis_object != null)
                    {
                        string current_special_axis_name = special_axis_object.name;
                        if (current_special_axis_name.IndexOf("XLEFT")>=0)
                        {
                            transform_rotate_using_v3.x = 0;
                            transform_rotate_using_v3.z = 0;
                            transform_rotate_using_v3.y = 45;
                            current_focusing_gameobject.transform.Rotate(transform_rotate_using_v3, Space.World);
                        }
                        else if(current_special_axis_name.IndexOf("XRIGHT") >= 0)
                        {
                            transform_rotate_using_v3.x = 0;
                            transform_rotate_using_v3.z = 0;
                            transform_rotate_using_v3.y = -45;
                            current_focusing_gameobject.transform.Rotate(transform_rotate_using_v3, Space.World);
                        }
                        else if (current_special_axis_name.IndexOf("XBACK") >= 0)
                        {
                            transform_rotate_using_v3.x = 0;
                            transform_rotate_using_v3.y = 0;
                            transform_rotate_using_v3.z = 0;
                            current_focusing_gameobject.transform.rotation = Quaternion.Euler(transform_rotate_using_v3);
                        }
                        else if (current_special_axis_name.IndexOf("YLEFT") >= 0)
                        {
                            transform_rotate_using_v3.x = 0;
                            transform_rotate_using_v3.z = 45;
                            transform_rotate_using_v3.y = 0;
                            current_focusing_gameobject.transform.Rotate(transform_rotate_using_v3, Space.World);
                        }
                        else if (current_special_axis_name.IndexOf("YRIGHT") >= 0)
                        {
                            transform_rotate_using_v3.x = 0;
                            transform_rotate_using_v3.z = -45;
                            transform_rotate_using_v3.y = 0;
                            current_focusing_gameobject.transform.Rotate(transform_rotate_using_v3, Space.World);
                        }
                        else if (current_special_axis_name.IndexOf("YBACK") >= 0)
                        {
                            transform_rotate_using_v3.x = 0;
                            transform_rotate_using_v3.y = 0;
                            transform_rotate_using_v3.z = 0;
                            current_focusing_gameobject.transform.rotation = Quaternion.Euler(transform_rotate_using_v3);
                        }
                        else if (current_special_axis_name.IndexOf("ZLEFT") >= 0)
                        {
                            transform_rotate_using_v3.x = 45;
                            transform_rotate_using_v3.z = 0;
                            transform_rotate_using_v3.y = 0;
                            current_focusing_gameobject.transform.Rotate(transform_rotate_using_v3, Space.World);
                        }
                        else if (current_special_axis_name.IndexOf("ZRIGHT") >= 0)
                        {
                            transform_rotate_using_v3.x = -45;
                            transform_rotate_using_v3.z = 0;
                            transform_rotate_using_v3.y = 0;
                            current_focusing_gameobject.transform.Rotate(transform_rotate_using_v3, Space.World);
                        }
                        else if (current_special_axis_name.IndexOf("ZBACK") >= 0)
                        {
                            transform_rotate_using_v3.x = 0;
                            transform_rotate_using_v3.y = 0;
                            transform_rotate_using_v3.z = 0;
                            current_focusing_gameobject.transform.rotation = Quaternion.Euler(transform_rotate_using_v3);
                        }
                    }
                }
                return;
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
                        .getXRotateAxis_X().name;
                    string y_name
                        = XPreSpawnObjectManager.getInstance()
                        .getXRotateAxis_Y().name;
                    string z_name
                        = XPreSpawnObjectManager.getInstance()
                        .getXRotateAxis_Z().name;
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
                GameObject XRotateAxis_X
                    = XPreSpawnObjectManager.getInstance()
                .getXRotateAxis_X();
                GameObject XRotateAxis_Y
                     = XPreSpawnObjectManager.getInstance()
                    .getXRotateAxis_Y();
                GameObject XRotateAxis_Z
                     = XPreSpawnObjectManager.getInstance()
                    .getXRotateAxis_Z();
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
                                float x_rotated = Vector3.Distance(current_pointing_axis_point, start_colliding_point);
                                x_rotated *= 1.1f;
                                //rotate object
                                Vector3 starter_vector = start_colliding_point - current_focusing_gameobject.transform.position;
                                Vector3 ender_vector = current_pointing_axis_point - current_focusing_gameobject.transform.position;
                                Vector3 cross_result = Vector3.Cross(starter_vector, ender_vector);
                                transform_rotate_using_v3.x = 0;
                                transform_rotate_using_v3.z = 0;
                                if (cross_result.y > 0)
                                {
                                    transform_rotate_using_v3.y = x_rotated;
                                    current_focusing_gameobject.transform.Rotate(transform_rotate_using_v3,Space.World);
                                }
                                else
                                {
                                    transform_rotate_using_v3.y = -x_rotated;
                                    current_focusing_gameobject.transform.Rotate(transform_rotate_using_v3, Space.World);
                                }
                                //move axis
                                
                                XRotateAxis_X.transform.position
                                    = current_focusing_gameobject.transform.position;
                                XRotateAxis_Y.transform.position
                                    = current_focusing_gameobject.transform.position;
                                XRotateAxis_Z.transform.position
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
                                float y_rotated = Vector3.Distance(current_pointing_axis_point, start_colliding_point);
                                y_rotated *= 1.1f;
                                //rotate object
                                Vector3 starter_vector = start_colliding_point - current_focusing_gameobject.transform.position;
                                Vector3 ender_vector = current_pointing_axis_point - current_focusing_gameobject.transform.position;
                                Vector3 cross_result = Vector3.Cross(starter_vector, ender_vector);
                                transform_rotate_using_v3.x = 0;
                                transform_rotate_using_v3.y = 0;
                                if (cross_result.z > 0)
                                {
                                    transform_rotate_using_v3.z = y_rotated;
                                    current_focusing_gameobject.transform.Rotate(transform_rotate_using_v3, Space.World);
                                }
                                else
                                {
                                    transform_rotate_using_v3.z = -y_rotated;
                                    current_focusing_gameobject.transform.Rotate(transform_rotate_using_v3, Space.World);
                                }
                                //move axis
                       
                                XRotateAxis_X.transform.position
                                    = current_focusing_gameobject.transform.position;
                                XRotateAxis_Y.transform.position
                                    = current_focusing_gameobject.transform.position;
                                XRotateAxis_Z.transform.position
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
                                float z_rotated = Vector3.Distance(current_pointing_axis_point, start_colliding_point);
                                z_rotated *= 1.1f;
                                //rotate object
                                Vector3 starter_vector = start_colliding_point - current_focusing_gameobject.transform.position;
                                Vector3 ender_vector = current_pointing_axis_point - current_focusing_gameobject.transform.position;
                                Vector3 cross_result = Vector3.Cross(starter_vector, ender_vector);
                                transform_rotate_using_v3.z = 0;
                                transform_rotate_using_v3.y = 0;
                                if (cross_result.x > 0)
                                {
                                    transform_rotate_using_v3.x = z_rotated;
                                    current_focusing_gameobject.transform.Rotate(transform_rotate_using_v3, Space.World);
                                }
                                else
                                {
                                    transform_rotate_using_v3.x = -z_rotated;
                                    current_focusing_gameobject.transform.Rotate(transform_rotate_using_v3, Space.World);
                                }
                                //move axis
                               
                                XRotateAxis_X.transform.position
                                    = current_focusing_gameobject.transform.position;
                                XRotateAxis_Y.transform.position
                                    = current_focusing_gameobject.transform.position;
                                XRotateAxis_Z.transform.position
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
                GameObject XRotateAxis_X
                         = XPreSpawnObjectManager.getInstance()
                        .getXRotateAxis_X();
                GameObject XRotateAxis_Y
                     = XPreSpawnObjectManager.getInstance()
                    .getXRotateAxis_Y();
                GameObject XRotateAxis_Z
                     = XPreSpawnObjectManager.getInstance()
                    .getXRotateAxis_Z();
                XRotateAxis_X.SetActive(false);
                XRotateAxis_Y.SetActive(false);
                XRotateAxis_Z.SetActive(false);
            }
        }
    }
}
