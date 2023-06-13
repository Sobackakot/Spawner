using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    private Queue<GameObject> spawnedObjects = new Queue<GameObject>();
    private UIManager uiManager; 
    private float speed;
    private float distance;
    private float interval;

    private void Start()
    {
        StartCoroutine(SpawnObject());
        StartCoroutine(GeneratePrefabsAtIntervals());
        uiManager = UIManager.instance; 
    }

    private void LateUpdate()
    {
        MoveObject();
        CheckDistance();
    }
    private IEnumerator GeneratePrefabsAtIntervals()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            EnqueuePrefabs();
        }
    }
    private IEnumerator SpawnObject()
    {   
        while (true)
        { 
            if (uiManager != null)
            {
                interval = uiManager.interval;
            }
            spawnedObjects.Enqueue(PoolingObjects.SpawnObject(prefab, prefab.transform.position, Quaternion.identity, PoolingObjects.PoolType.GameObject));
            yield return new WaitForSeconds(interval);
        }
    }
    private void EnqueuePrefabs()
    {
        spawnedObjects.Enqueue(PoolingObjects.SpawnObject(prefab, prefab.transform.position, Quaternion.identity, PoolingObjects.PoolType.GameObject));

    }

    private void MoveObject()
    { 
        if (uiManager!= null)
        {
            speed = uiManager.speed;
        }
        if (spawnedObjects.Count > 0)
        {
            foreach(GameObject spawnedObject in spawnedObjects)
            {
                spawnedObject.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
        }
            
    }

    private void CheckDistance()
    { 
        if (uiManager!= null)
        { 
            distance = uiManager.distance;
        }
        if (spawnedObjects.Count > 0)
        {
            if (spawnedObjects.Peek().transform.position.z >= distance)
            {
                PoolingObjects.ReturnObjectToPool(spawnedObjects.Peek());
                spawnedObjects.Dequeue();
            }
        } 
    }
}

