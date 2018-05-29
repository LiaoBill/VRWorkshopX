using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XVertexTranformMove : MonoBehaviour {
    [SerializeField]
    GameObject ObjectSpawnerPoint;
    [SerializeField]
    GameObject VertexHead_BALL;
    [SerializeField]
    Material VertexHead_Ball_origin;
    [SerializeField]
    Material VertexHead_Ball_react;
    // Use this for initialization
    void Start () {
        binding_gameObject = null;
    }
    private GameObject binding_gameObject;
	// Update is called once per frame
	void Update () {
        XEnumFunctionStatus current_function_status
            = XFunctionInfor.getInstance().getCurrentFunctionStatus();
        if (current_function_status != XEnumFunctionStatus.NOTHING)
        {
            return;
        }
        else
        {

        }
        GameObject current_touching = XVertexCollideService.getInstance()
            .getCurrentTouchingGameObject();
        //render vertex
        if(current_touching == null && binding_gameObject == null)
        {
            VertexHead_BALL.GetComponent<Renderer>().material
                = VertexHead_Ball_origin;
            return;
        }
        else
        {
            VertexHead_BALL.GetComponent<Renderer>().material
                = VertexHead_Ball_react;
        }
        
        //vive event handler
        if (RightControllerDriver.getInstance() != null)
        {
            if (RightControllerDriver.getInstance().Triggering())
            {
                if (binding_gameObject == null)
                {
                    binding_gameObject = XGroupList.getInstance().getFatherFromGroupSon(current_touching);
                    if (binding_gameObject == null)
                    {
                        binding_gameObject = current_touching;
                    }
                }
                else
                {
                    binding_gameObject.transform.position =
                        ObjectSpawnerPoint.transform.position;
                }
                return;
            }
            if (RightControllerDriver.getInstance().TriggerUp())
            {
                binding_gameObject = null;
            }
        }
    }
}
