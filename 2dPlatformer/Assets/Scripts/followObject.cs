using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followObject : MonoBehaviour
{
    [SerializeField] private Transform objectToFollow;

    void Update()
    {
        var follow = new Vector3(objectToFollow.position.x, objectToFollow.position.y, transform.position.z);
        transform.position = follow;
    }
}
