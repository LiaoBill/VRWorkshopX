using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XFunctionInfor : MonoBehaviour {
    private XEnumFunctionStatus current_function_status;
    public void setCurrentFunctionStatus(XEnumFunctionStatus current_function_status)
    {
        this.current_function_status = current_function_status;
    }
    public XEnumFunctionStatus getCurrentFunctionStatus()
    {
        return current_function_status;
    }
    private static XFunctionInfor thisInstance;
    public static XFunctionInfor getInstance()
    {
        return thisInstance;
    }
    private void Awake()
    {
        thisInstance = this;
    }
    // Use this for initialization
    void Start () {
        current_function_status = XEnumFunctionStatus.NOTHING;
    }
	
	// Update is called once per frame
	void Update () {
	    //render current_function_status
        
	}
}
