using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XArrayObject : MonoBehaviour {
    [SerializeField]
    GameObject ObjectArrayCanvas;
    [SerializeField]
    GameObject WANT_COUNT;
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
    Material DECLARATION_MATERIAL;
    [SerializeField]
    GameObject X;
    [SerializeField]
    GameObject Y;
    [SerializeField]
    GameObject Z;
    private Vector3 default_vector3;
    // Use this for initialization
    void Start () {
        current_focusing_gameobject = null;
        default_vector3 = default(Vector3);
        transform_array_using_v3 = default_vector3;
        array_object_want_count = 1;
        pre_spawn_declaringobject_list = new List<GameObject>();
        pre_spawn_declaringobject_list_back = new List<GameObject>();
    }
    private GameObject current_focusing_gameobject;
    private Vector3 transform_array_using_v3;
    private int array_object_want_count;
    private Material array_saving_origin_material;
    private List<GameObject> pre_spawn_declaringobject_list;
    private List<GameObject> pre_spawn_declaringobject_list_back;
    private GameObject middle_declaringobject;
    // Update is called once per frame
    void Update()
    {
        XEnumFunctionStatus current_function_status
            = XFunctionInfor.getInstance().getCurrentFunctionStatus();
        if (current_function_status != XEnumFunctionStatus.ARRAY)
        {
            ObjectArrayCanvas.SetActive(false);
            return;
        }
        else
        {
            ObjectArrayCanvas.SetActive(true);
        }
        //render canvas
        WANT_COUNT.GetComponent<Text>().text = "COPY COUNT\n:\n[" + array_object_want_count +"]";
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
        X.SetActive(false);
        Y.SetActive(false);
        Z.SetActive(false);
        GameObject current_pointing_object
            = XRayService.getInstance().getPointingObject();
        //render objects
        if(current_focusing_gameobject != null)
        {
            GameObject current_pointing_axis
    = XAxisRayService.getInstance().getPointingAxisGameObject();
            string current_axis_way = "N";
            //render canvas
            if (current_pointing_axis != null)
            {
                string x_name
                    = XPreSpawnObjectManager.getInstance()
                    .getXArrayAxis_X().name;
                string y_name
                    = XPreSpawnObjectManager.getInstance()
                    .getXArrayAxis_Y().name;
                string z_name
                    = XPreSpawnObjectManager.getInstance()
                    .getXArrayAxis_Z().name;
                string xy_name
                    = XPreSpawnObjectManager.getInstance()
                    .getXArrayAxis_XY().name;
                string xz_name
                    = XPreSpawnObjectManager.getInstance()
                    .getXArrayAxis_XZ().name;
                string yz_name
                    = XPreSpawnObjectManager.getInstance()
                    .getXArrayAxis_YZ().name;
                string xy1_name
                    = XPreSpawnObjectManager.getInstance()
                    .getXArrayAxis_XY_1().name;
                string xz1_name
                    = XPreSpawnObjectManager.getInstance()
                    .getXArrayAxis_XZ_1().name;
                string yz1_name
                    = XPreSpawnObjectManager.getInstance()
                    .getXArrayAxis_YZ_1().name;
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
                else if (current_pointing_axis_name.Equals(xy_name)|| current_pointing_axis_name.Equals(xy1_name))
                {
                    //show XY
                    //Z_SHOWTAG.SetActive(true);
                    X.SetActive(true);
                    current_axis_way = "XY";
                }
                else if (current_pointing_axis_name.Equals(xz_name) || current_pointing_axis_name.Equals(xz1_name))
                {
                    //show XY
                    //Z_SHOWTAG.SetActive(true);
                    Z.SetActive(true);
                    current_axis_way = "XZ";
                }
                else if (current_pointing_axis_name.Equals(yz_name) || current_pointing_axis_name.Equals(yz1_name))
                {
                    //show XY
                    //Z_SHOWTAG.SetActive(true);
                    Y.SetActive(true);
                    current_axis_way = "YZ";
                }
            }
            else
            {
                //show "no axis"
                current_axis_way = "N";
            }
            //render clone object declaration
            Vector3 current_pointing_axis_point;
            XAxisRayService.getInstance()
                .getPointingPoint(out current_pointing_axis_point);
            switch (current_axis_way)
            {
                case "N":
                    {
                        //show nothing
                        for(int i = 0; i != pre_spawn_declaringobject_list.Count; i++)
                        {
                            pre_spawn_declaringobject_list[i].SetActive(false);
                        }
                        for (int i = 0; i != pre_spawn_declaringobject_list_back.Count; i++)
                        {
                            pre_spawn_declaringobject_list_back[i].SetActive(false);
                        }
                        middle_declaringobject.SetActive(false);
                        break;
                    }
                case "X":
                    {
                        float object_point_axis_distance = Vector3.Distance(current_pointing_axis_point, current_focusing_gameobject.transform.position);
                        transform_array_using_v3 = current_pointing_axis_point - current_focusing_gameobject.transform.position;
                        if (transform_array_using_v3.x < 0)
                        {
                            object_point_axis_distance = -object_point_axis_distance;
                        }
                        float internal_distance = (object_point_axis_distance / array_object_want_count);
                        array_saving_origin_material = current_focusing_gameobject.GetComponent<Renderer>().material;
                        for(int i = 0; i != array_object_want_count; i++)
                        {
                            transform_array_using_v3.x = current_focusing_gameobject.transform.position.x;
                            transform_array_using_v3.y = current_focusing_gameobject.transform.position.y;
                            transform_array_using_v3.z = current_focusing_gameobject.transform.position.z;
                            transform_array_using_v3.x += internal_distance * (i + 1);
                            pre_spawn_declaringobject_list[i].transform.position = transform_array_using_v3;
                            pre_spawn_declaringobject_list[i].SetActive(true);
                        }
                        break;
                    }
                case "Y":
                    {
                        float object_point_axis_distance = Vector3.Distance(current_pointing_axis_point, current_focusing_gameobject.transform.position);
                        transform_array_using_v3 = current_pointing_axis_point - current_focusing_gameobject.transform.position;
                        if (transform_array_using_v3.y < 0)
                        {
                            object_point_axis_distance = -object_point_axis_distance;
                        }
                        float internal_distance = (object_point_axis_distance / array_object_want_count);
                        array_saving_origin_material = current_focusing_gameobject.GetComponent<Renderer>().material;
                        for (int i = 0; i != array_object_want_count; i++)
                        {
                            transform_array_using_v3.x = current_focusing_gameobject.transform.position.x;
                            transform_array_using_v3.y = current_focusing_gameobject.transform.position.y;
                            transform_array_using_v3.z = current_focusing_gameobject.transform.position.z;
                            transform_array_using_v3.y += internal_distance * (i + 1);
                            pre_spawn_declaringobject_list[i].transform.position = transform_array_using_v3;
                            pre_spawn_declaringobject_list[i].SetActive(true);
                        }
                        break;
                    }
                case "Z":
                    {
                        float object_point_axis_distance = Vector3.Distance(current_pointing_axis_point, current_focusing_gameobject.transform.position);
                        transform_array_using_v3 = current_pointing_axis_point - current_focusing_gameobject.transform.position;
                        if (transform_array_using_v3.z < 0)
                        {
                            object_point_axis_distance = -object_point_axis_distance;
                        }
                        float internal_distance = (object_point_axis_distance / array_object_want_count);
                        array_saving_origin_material = current_focusing_gameobject.GetComponent<Renderer>().material;
                        for (int i = 0; i != array_object_want_count; i++)
                        {
                            transform_array_using_v3.x = current_focusing_gameobject.transform.position.x;
                            transform_array_using_v3.y = current_focusing_gameobject.transform.position.y;
                            transform_array_using_v3.z = current_focusing_gameobject.transform.position.z;
                            transform_array_using_v3.z += internal_distance * (i + 1);
                            pre_spawn_declaringobject_list[i].transform.position = transform_array_using_v3;
                            pre_spawn_declaringobject_list[i].SetActive(true);
                        }
                        break;
                    }
                case "XY":
                    {
                        float object_point_axis_distance;
                        float internal_distance;
                        //x
                        transform_array_using_v3.x = current_focusing_gameobject.transform.position.x;
                        transform_array_using_v3.y = current_focusing_gameobject.transform.position.y;
                        transform_array_using_v3.z = current_focusing_gameobject.transform.position.z;
                        transform_array_using_v3.x = current_pointing_axis_point.x;
                        object_point_axis_distance = Vector3.Distance(transform_array_using_v3, current_focusing_gameobject.transform.position);
                        transform_array_using_v3 = current_pointing_axis_point - current_focusing_gameobject.transform.position;
                        if (transform_array_using_v3.x < 0)
                        {
                            object_point_axis_distance = -object_point_axis_distance;
                        }
                        internal_distance = (object_point_axis_distance / array_object_want_count);
                        array_saving_origin_material = current_focusing_gameobject.GetComponent<Renderer>().material;
                        for (int i = 0; i != array_object_want_count; i++)
                        {
                            transform_array_using_v3.x = current_focusing_gameobject.transform.position.x;
                            transform_array_using_v3.y = current_focusing_gameobject.transform.position.y;
                            transform_array_using_v3.z = current_focusing_gameobject.transform.position.z;
                            transform_array_using_v3.x += internal_distance * (i + 1);
                            pre_spawn_declaringobject_list[i].transform.position = transform_array_using_v3;
                            pre_spawn_declaringobject_list[i].SetActive(true);
                        }
                        //y
                        transform_array_using_v3.x = current_focusing_gameobject.transform.position.x;
                        transform_array_using_v3.y = current_focusing_gameobject.transform.position.y;
                        transform_array_using_v3.z = current_focusing_gameobject.transform.position.z;
                        transform_array_using_v3.y = current_pointing_axis_point.y;
                        object_point_axis_distance = Vector3.Distance(transform_array_using_v3, current_focusing_gameobject.transform.position);
                        transform_array_using_v3 = current_pointing_axis_point - current_focusing_gameobject.transform.position;
                        if (transform_array_using_v3.y < 0)
                        {
                            object_point_axis_distance = -object_point_axis_distance;
                        }
                        internal_distance = (object_point_axis_distance / array_object_want_count);
                        array_saving_origin_material = current_focusing_gameobject.GetComponent<Renderer>().material;
                        for (int i = 0; i != array_object_want_count; i++)
                        {
                            transform_array_using_v3.x = current_focusing_gameobject.transform.position.x;
                            transform_array_using_v3.y = current_focusing_gameobject.transform.position.y;
                            transform_array_using_v3.z = current_focusing_gameobject.transform.position.z;
                            transform_array_using_v3.y += internal_distance * (i + 1);
                            pre_spawn_declaringobject_list_back[i].transform.position = transform_array_using_v3;
                            pre_spawn_declaringobject_list_back[i].SetActive(true);
                        }
                        //middle
                        transform_array_using_v3.x = current_focusing_gameobject.transform.position.x;
                        transform_array_using_v3.y = current_focusing_gameobject.transform.position.y;
                        transform_array_using_v3.z = current_focusing_gameobject.transform.position.z;
                        transform_array_using_v3.x = current_pointing_axis_point.x;
                        transform_array_using_v3.y = current_pointing_axis_point.y;
                        middle_declaringobject.transform.position = transform_array_using_v3;
                        middle_declaringobject.SetActive(true);
                        break;
                    }
                case "XZ":
                    {
                        float object_point_axis_distance;
                        float internal_distance;
                        //x
                        transform_array_using_v3.x = current_focusing_gameobject.transform.position.x;
                        transform_array_using_v3.y = current_focusing_gameobject.transform.position.y;
                        transform_array_using_v3.z = current_focusing_gameobject.transform.position.z;
                        transform_array_using_v3.x = current_pointing_axis_point.x;
                        object_point_axis_distance = Vector3.Distance(transform_array_using_v3, current_focusing_gameobject.transform.position);
                        transform_array_using_v3 = current_pointing_axis_point - current_focusing_gameobject.transform.position;
                        if (transform_array_using_v3.x < 0)
                        {
                            object_point_axis_distance = -object_point_axis_distance;
                        }
                        internal_distance = (object_point_axis_distance / array_object_want_count);
                        array_saving_origin_material = current_focusing_gameobject.GetComponent<Renderer>().material;
                        for (int i = 0; i != array_object_want_count; i++)
                        {
                            transform_array_using_v3.x = current_focusing_gameobject.transform.position.x;
                            transform_array_using_v3.y = current_focusing_gameobject.transform.position.y;
                            transform_array_using_v3.z = current_focusing_gameobject.transform.position.z;
                            transform_array_using_v3.x += internal_distance * (i + 1);
                            pre_spawn_declaringobject_list[i].transform.position = transform_array_using_v3;
                            pre_spawn_declaringobject_list[i].SetActive(true);
                        }
                        //z
                        transform_array_using_v3.x = current_focusing_gameobject.transform.position.x;
                        transform_array_using_v3.y = current_focusing_gameobject.transform.position.y;
                        transform_array_using_v3.z = current_focusing_gameobject.transform.position.z;
                        transform_array_using_v3.z = current_pointing_axis_point.z;
                        object_point_axis_distance = Vector3.Distance(transform_array_using_v3, current_focusing_gameobject.transform.position);
                        transform_array_using_v3 = current_pointing_axis_point - current_focusing_gameobject.transform.position;
                        if (transform_array_using_v3.z < 0)
                        {
                            object_point_axis_distance = -object_point_axis_distance;
                        }
                        internal_distance = (object_point_axis_distance / array_object_want_count);
                        array_saving_origin_material = current_focusing_gameobject.GetComponent<Renderer>().material;
                        for (int i = 0; i != array_object_want_count; i++)
                        {
                            transform_array_using_v3.x = current_focusing_gameobject.transform.position.x;
                            transform_array_using_v3.y = current_focusing_gameobject.transform.position.y;
                            transform_array_using_v3.z = current_focusing_gameobject.transform.position.z;
                            transform_array_using_v3.z += internal_distance * (i + 1);
                            pre_spawn_declaringobject_list_back[i].transform.position = transform_array_using_v3;
                            pre_spawn_declaringobject_list_back[i].SetActive(true);
                        }
                        //middle
                        transform_array_using_v3.x = current_focusing_gameobject.transform.position.x;
                        transform_array_using_v3.y = current_focusing_gameobject.transform.position.y;
                        transform_array_using_v3.z = current_focusing_gameobject.transform.position.z;
                        transform_array_using_v3.x = current_pointing_axis_point.x;
                        transform_array_using_v3.z = current_pointing_axis_point.z;
                        middle_declaringobject.transform.position = transform_array_using_v3;
                        middle_declaringobject.SetActive(true);
                        break;
                    }
                case "YZ":
                    {
                        float object_point_axis_distance;
                        float internal_distance;
                        //y
                        transform_array_using_v3.x = current_focusing_gameobject.transform.position.x;
                        transform_array_using_v3.y = current_focusing_gameobject.transform.position.y;
                        transform_array_using_v3.z = current_focusing_gameobject.transform.position.z;
                        transform_array_using_v3.y = current_pointing_axis_point.y;
                        object_point_axis_distance = Vector3.Distance(transform_array_using_v3, current_focusing_gameobject.transform.position);
                        transform_array_using_v3 = current_pointing_axis_point - current_focusing_gameobject.transform.position;
                        if (transform_array_using_v3.y < 0)
                        {
                            object_point_axis_distance = -object_point_axis_distance;
                        }
                        internal_distance = (object_point_axis_distance / array_object_want_count);
                        array_saving_origin_material = current_focusing_gameobject.GetComponent<Renderer>().material;
                        for (int i = 0; i != array_object_want_count; i++)
                        {
                            transform_array_using_v3.x = current_focusing_gameobject.transform.position.x;
                            transform_array_using_v3.y = current_focusing_gameobject.transform.position.y;
                            transform_array_using_v3.z = current_focusing_gameobject.transform.position.z;
                            transform_array_using_v3.y += internal_distance * (i + 1);
                            pre_spawn_declaringobject_list[i].transform.position = transform_array_using_v3;
                            pre_spawn_declaringobject_list[i].SetActive(true);
                        }
                        //z
                        transform_array_using_v3.x = current_focusing_gameobject.transform.position.x;
                        transform_array_using_v3.y = current_focusing_gameobject.transform.position.y;
                        transform_array_using_v3.z = current_focusing_gameobject.transform.position.z;
                        transform_array_using_v3.z = current_pointing_axis_point.z;
                        object_point_axis_distance = Vector3.Distance(transform_array_using_v3, current_focusing_gameobject.transform.position);
                        transform_array_using_v3 = current_pointing_axis_point - current_focusing_gameobject.transform.position;
                        if (transform_array_using_v3.z < 0)
                        {
                            object_point_axis_distance = -object_point_axis_distance;
                        }
                        internal_distance = (object_point_axis_distance / array_object_want_count);
                        array_saving_origin_material = current_focusing_gameobject.GetComponent<Renderer>().material;
                        for (int i = 0; i != array_object_want_count; i++)
                        {
                            transform_array_using_v3.x = current_focusing_gameobject.transform.position.x;
                            transform_array_using_v3.y = current_focusing_gameobject.transform.position.y;
                            transform_array_using_v3.z = current_focusing_gameobject.transform.position.z;
                            transform_array_using_v3.z += internal_distance * (i + 1);
                            pre_spawn_declaringobject_list_back[i].transform.position = transform_array_using_v3;
                            pre_spawn_declaringobject_list_back[i].SetActive(true);
                        }
                        //middle
                        transform_array_using_v3.x = current_focusing_gameobject.transform.position.x;
                        transform_array_using_v3.y = current_focusing_gameobject.transform.position.y;
                        transform_array_using_v3.z = current_focusing_gameobject.transform.position.z;
                        transform_array_using_v3.y = current_pointing_axis_point.y;
                        transform_array_using_v3.z = current_pointing_axis_point.z;
                        middle_declaringobject.transform.position = transform_array_using_v3;
                        middle_declaringobject.SetActive(true);
                        break;
                    }
            }
        }
        if (current_pointing_object == null && current_focusing_gameobject == null)
        {
            return;
        }

        if (RightControllerDriver.getInstance() != null)
        {
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
                    GameObject XArrayAxis_X
                         = XPreSpawnObjectManager.getInstance()
                        .getXArrayAxis_X();
                    GameObject XArrayAxis_Y
                         = XPreSpawnObjectManager.getInstance()
                        .getXArrayAxis_Y();
                    GameObject XArrayAxis_Z
                         = XPreSpawnObjectManager.getInstance()
                        .getXArrayAxis_Z();
                    GameObject XArrayAxis_XY
                         = XPreSpawnObjectManager.getInstance()
                        .getXArrayAxis_XY();
                    GameObject XArrayAxis_YZ
                         = XPreSpawnObjectManager.getInstance()
                        .getXArrayAxis_YZ();
                    GameObject XArrayAxis_XZ
                         = XPreSpawnObjectManager.getInstance()
                        .getXArrayAxis_XZ();
                    GameObject XArrayAxis_XY_1
                         = XPreSpawnObjectManager.getInstance()
                        .getXArrayAxis_XY_1();
                    GameObject XArrayAxis_YZ_1
                         = XPreSpawnObjectManager.getInstance()
                        .getXArrayAxis_YZ_1();
                    GameObject XArrayAxis_XZ_1
                         = XPreSpawnObjectManager.getInstance()
                        .getXArrayAxis_XZ_1();
                    XArrayAxis_X.transform.position
                        = current_focusing_gameobject.transform.position;
                    XArrayAxis_Y.transform.position
                        = current_focusing_gameobject.transform.position;
                    XArrayAxis_Z.transform.position
                        = current_focusing_gameobject.transform.position;
                    XArrayAxis_XY.transform.position
                        = current_focusing_gameobject.transform.position;
                    XArrayAxis_YZ.transform.position
                        = current_focusing_gameobject.transform.position;
                    XArrayAxis_XZ.transform.position
                        = current_focusing_gameobject.transform.position;
                    XArrayAxis_XY_1.transform.position
                        = current_focusing_gameobject.transform.position;
                    XArrayAxis_YZ_1.transform.position
                        = current_focusing_gameobject.transform.position;
                    XArrayAxis_XZ_1.transform.position
                        = current_focusing_gameobject.transform.position;
                    XArrayAxis_X.SetActive(true);
                    XArrayAxis_Y.SetActive(true);
                    XArrayAxis_Z.SetActive(true);
                    XArrayAxis_XY.SetActive(true);
                    XArrayAxis_YZ.SetActive(true);
                    XArrayAxis_XZ.SetActive(true);
                    XArrayAxis_XY_1.SetActive(true);
                    XArrayAxis_YZ_1.SetActive(true);
                    XArrayAxis_XZ_1.SetActive(true);
                    //pre spawn objects
                    for (int i =0;i!= pre_spawn_declaringobject_list.Count; i++)
                    {
                        GameObject.Destroy(pre_spawn_declaringobject_list[i]);
                    }
                    pre_spawn_declaringobject_list.Clear();
                    for (int i = 0; i != array_object_want_count; i++)
                    {
                        GameObject current_prespawn = GameObject.Instantiate(current_focusing_gameobject);
                        current_prespawn.name = current_prespawn.name.Replace("(Clone)", "");
                        pre_spawn_declaringobject_list.Add(current_prespawn);
                        current_prespawn.GetComponent<Renderer>().material = DECLARATION_MATERIAL;
                        current_prespawn.SetActive(false);
                    }

                    for (int i = 0; i != pre_spawn_declaringobject_list_back.Count; i++)
                    {
                        GameObject.Destroy(pre_spawn_declaringobject_list_back[i]);
                    }
                    pre_spawn_declaringobject_list_back.Clear();
                    for (int i = 0; i != array_object_want_count; i++)
                    {
                        GameObject current_prespawn = GameObject.Instantiate(current_focusing_gameobject);
                        current_prespawn.name = current_prespawn.name.Replace("(Clone)", "");
                        pre_spawn_declaringobject_list_back.Add(current_prespawn);
                        current_prespawn.GetComponent<Renderer>().material = DECLARATION_MATERIAL;
                        current_prespawn.SetActive(false);
                    }

                    GameObject.Destroy(middle_declaringobject);
                    middle_declaringobject = GameObject.Instantiate(current_focusing_gameobject);
                    middle_declaringobject.name = middle_declaringobject.name.Replace("(Clone)", "");
                    middle_declaringobject.GetComponent<Renderer>().material = DECLARATION_MATERIAL;
                    middle_declaringobject.SetActive(false);
                }
                else
                {
                    //instantiate objects
                    GameObject current_pointing_axis
    = XAxisRayService.getInstance().getPointingAxisGameObject();
                    if(current_pointing_axis != null)
                    {
                        for(int i = 0; i != pre_spawn_declaringobject_list.Count; i++)
                        {
                            GameObject current_ins = GameObject.Instantiate(pre_spawn_declaringobject_list[i]);
                            current_ins.name = current_ins.name.Replace("(Clone)", "");
                            current_ins.GetComponent<Renderer>().material = array_saving_origin_material;
                        }
                        if (pre_spawn_declaringobject_list_back[0].activeSelf)
                        {
                            for (int i = 0; i != pre_spawn_declaringobject_list_back.Count; i++)
                            {
                                GameObject current_ins = GameObject.Instantiate(pre_spawn_declaringobject_list_back[i]);
                                current_ins.name = current_ins.name.Replace("(Clone)", "");
                                current_ins.GetComponent<Renderer>().material = array_saving_origin_material;
                            }
                            GameObject middle_ins = GameObject.Instantiate(middle_declaringobject);
                            middle_ins.name = middle_ins.name.Replace("(Clone)", "");
                            middle_ins.GetComponent<Renderer>().material = array_saving_origin_material;
                        }
                    }
                }
                return;
            }
            if (RightControllerDriver.getInstance().TriggerUp())
            {

                return;
            }
            //panel
            if (RightControllerDriver.getInstance().PanelDown())
            {
                //show nothing
                for (int i = 0; i != pre_spawn_declaringobject_list.Count; i++)
                {
                    pre_spawn_declaringobject_list[i].SetActive(false);
                }
                for (int i = 0; i != pre_spawn_declaringobject_list_back.Count; i++)
                {
                    pre_spawn_declaringobject_list_back[i].SetActive(false);
                }
                middle_declaringobject.SetActive(false);
                current_focusing_gameobject = null;
                //delete axis
                GameObject XArrayAxis_X
                     = XPreSpawnObjectManager.getInstance()
                    .getXArrayAxis_X();
                GameObject XArrayAxis_Y
                     = XPreSpawnObjectManager.getInstance()
                    .getXArrayAxis_Y();
                GameObject XArrayAxis_Z
                     = XPreSpawnObjectManager.getInstance()
                    .getXArrayAxis_Z();
                GameObject XArrayAxis_XY
                     = XPreSpawnObjectManager.getInstance()
                    .getXArrayAxis_XY();
                GameObject XArrayAxis_YZ
                     = XPreSpawnObjectManager.getInstance()
                    .getXArrayAxis_YZ();
                GameObject XArrayAxis_XZ
                     = XPreSpawnObjectManager.getInstance()
                    .getXArrayAxis_XZ();
                GameObject XArrayAxis_XY_1
                     = XPreSpawnObjectManager.getInstance()
                    .getXArrayAxis_XY_1();
                GameObject XArrayAxis_YZ_1
                     = XPreSpawnObjectManager.getInstance()
                    .getXArrayAxis_YZ_1();
                GameObject XArrayAxis_XZ_1
                     = XPreSpawnObjectManager.getInstance()
                    .getXArrayAxis_XZ_1();
                XArrayAxis_X.SetActive(false);
                XArrayAxis_Y.SetActive(false);
                XArrayAxis_Z.SetActive(false);
                XArrayAxis_XY.SetActive(false);
                XArrayAxis_YZ.SetActive(false);
                XArrayAxis_XZ.SetActive(false);
                XArrayAxis_XY_1.SetActive(false);
                XArrayAxis_YZ_1.SetActive(false);
                XArrayAxis_XZ_1.SetActive(false);
                return;
            }
            if (RightControllerDriver.getInstance().GripDown())
            {
                //low array_object_want_count
                if(array_object_want_count != 1)
                {
                    array_object_want_count--;
                    if (current_focusing_gameobject != null)
                    {
                        GameObject
                            .Destroy(pre_spawn_declaringobject_list[pre_spawn_declaringobject_list.Count - 1]);
                        GameObject
                            .Destroy(pre_spawn_declaringobject_list_back[pre_spawn_declaringobject_list_back.Count - 1]);
                        pre_spawn_declaringobject_list.RemoveAt(pre_spawn_declaringobject_list.Count - 1);
                        pre_spawn_declaringobject_list_back.RemoveAt(pre_spawn_declaringobject_list_back.Count - 1);
                    }
                }
                return;
            }
            if (LeftControllerDriver.getInstance() != null)
            {
                if (LeftControllerDriver.getInstance().GripDown())
                {
                    //add array_object_want_count
                    array_object_want_count++;
                    if (current_focusing_gameobject != null)
                    {
                        //pre spawn objects
                        GameObject current_ins = Instantiate(pre_spawn_declaringobject_list[0]);
                        current_ins.name = current_ins.name.Replace("(Clone)", "");
                        pre_spawn_declaringobject_list.Add(current_ins);
                    }
                    if (current_focusing_gameobject != null)
                    {
                        //pre spawn objects
                        GameObject current_ins = Instantiate(pre_spawn_declaringobject_list_back[0]);
                        current_ins.name = current_ins.name.Replace("(Clone)", "");
                        pre_spawn_declaringobject_list_back.Add(current_ins);
                    }
                    return;
                }
            }
        }
    }
}
