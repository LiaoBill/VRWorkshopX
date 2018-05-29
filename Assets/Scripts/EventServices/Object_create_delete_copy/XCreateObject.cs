using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XCreateObject : MonoBehaviour {
    [Header("the whole canvas")]
    [SerializeField]
    GameObject CreateObjectMenuCanvas;

    [Header("button gameobject array")]
    [SerializeField]
    GameObject[] CREATE_OBJECT_BUTTONS;

    [Header("object prefabs")]
    [SerializeField]
    GameObject XCreate_CUBE_PREFAB;
    [SerializeField]
    GameObject XCreate_CUBE_DECLARE;
    [SerializeField]
    GameObject XCreate_SPHERE_PREFAB;
    [SerializeField]
    GameObject XCreate_SPHERE_DECLARE;
    [SerializeField]
    GameObject XCreate_PLANE_PREFAB;
    [SerializeField]
    GameObject XCreate_PLANE_DECLARE;

    [Header("places for button spawning")]
    [SerializeField]
    GameObject MiddleButton_POINT;
    [SerializeField]
    GameObject LeftButton_POINT;
    [SerializeField]
    GameObject RightButton_POINT;
    [Header("places for object spawning")]
    [SerializeField]
    GameObject ObjectSpawnerPoint;

    private Color fading_button_color;
    private Color selecting_button_color;
    // Use this for initialization
    void Start () {
        selection_index_boundry = CREATE_OBJECT_BUTTONS.Length - 1;
        fading_button_color = new Color(0.0f, 160 / 255.0f, 1.0f, 0.5f);
        selecting_button_color = new Color(1.0f, 0.0f, 79 / 255.0f, 1.0f);
        spawned_declaring_object = null;
    }
    private int selection_index;
    private int selection_index_boundry;
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
    private int getPreviousSelectionIndex()
    {
        if(selection_index == 0)
        {
            return selection_index_boundry;
        }
        else
        {
            return selection_index-1;
        }
    }
    private int getAfterSelectionIndex()
    {
        if(selection_index == selection_index_boundry)
        {
            return 0;
        }
        else
        {
            return selection_index + 1;
        }
    }
    private GameObject spawned_declaring_object;
    // Update is called once per frame
    void Update () {
        XEnumFunctionStatus current_function_status
            = XFunctionInfor.getInstance().getCurrentFunctionStatus();
        if (current_function_status != XEnumFunctionStatus.CREATE)
        {
            CreateObjectMenuCanvas.SetActive(false);
            GameObject.Destroy(spawned_declaring_object);
            spawned_declaring_object = null;
            return;
        }
        else
        {
            CreateObjectMenuCanvas.SetActive(true);
        }
        //render CREATE_OBJECT_BUTTONS
        if(!(selection_index >= 0 && selection_index <= selection_index_boundry))
        {
            Debug.Log("[XCreateObject] selection index out of bound, value is : " + selection_index);
        }
        int previous_selection_index = getPreviousSelectionIndex();
        int after_selection_index = getAfterSelectionIndex();
        GameObject middle_object_button 
            = CREATE_OBJECT_BUTTONS[selection_index];
        GameObject left_object_button 
            = CREATE_OBJECT_BUTTONS[previous_selection_index];
        GameObject right_object_button 
            = CREATE_OBJECT_BUTTONS[after_selection_index];
        //set transform
        middle_object_button.transform.position 
            = MiddleButton_POINT.transform.position;
        left_object_button.transform.position
            = LeftButton_POINT.transform.position;
        right_object_button.transform.position
            = RightButton_POINT.transform.position;
        //set color
        left_object_button.GetComponent<Image>().color = fading_button_color;
        right_object_button.GetComponent<Image>().color = fading_button_color;
        middle_object_button.GetComponent<Image>().color = selecting_button_color;
        //draw object
        switch(selection_index){
            case 0:
                {
                    if (spawned_declaring_object == null)
                    {
                        spawned_declaring_object = GameObject.Instantiate(XCreate_CUBE_DECLARE);
                        spawned_declaring_object.transform.position = ObjectSpawnerPoint.transform.position;
                    }
                    else
                    {
                        spawned_declaring_object.transform.position = ObjectSpawnerPoint.transform.position;
                    }
                    break;
                }
            case 1:
                {
                    if (spawned_declaring_object == null)
                    {
                        spawned_declaring_object = GameObject.Instantiate(XCreate_SPHERE_DECLARE);
                        spawned_declaring_object.transform.position = ObjectSpawnerPoint.transform.position;
                    }
                    else
                    {
                        spawned_declaring_object.transform.position = ObjectSpawnerPoint.transform.position;
                    }
                    break;
                }
            case 2:
                {
                    if (spawned_declaring_object == null)
                    {
                        spawned_declaring_object = GameObject.Instantiate(XCreate_PLANE_DECLARE);
                        spawned_declaring_object.transform.position = ObjectSpawnerPoint.transform.position;
                    }
                    else
                    {
                        spawned_declaring_object.transform.position = ObjectSpawnerPoint.transform.position;
                    }
                    break;
                }
            default:
                {
                    Debug.Log("[XCreateObject_draw_object] selection index out of bound, value is : " + selection_index);
                    break;
                }
        }
        
        //htcvive rightcontroller event handler
        if (RightControllerDriver.getInstance() != null)
        {
            //panel
            if (RightControllerDriver.getInstance().PanelRightDown())
            {
                addSelectionIndex();
                if (spawned_declaring_object != null)
                {
                    GameObject.Destroy(spawned_declaring_object);
                    spawned_declaring_object = null;
                }
                return;
            }
            if (RightControllerDriver.getInstance().PanelLeftDown())
            {
                diminishSelectionIndex();
                if (spawned_declaring_object != null)
                {
                    GameObject.Destroy(spawned_declaring_object);
                    spawned_declaring_object = null;
                }
                return;
            }
            //trigger
            if (RightControllerDriver.getInstance().TriggerDown())
            {
                switch (selection_index)
                {
                    case 0:
                        {
                            GameObject created_object = GameObject.Instantiate(XCreate_CUBE_PREFAB);
                            created_object.transform.position = ObjectSpawnerPoint.transform.position;
                            break;
                        }
                    case 1:
                        {
                            GameObject created_object = GameObject.Instantiate(XCreate_SPHERE_PREFAB);
                            created_object.transform.position = ObjectSpawnerPoint.transform.position;
                            break;
                        }
                    case 2:
                        {
                            GameObject created_object = GameObject.Instantiate(XCreate_PLANE_PREFAB);
                            created_object.transform.position = ObjectSpawnerPoint.transform.position;
                            break;
                        }
                    default:
                        {
                            Debug.Log("[XCreateObject_draw_object] selection index out of bound, value is : " + selection_index);
                            break;
                        }
                }
                return;
            }
        }
        
    }
}
