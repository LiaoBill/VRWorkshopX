using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XGroupList{
    private static XGroupList thisInstance;
    public static XGroupList getInstance()
    {
        if (thisInstance == null)
        {
            thisInstance = new XGroupList();
        }
        return thisInstance;
    }
    private XGroupList()
    {
        group_list = new List<GameObject>();
        father_object = null;
    }
    private List<GameObject> group_list;
    private GameObject father_object;
    public void add(GameObject new_object)
    {
        if(group_list.Count == 0)
        {
            group_list.Add(new_object);
            father_object = new_object;
        }
        else
        {
            for (int i = 0; i != group_list.Count; i++)
            {
                if (group_list[i].Equals(new_object))
                {
                    return;
                }
            }
            group_list.Add(new_object);
            new_object.transform.SetParent(father_object.transform,true);
        }
    }
    public void dismissList()
    {
        if (!(group_list.Count == 0 || group_list.Count == 1))
        {
            for(int i = 1; i != group_list.Count; i++)
            {
                group_list[i].transform.SetParent(null, true);
            }
        }
        group_list.Clear();
        father_object = null;
    }
    public GameObject getFatherFromGroupSon(GameObject son)
    {
        for(int i = 0; i != group_list.Count; i++)
        {
            if (group_list[i].Equals(son))
            {
                return group_list[0];
            }
        }
        return null;
    }
    public void deleteGameObject(GameObject target)
    {
        for (int i = 0; i != group_list.Count; i++)
        {
            if (group_list[i].Equals(target))
            {
                if(i == 0)
                {
                    //father
                    if(group_list.Count != 1)
                    {
                        group_list[1].transform.SetParent(null, true);
                        father_object = group_list[1];
                        for (int j = 2; j != group_list.Count; j++)
                        {
                            group_list[j].transform.SetParent(father_object.transform, true);
                        }
                    }
                    else
                    {
                        father_object = null;
                    }
                }
                else
                {
                    group_list[i].transform.SetParent(null, true);
                }
                group_list.RemoveAt(i);
            }
        }
    }
    public int getListCount()
    {
        return group_list.Count;
    }
}
