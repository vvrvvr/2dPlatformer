using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crate : MonoBehaviour
{
    private Vector2 com = new Vector2(0.5f, 0.35f);
    private Rigidbody2D rb;
    

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        rb.centerOfMass = com;
    }
   
}
