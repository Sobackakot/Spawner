
using UnityEngine;

public class MoveObjectPrefub : MonoBehaviour
{
    private Transform objTransform; 
    private PoolSystem poolSystem;
    private float speedMove;
    private float maxDistance;
    private Vector3 startPoint; 
    private void Awake()
    {
        objTransform = GetComponent<Transform>();
        startPoint = objTransform.position;
    }
    private void Start()
    {
        poolSystem = PoolSystem.instance;
    }
    private void Update()
    { 
        objTransform.position += objTransform.forward * speedMove * Time.deltaTime;
        float distance = Vector3.Distance(startPoint, objTransform.position);
        if(distance > maxDistance)
        {
            poolSystem.ReturnToPool(this);
            Debug.Log("Destroy");
        }
    }
    public void InitializeMove(float speed, float maxDistance)
    {
        speedMove = speed;
        this.maxDistance = maxDistance;
    }
}
