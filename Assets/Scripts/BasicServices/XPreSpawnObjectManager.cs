using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPreSpawnObjectManager : MonoBehaviour {
    //moving
    [SerializeField]
    GameObject XMovingAxis_X;
    private GameObject xMovingAxis_X;
    public GameObject getXMovingAxis_X()
    {
        return xMovingAxis_X;
    }

    [SerializeField]
    GameObject XMovingAxis_Y;
    private GameObject xMovingAxis_Y;
    public GameObject getXMovingAxis_Y()
    {
        return xMovingAxis_Y;
    }

    [SerializeField]
    GameObject XMovingAxis_Z;
    private GameObject xMovingAxis_Z;
    public GameObject getXMovingAxis_Z()
    {
        return xMovingAxis_Z;
    }
    //scale
    [SerializeField]
    GameObject XScaleAxis_X;
    private GameObject xScaleAxis_X;
    public GameObject getXScaleAxis_X()
    {
        return xScaleAxis_X;
    }

    [SerializeField]
    GameObject XScaleAxis_Y;
    private GameObject xScaleAxis_Y;
    public GameObject getXScaleAxis_Y()
    {
        return xScaleAxis_Y;
    }

    [SerializeField]
    GameObject XScaleAxis_Z;
    private GameObject xScaleAxis_Z;
    public GameObject getXScaleAxis_Z()
    {
        return xScaleAxis_Z;
    }
    //rotate
    [SerializeField]
    GameObject XRotateAxis_X;
    private GameObject xRotateAxis_X;
    public GameObject getXRotateAxis_X()
    {
        return xRotateAxis_X;
    }

    [SerializeField]
    GameObject XRotateAxis_Y;
    private GameObject xRotateAxis_Y;
    public GameObject getXRotateAxis_Y()
    {
        return xRotateAxis_Y;
    }

    [SerializeField]
    GameObject XRotateAxis_Z;
    private GameObject xRotateAxis_Z;
    public GameObject getXRotateAxis_Z()
    {
        return xRotateAxis_Z;
    }
    //array
    [SerializeField]
    GameObject XArrayAxis_X;
    private GameObject xArrayAxis_X;
    public GameObject getXArrayAxis_X()
    {
        return xArrayAxis_X;
    }

    [SerializeField]
    GameObject XArrayAxis_Y;
    private GameObject xArrayAxis_Y;
    public GameObject getXArrayAxis_Y()
    {
        return xArrayAxis_Y;
    }

    [SerializeField]
    GameObject XArrayAxis_Z;
    private GameObject xArrayAxis_Z;
    public GameObject getXArrayAxis_Z()
    {
        return xArrayAxis_Z;
    }

    [SerializeField]
    GameObject XArrayAxis_XY;
    private GameObject xArrayAxis_XY;
    public GameObject getXArrayAxis_XY()
    {
        return xArrayAxis_XY;
    }

    [SerializeField]
    GameObject XArrayAxis_YZ;
    private GameObject xArrayAxis_YZ;
    public GameObject getXArrayAxis_YZ()
    {
        return xArrayAxis_YZ;
    }

    [SerializeField]
    GameObject XArrayAxis_XZ;
    private GameObject xArrayAxis_XZ;
    public GameObject getXArrayAxis_XZ()
    {
        return xArrayAxis_XZ;
    }
    [SerializeField]
    GameObject XArrayAxis_XY_1;
    private GameObject xArrayAxis_XY_1;
    public GameObject getXArrayAxis_XY_1()
    {
        return xArrayAxis_XY_1;
    }

    [SerializeField]
    GameObject XArrayAxis_YZ_1;
    private GameObject xArrayAxis_YZ_1;
    public GameObject getXArrayAxis_YZ_1()
    {
        return xArrayAxis_YZ_1;
    }

    [SerializeField]
    GameObject XArrayAxis_XZ_1;
    private GameObject xArrayAxis_XZ_1;
    public GameObject getXArrayAxis_XZ_1()
    {
        return xArrayAxis_XZ_1;
    }
    [SerializeField]
    GameObject XRay_AttachingBall;
    private GameObject xRay_AttachingBall;
    public GameObject getxRay_AttachingBall()
    {
        return xRay_AttachingBall;
    }

    [SerializeField]
    GameObject XAxisRay_AttachingBall;
    private GameObject xAxisRay_AttachingBall;
    public GameObject getxAxisRay_AttachingBall()
    {
        return xAxisRay_AttachingBall;
    }

    private static XPreSpawnObjectManager thisInstance;
    public static XPreSpawnObjectManager getInstance()
    {
        return thisInstance;
    }
    private void Awake()
    {
        thisInstance = this;
    }
    // Use this for initialization
    void Start () {

        xMovingAxis_X = Instantiate(XMovingAxis_X);
        xMovingAxis_X.SetActive(false);

        xMovingAxis_Y = Instantiate(XMovingAxis_Y);
        xMovingAxis_Y.SetActive(false);

        xMovingAxis_Z = Instantiate(XMovingAxis_Z);
        xMovingAxis_Z.SetActive(false);

        xScaleAxis_X = Instantiate(XScaleAxis_X);
        xScaleAxis_X.SetActive(false);

        xScaleAxis_Y = Instantiate(XScaleAxis_Y);
        xScaleAxis_Y.SetActive(false);

        xScaleAxis_Z = Instantiate(XScaleAxis_Z);
        xScaleAxis_Z.SetActive(false);

        xRotateAxis_X = Instantiate(XRotateAxis_X);
        xRotateAxis_X.SetActive(false);

        xRotateAxis_Y = Instantiate(XRotateAxis_Y);
        xRotateAxis_Y.SetActive(false);

        xRotateAxis_Z = Instantiate(XRotateAxis_Z);
        xRotateAxis_Z.SetActive(false);

        xArrayAxis_X = Instantiate(XArrayAxis_X);
        xArrayAxis_X.SetActive(false);

        xArrayAxis_Y = Instantiate(XArrayAxis_Y);
        xArrayAxis_Y.SetActive(false);

        xArrayAxis_Z = Instantiate(XArrayAxis_Z);
        xArrayAxis_Z.SetActive(false);

        xArrayAxis_XY = Instantiate(XArrayAxis_XY);
        xArrayAxis_XY.SetActive(false);

        xArrayAxis_YZ = Instantiate(XArrayAxis_YZ);
        xArrayAxis_YZ.SetActive(false);

        xArrayAxis_XZ = Instantiate(XArrayAxis_XZ);
        xArrayAxis_XZ.SetActive(false);

        xArrayAxis_XY_1 = Instantiate(XArrayAxis_XY_1);
        xArrayAxis_XY_1.SetActive(false);

        xArrayAxis_YZ_1 = Instantiate(XArrayAxis_YZ_1);
        xArrayAxis_YZ_1.SetActive(false);

        xArrayAxis_XZ_1 = Instantiate(XArrayAxis_XZ_1);
        xArrayAxis_XZ_1.SetActive(false);

        xRay_AttachingBall = Instantiate(XRay_AttachingBall);
        xRay_AttachingBall.SetActive(false);

        xAxisRay_AttachingBall = Instantiate(XAxisRay_AttachingBall);
        xAxisRay_AttachingBall.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
