using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovT : MonoBehaviour
{
    private Transform cam;
    public Transform pers;
    private Vector3 offset;
    private float x;
    private float y;

    private void Awake()
    {
        cam = GetComponent<Transform>();
    }
    void Start()
    {
        offset = cam.position - pers.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        RotateCamera();
    }
    private Vector3 InputAxis()
    {
        x += Input.GetAxis("Mouse X") * 5f;
        y -= Input.GetAxis("Mouse Y") * 5f;
        y = Mathf.Clamp(y, -65, 65);
        return new Vector3(y, x, 0); 
    }
    private void RotateCamera()
    {
        cam.localEulerAngles = InputAxis();
        cam.position = cam.localRotation * offset + pers.position;
    }
}
