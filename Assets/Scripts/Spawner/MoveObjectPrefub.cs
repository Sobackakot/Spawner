
using UnityEngine;

public class MoveObjectPrefub : MonoBehaviour
{
    private Transform objTransform;  
    private float speedMove;
    private float maxDistance;
    private Vector3 startPoint; 
    private void Awake()
    {
        objTransform = GetComponent<Transform>();
        startPoint = objTransform.position;
    } 
    public bool UpdateMove()
    { 
        objTransform.position += objTransform.forward * speedMove * Time.deltaTime;
        float distance = Vector3.Distance(startPoint, objTransform.position);
        if(distance > maxDistance)
        { 
           return true;
        }
        return false;
    }
    public void InitializeMove(float speed, float maxDistance)
    {
        speedMove = speed;
        this.maxDistance = maxDistance;
    }
}
