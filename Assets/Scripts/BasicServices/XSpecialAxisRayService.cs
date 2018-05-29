using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XSpecialAxisRayService : MonoBehaviour {
    [SerializeField]
    GameObject XRayStarter;
    [SerializeField]
    GameObject XRayEnder;
    private GameObject pointing_object;
    public GameObject getPointingObject()
    {
        return pointing_object;
    }
    private Ray XRay;
    private static XSpecialAxisRayService thisInstance;
    public static XSpecialAxisRayService getInstance()
    {
        return thisInstance;
    }
    private void Awake()
    {
        thisInstance = this;
    }
    // Use this for initialization
    void Start()
    {
        XRay = new Ray();
        pointing_object = null;
        //line_render_positions = new Vector3[2];
    }
    //private Vector3[] line_render_positions;
    // Update is called once per frame
    void Update()
    {
        XRay.origin = XRayStarter.transform.position;
        XRay.direction
            = XRayEnder.transform.position
            - XRayStarter.transform.position;
        //render line
        //line_render_positions[0] = XRay.origin;
        //line_render_positions[1] = XRay.origin + XRay.direction * 10.0f;
        //XRay_Line.GetComponent<LineRenderer>().SetPositions(line_render_positions);
        RaycastHit hit;
        int layerMask = 1 << 11;
        if (Physics.Raycast(XRay, out hit, Mathf.Infinity, layerMask))
        {
            pointing_object = hit.collider.gameObject;
            //render attaching_ball
            /*
            GameObject attaching_ball = XPreSpawnObjectManager.getInstance()
                .getxRay_AttachingBall();
            attaching_ball.transform.position
                = hit.point;
            attaching_ball.SetActive(true);
            */
        }
        else
        {
            pointing_object = null;
            /*
            GameObject attaching_ball = XPreSpawnObjectManager.getInstance()
                .getxRay_AttachingBall();
            attaching_ball.SetActive(false);
            */
        }

    }
}
