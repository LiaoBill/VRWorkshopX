using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XTranformMove : MonoBehaviour {
    [SerializeField]
    GameObject TransformMoveCanvas;
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



    private Vector3 default_vector3;
	// Use this for initialization
	void Start () {
        current_focusing_gameobject = null;
        default_vector3 = default(Vector3);
        start_colliding_point = default_vector3;
        transform_move_using_v3 = default_vector3;
    }
    private Vector3 start_colliding_point;
    private GameObject current_focusing_gameobject;
    private Vector3 transform_move_using_v3;
	// Update is called once per frame
	void Update () {
        XEnumFunctionStatus current_function_status
            = XFunctionInfor.getInstance().getCurrentFunctionStatus();
        if (current_function_status != XEnumFunctionStatus.MOVE)
        {
            TransformMoveCanvas.SetActive(false);
            return;
        }
        else
        {
            TransformMoveCanvas.SetActive(true);
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
        if (current_pointing_object == null && current_focusing_gameobject == null)
        {
            return;
        }
        

        //htc vive handler event
        if (RightControllerDriver.getInstance() != null)
        {
            //trigger
            if (RightControllerDriver.getInstance().TriggerDown())
            {
                if(current_focusing_gameobject == null)
                {
                    current_focusing_gameobject = XGroupList.getInstance().getFatherFromGroupSon(current_pointing_object);
                    if (current_focusing_gameobject == null)
                    {
                        current_focusing_gameobject = current_pointing_object;
                    }
                    //spawn axises
                    GameObject XMovingAxis_X
                         = XPreSpawnObjectManager.getInstance()
                        .getXMovingAxis_X();
                    GameObject XMovingAxis_Y
                         = XPreSpawnObjectManager.getInstance()
                        .getXMovingAxis_Y();
                    GameObject XMovingAxis_Z
                         = XPreSpawnObjectManager.getInstance()
                        .getXMovingAxis_Z();
                    XMovingAxis_X.transform.position
                        = current_focusing_gameobject.transform.position;
                    XMovingAxis_Y.transform.position
                        = current_focusing_gameobject.transform.position;
                    XMovingAxis_Z.transform.position
                        = current_focusing_gameobject.transform.position;
                    XMovingAxis_X.SetActive(true);
                    XMovingAxis_Y.SetActive(true);
                    XMovingAxis_Z.SetActive(true);
                }
                return;
            }
            if (RightControllerDriver.getInstance().Triggering())
            {
                if(current_focusing_gameobject == null)
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
                        .getXMovingAxis_X().name;
                    string y_name
                        = XPreSpawnObjectManager.getInstance()
                        .getXMovingAxis_Y().name;
                    string z_name
                        = XPreSpawnObjectManager.getInstance()
                        .getXMovingAxis_Z().name;
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
                    else if(current_pointing_axis_name.Equals(z_name))
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
                                float x_moved = current_pointing_axis_point.x - start_colliding_point.x;
                                //move object
                                transform_move_using_v3.x
                                    = current_focusing_gameobject.transform.position.x
                                    + x_moved;
                                transform_move_using_v3.y
                                    = current_focusing_gameobject.transform.position.y;
                                transform_move_using_v3.z
                                    = current_focusing_gameobject.transform.position.z;
                                current_focusing_gameobject.transform.position
                                    = transform_move_using_v3;
                                start_colliding_point = current_pointing_axis_point;
                                //move axis
                                GameObject XMovingAxis_X
                                     = XPreSpawnObjectManager.getInstance()
                                    .getXMovingAxis_X();
                                GameObject XMovingAxis_Y
                                     = XPreSpawnObjectManager.getInstance()
                                    .getXMovingAxis_Y();
                                GameObject XMovingAxis_Z
                                     = XPreSpawnObjectManager.getInstance()
                                    .getXMovingAxis_Z();
                                XMovingAxis_X.transform.position 
                                    = current_focusing_gameobject.transform.position;
                                XMovingAxis_Y.transform.position
                                    = current_focusing_gameobject.transform.position;
                                XMovingAxis_Z.transform.position
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
                                float y_moved = current_pointing_axis_point.y - start_colliding_point.y;
                                //move object
                                transform_move_using_v3.y
                                    = current_focusing_gameobject.transform.position.y
                                    + y_moved;
                                transform_move_using_v3.x
                                    = current_focusing_gameobject.transform.position.x;
                                transform_move_using_v3.z
                                    = current_focusing_gameobject.transform.position.z;
                                current_focusing_gameobject.transform.position
                                    = transform_move_using_v3;
                                start_colliding_point = current_pointing_axis_point;
                                //move axis
                                GameObject XMovingAxis_X
                                     = XPreSpawnObjectManager.getInstance()
                                    .getXMovingAxis_X();
                                GameObject XMovingAxis_Y
                                     = XPreSpawnObjectManager.getInstance()
                                    .getXMovingAxis_Y();
                                GameObject XMovingAxis_Z
                                     = XPreSpawnObjectManager.getInstance()
                                    .getXMovingAxis_Z();
                                XMovingAxis_X.transform.position
                                    = current_focusing_gameobject.transform.position;
                                XMovingAxis_Y.transform.position
                                    = current_focusing_gameobject.transform.position;
                                XMovingAxis_Z.transform.position
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
                                float z_moved = current_pointing_axis_point.z - start_colliding_point.z;
                                //move object
                                transform_move_using_v3.z
                                    = current_focusing_gameobject.transform.position.z
                                    + z_moved;
                                transform_move_using_v3.x
                                    = current_focusing_gameobject.transform.position.x;
                                transform_move_using_v3.y
                                    = current_focusing_gameobject.transform.position.y;
                                current_focusing_gameobject.transform.position
                                    = transform_move_using_v3;
                                start_colliding_point = current_pointing_axis_point;
                                //move axis
                                GameObject XMovingAxis_X
                                     = XPreSpawnObjectManager.getInstance()
                                    .getXMovingAxis_X();
                                GameObject XMovingAxis_Y
                                     = XPreSpawnObjectManager.getInstance()
                                    .getXMovingAxis_Y();
                                GameObject XMovingAxis_Z
                                     = XPreSpawnObjectManager.getInstance()
                                    .getXMovingAxis_Z();
                                XMovingAxis_X.transform.position
                                    = current_focusing_gameobject.transform.position;
                                XMovingAxis_Y.transform.position
                                    = current_focusing_gameobject.transform.position;
                                XMovingAxis_Z.transform.position
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
                GameObject XMovingAxis_X
                         = XPreSpawnObjectManager.getInstance()
                        .getXMovingAxis_X();
                GameObject XMovingAxis_Y
                     = XPreSpawnObjectManager.getInstance()
                    .getXMovingAxis_Y();
                GameObject XMovingAxis_Z
                     = XPreSpawnObjectManager.getInstance()
                    .getXMovingAxis_Z();
                XMovingAxis_X.SetActive(false);
                XMovingAxis_Y.SetActive(false);
                XMovingAxis_Z.SetActive(false);
            }
        }

    }
}
