using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XMenuBase : MonoBehaviour
{
    [SerializeField]
    GameObject MenuCanvas;
    //TRANSOFRM
    [SerializeField]
    GameObject TRANSFORM;
    [SerializeField]
    GameObject OBJECT;
    [SerializeField]
    GameObject PAINT;
    //singleton
    private static XMenuBase thisInstance;
    public static XMenuBase getInstance()
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
        is_show_menu = false;
        current_status = XEnumMenuStatus.MAIN_MENU;
        selection_index = 0;
        unselected_color = new Color(0.0f, 160 / 255.0f, 1.0f, 1.0f);
        selected_color = new Color(1.0f, 0.0f, 79 / 255.0f, 1.0f);
    }
    private Color unselected_color;
    private Color selected_color;
    private bool is_show_menu;
    public bool getIsShowMenu()
    {
        return is_show_menu;
    }
    private XEnumMenuStatus current_status;
    public XEnumMenuStatus getCurrentStatus()
    {
        return current_status;
    }
    public void setCurrentStatus(XEnumMenuStatus current_status)
    {
        this.current_status = current_status;
    }
    private int selection_index;
    private const int selection_index_boundry = 2;
    private void addSelectionIndex()
    {
        if(selection_index == selection_index_boundry)
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
        if(selection_index == 0)
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
        if (is_show_menu)
        {
            MenuCanvas.SetActive(true);
        }
        else
        {
            MenuCanvas.SetActive(false);
            if (LeftControllerDriver.getInstance() != null)
            {
                //menu
                if (LeftControllerDriver.getInstance().AppMenuDown())
                {
                    switchIsShowMenu();
                    return;
                }
            }
            return;
        }
        if (current_status != XEnumMenuStatus.MAIN_MENU)
        {
            return;
        }
        //print selection_index
        TRANSFORM.GetComponent<Image>().color = unselected_color;
        OBJECT.GetComponent<Image>().color = unselected_color;
        PAINT.GetComponent<Image>().color = unselected_color;
        switch (selection_index)
        {
            case 0:
                {
                    TRANSFORM.GetComponent<Image>().color = selected_color;
                    break;
                }
            case 1:
                {
                    OBJECT.GetComponent<Image>().color = selected_color;
                    break;
                }
            case 2:
                {
                    PAINT.GetComponent<Image>().color = selected_color;
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
            //menu
            if (LeftControllerDriver.getInstance().AppMenuDown())
            {
                switchIsShowMenu();
                return;
            }
            //panel
            if (LeftControllerDriver.getInstance().PanelRightDown())
            {
                addSelectionIndex();
                return;
            }
            if (LeftControllerDriver.getInstance().PanelLeftDown())
            {
                diminishSelectionIndex();
                return;
            }
            if (LeftControllerDriver.getInstance().TriggerDown())
            {
                switch (selection_index)
                {
                    case 0:
                        {
                            current_status = XEnumMenuStatus.TRANSFORM;
                            break;
                        }
                    case 1:
                        {
                            current_status = XEnumMenuStatus.OBJECT;
                            break;
                        }
                    case 2:
                        {
                            current_status = XEnumMenuStatus.PAINT;
                            break;
                        }
                    default:
                        {
                            Debug.Log("[TriggerDown Part] Menu selection_index status error, index value : " + selection_index);
                            break;
                        }
                }
                return;
            }
        }
    }
    private void switchIsShowMenu()
    {
        if (is_show_menu)
        {
            is_show_menu = false;
        }
        else
        {
            is_show_menu = true;
        }
    }
}
