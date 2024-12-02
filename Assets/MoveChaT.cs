using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChaT : MonoBehaviour
{
    private Rigidbody rb;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>(); 
    }

     
    void FixedUpdate()
    {
        Moving();
    }
    private Vector3 InputAxis()
    {
        return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }
    private void Moving()
    {
        rb.MovePosition(rb.position + InputAxis() * 6 * Time.deltaTime);
    }
}
