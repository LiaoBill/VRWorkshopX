using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XMenuTranform : MonoBehaviour {
    [SerializeField]
    GameObject MOVE;
    [SerializeField]
    GameObject ROTATE;
    [SerializeField]
    GameObject SCALE;
    [SerializeField]
    GameObject GROUP;
    // Use this for initialization
    void Start () {
        selection_index = 0;
        unselected_color = new Color(0.0f, 160 / 255.0f, 1.0f, 1.0f);
        selected_color = new Color(1.0f, 0.0f, 79 / 255.0f, 1.0f);
    }
    private Color unselected_color;
    private Color selected_color;
    private int selection_index;
    private const int selection_index_boundry = 3;
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
    void Update () {
        if (!XMenuBase.getInstance().getIsShowMenu())
        {
            return;
        }
        if (XMenuBase.getInstance().getCurrentStatus()!=XEnumMenuStatus.TRANSFORM)
        {
            MOVE.SetActive(false);
            ROTATE.SetActive(false);
            SCALE.SetActive(false);
            GROUP.SetActive(false);
            return;
        }
        else
        {
            MOVE.SetActive(true);
            ROTATE.SetActive(true);
            SCALE.SetActive(true);
            GROUP.SetActive(true);
        }
        //print selection_index
        MOVE.GetComponent<Image>().color = unselected_color;
        ROTATE.GetComponent<Image>().color = unselected_color;
        SCALE.GetComponent<Image>().color = unselected_color;
        GROUP.GetComponent<Image>().color = unselected_color;
        switch (selection_index)
        {
            case 0:
                {
                    MOVE.GetComponent<Image>().color = selected_color;
                    break;
                }
            case 1:
                {
                    ROTATE.GetComponent<Image>().color = selected_color;
                    break;
                }
            case 2:
                {
                    SCALE.GetComponent<Image>().color = selected_color;
                    break;
                }
            case 3:
                {
                    GROUP.GetComponent<Image>().color = selected_color;
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
                if (current_function_status == XEnumFunctionStatus.MOVE
                    || current_function_status == XEnumFunctionStatus.ROTATE
                   || current_function_status == XEnumFunctionStatus.SCALE
                   || current_function_status == XEnumFunctionStatus.GROUP)
                {
                    XFunctionInfor.getInstance().setCurrentFunctionStatus(XEnumFunctionStatus.NOTHING);
                }
                else
                {
                    switch (selection_index)
                    {
                        case 0:
                            {
                                XFunctionInfor.getInstance().setCurrentFunctionStatus(XEnumFunctionStatus.MOVE);
                                break;
                            }
                        case 1:
                            {
                                XFunctionInfor.getInstance().setCurrentFunctionStatus(XEnumFunctionStatus.ROTATE);
                                break;
                            }
                        case 2:
                            {
                                XFunctionInfor.getInstance().setCurrentFunctionStatus(XEnumFunctionStatus.SCALE);
                                break;
                            }
                        case 3:
                            {
                                XFunctionInfor.getInstance().setCurrentFunctionStatus(XEnumFunctionStatus.GROUP);
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
