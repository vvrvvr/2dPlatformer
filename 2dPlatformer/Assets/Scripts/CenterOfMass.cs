using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterOfMass : MonoBehaviour
{
    [SerializeField] float xCenter;
    [SerializeField] float yCenter;
    private Rigidbody2D rb;


    void Start()
    {
        Vector2 com = new Vector2(xCenter, yCenter);
        rb = GetComponent<Rigidbody2D>();
        rb.centerOfMass = com;
    }
}
