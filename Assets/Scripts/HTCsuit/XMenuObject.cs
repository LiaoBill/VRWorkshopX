using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XMenuObject : MonoBehaviour {
    [SerializeField]
    GameObject CREATE;
    [SerializeField]
    GameObject DELETE;
    [SerializeField]
    GameObject COPY;
    [SerializeField]
    GameObject EXPORT;
    [SerializeField]
    GameObject ARRAY;
    // Use this for initialization
    void Start () {
        selection_index = 0;
        unselected_color = new Color(0.0f, 160 / 255.0f, 1.0f, 1.0f);
        selected_color = new Color(1.0f, 0.0f, 79 / 255.0f, 1.0f);
    }
    private Color unselected_color;
    private Color selected_color;
    private int selection_index;
    private const int selection_index_boundry = 4;
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
        if (XMenuBase.getInstance().getCurrentStatus() != XEnumMenuStatus.OBJECT)
        {
            CREATE.SetActive(false);
            DELETE.SetActive(false);
            COPY.SetActive(false);
            EXPORT.SetActive(false);
            ARRAY.SetActive(false);
            return;
        }
        else
        {
            CREATE.SetActive(true);
            DELETE.SetActive(true);
            COPY.SetActive(true);
            EXPORT.SetActive(true);
            ARRAY.SetActive(true);
        }
        //print selection_index
        CREATE.GetComponent<Image>().color = unselected_color;
        DELETE.GetComponent<Image>().color = unselected_color;
        COPY.GetComponent<Image>().color = unselected_color;
        EXPORT.GetComponent<Image>().color = unselected_color;
        ARRAY.GetComponent<Image>().color = unselected_color;
        switch (selection_index)
        {
            case 0:
                {
                    CREATE.GetComponent<Image>().color = selected_color;
                    break;
                }
            case 1:
                {
                    DELETE.GetComponent<Image>().color = selected_color;
                    break;
                }
            case 2:
                {
                    COPY.GetComponent<Image>().color = selected_color;
                    break;
                }
            case 3:
                {
                    EXPORT.GetComponent<Image>().color = selected_color;
                    break;
                }
            case 4:
                {
                    ARRAY.GetComponent<Image>().color = selected_color;
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
                if (current_function_status == XEnumFunctionStatus.CREATE
                    || current_function_status == XEnumFunctionStatus.DELETE
                   || current_function_status == XEnumFunctionStatus.COPY
                   || current_function_status == XEnumFunctionStatus.EXPORT
                   || current_function_status == XEnumFunctionStatus.ARRAY)
                {
                    XFunctionInfor.getInstance().setCurrentFunctionStatus(XEnumFunctionStatus.NOTHING);
                }
                else
                {
                    switch (selection_index)
                    {
                        case 0:
                            {
                                XFunctionInfor.getInstance().setCurrentFunctionStatus(XEnumFunctionStatus.CREATE);
                                break;
                            }
                        case 1:
                            {
                                XFunctionInfor.getInstance().setCurrentFunctionStatus(XEnumFunctionStatus.DELETE);
                                break;
                            }
                        case 2:
                            {
                                XFunctionInfor.getInstance().setCurrentFunctionStatus(XEnumFunctionStatus.COPY);
                                break;
                            }
                        case 3:
                            {
                                XFunctionInfor.getInstance().setCurrentFunctionStatus(XEnumFunctionStatus.EXPORT);
                                break;
                            }
                        case 4:
                            {
                                XFunctionInfor.getInstance().setCurrentFunctionStatus(XEnumFunctionStatus.ARRAY);
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
