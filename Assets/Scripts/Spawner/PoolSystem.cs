 
using System.Collections.Generic;
using UnityEngine;

public class PoolSystem : MonoBehaviour
{ 
    private Transform poolTransform;
    [SerializeField] private GameObject objectPrefub; 
    [SerializeField] private int poolSize = 100; 
    Queue<MoveObjectPrefub> pool = new Queue<MoveObjectPrefub>();
     
    private void Awake()
    { 
        poolTransform = GetComponent<Transform>();
        InitializePool(); 
    }
    // Spawn an object from the pool
    public MoveObjectPrefub Spawn(Vector3 position, Quaternion rotation)
    {
        MoveObjectPrefub obj;

        if (pool.Count > 0)
        {
            obj = pool.Dequeue();
        }
        else
        {
            obj = CreateNewObject();
        }

        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.gameObject.SetActive(true);

        return obj;
    }

    // Return an object back to the pool
    public void ReturnToPool(MoveObjectPrefub obj)
    {
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }


    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            MoveObjectPrefub obj = CreateNewObject();
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    // Create a new object if pool runs out
    private MoveObjectPrefub CreateNewObject()
    {
        GameObject obj = Instantiate(objectPrefub);
        obj.transform.SetParent(poolTransform); // Organize pool in hierarchy
        return obj.GetComponent<MoveObjectPrefub>();
    } 
}
