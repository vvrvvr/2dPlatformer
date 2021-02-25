using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCollider : MonoBehaviour
{

    void Start()
    {
        foreach(Transform child in transform)
        {
            child.GetComponent<Renderer>().enabled = false;
        }
    }
}
