using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followObject : MonoBehaviour
{
    [SerializeField] private Transform objectToFollow;
    [SerializeField] private float yOffset;
    private float smoothTime = 0.2f;
    private Vector3 _velocity = Vector3.zero;
    private void Awake()
    {
        transform.position = new Vector3(objectToFollow.position.x, objectToFollow.position.y - yOffset, transform.position.z);
    }

    private void FixedUpdate()
    {
        var follow = new Vector3(objectToFollow.position.x, objectToFollow.position.y - yOffset, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, follow, ref _velocity, smoothTime);
    }
}
