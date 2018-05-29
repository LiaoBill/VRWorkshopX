using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XMenuPaint : MonoBehaviour {
    [SerializeField]
    GameObject MATERIAL2D;
    // Use this for initialization
    void Start () {
        selection_index = 0;
        unselected_color = new Color(0.0f, 160 / 255.0f, 1.0f, 1.0f);
        selected_color = new Color(1.0f, 0.0f, 79 / 255.0f, 1.0f);
    }
    private Color unselected_color;
    private Color selected_color;
    private int selection_index;
    private const int selection_index_boundry = 0;
    private void addSelectionIndex()
    {
        if (selection_index == selection_index_boundry)
        {
            selection_index = 0;
        }
        else
        {
            selection_index++;
        }
    }
    private void diminishSelectionIndex()
    {
        if (selection_index == 0)
        {
            selection_index = selection_index_boundry;
        }
        else
        {
            selection_index--;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!XMenuBase.getInstance().getIsShowMenu())
        {
            return;
        }
        if (XMenuBase.getInstance().getCurrentStatus() != XEnumMenuStatus.PAINT)
        {
            MATERIAL2D.SetActive(false);
            return;
        }
        else
        {
            MATERIAL2D.SetActive(true);
        }
        //print selection_index
        MATERIAL2D.GetComponent<Image>().color = unselected_color;
        switch (selection_index)
        {
            case 0:
                {
                    MATERIAL2D.GetComponent<Image>().color = selected_color;
                    break;
                }
            default:
                {
                    Debug.Log("Menu selection_index status error, index value : " + selection_index);
                    break;
                }
        }
        //htcvive event handlers
        if (LeftControllerDriver.getInstance() != null)
        {
            //panel
            if (LeftControllerDriver.getInstance().PanelRightDown())
            {
                addSelectionIndex();
            }
            if (LeftControllerDriver.getInstance().PanelLeftDown())
            {
                diminishSelectionIndex();
            }
            if (LeftControllerDriver.getInstance().PanelDownDown())
            {
                XMenuBase.getInstance().setCurrentStatus(XEnumMenuStatus.MAIN_MENU);
                return;
            }
            if (LeftControllerDriver.getInstance().TriggerDown())
            {
                XEnumFunctionStatus current_function_status = XFunctionInfor.getInstance().getCurrentFunctionStatus();
                if (current_function_status == XEnumFunctionStatus.MATERIAL2D)
                {
                    XFunctionInfor.getInstance().setCurrentFunctionStatus(XEnumFunctionStatus.NOTHING);
                }
                else
                {
                    switch (selection_index)
                    {
                        case 0:
                            {
                                XFunctionInfor.getInstance().setCurrentFunctionStatus(XEnumFunctionStatus.MATERIAL2D);
                                break;
                            }
                        default:
                            {
                                Debug.Log("[FUNCTION SELECITON TRIGGERING] Menu selection_index status error, index value : " + selection_index);
                                break;
                            }
                    }
                }
            }
        }
    }
}
