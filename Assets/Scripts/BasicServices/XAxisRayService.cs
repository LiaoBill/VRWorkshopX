using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XAxisRayService : MonoBehaviour {
    [SerializeField]
    GameObject XRayStarter;
    [SerializeField]
    GameObject XRayEnder;

    private GameObject pointing_axis;
    private Vector3 pointing_point;
    public bool getPointingPoint(out Vector3 pointingPoint)
    {
        if (pointing_point.Equals(default_vector3))
        {
            pointingPoint = default_vector3;
            return false;
        }
        else
        {
            pointingPoint = pointing_point;
            return true;
        }
    }
    public GameObject getPointingAxisGameObject()
    {
        return pointing_axis;
    }
    private static XAxisRayService thisInstance;
    public static XAxisRayService getInstance()
    {
        return thisInstance;
    }
    private void Awake()
    {
        thisInstance = this;
    }
    private Ray XAxisRay;
    private Vector3 default_vector3;
    // Use this for initialization
    void Start () {
        XAxisRay = new Ray();
        pointing_axis = null;
        default_vector3 = default(Vector3);
        pointing_point = default_vector3;
    }
	
	// Update is called once per frame
	void Update () {
        XAxisRay.origin = XRayStarter.transform.position;
        XAxisRay.direction
            = XRayEnder.transform.position
            - XRayStarter.transform.position;
        RaycastHit hit;
        int layerMask = 1 << 9;
        if (Physics.Raycast(XAxisRay, out hit, Mathf.Infinity, layerMask))
        {
            pointing_axis = hit.collider.gameObject;
            pointing_point = hit.point;
            //render attaching_ball
            GameObject attaching_ball = XPreSpawnObjectManager.getInstance()
                .getxAxisRay_AttachingBall();
            attaching_ball.transform.position
                = hit.point;
            attaching_ball.SetActive(true);
        }
        else
        {
            pointing_axis = null;
            pointing_point = default_vector3;
            GameObject attaching_ball = XPreSpawnObjectManager.getInstance()
                .getxAxisRay_AttachingBall();
            attaching_ball.SetActive(false);
        }
    }
}
