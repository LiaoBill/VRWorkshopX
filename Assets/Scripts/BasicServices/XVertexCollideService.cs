using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XVertexCollideService : MonoBehaviour {
    private static XVertexCollideService thisInstance;
    public static XVertexCollideService getInstance()
    {
        return thisInstance;
    }
    private void Awake()
    {
        thisInstance = this;
    }
    private GameObject current_touching_gameobject;
    public GameObject getCurrentTouchingGameObject()
    {
        return current_touching_gameobject;
    }
    private void OnTriggerEnter(Collider other)
    {
        current_touching_gameobject = other.gameObject;
    }
    private void OnTriggerStay(Collider other)
    {
        current_touching_gameobject = other.gameObject;
    }
    private void OnTriggerExit(Collider other)
    {
        current_touching_gameobject = null;
    }
    // Use this for initialization
    void Start () {
        current_touching_gameobject = null;
    }
}
