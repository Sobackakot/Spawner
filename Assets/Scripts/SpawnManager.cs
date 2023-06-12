using System.Collections; 
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    private GameObject spawnedObject;
    private UIManager uiManager; 
    private float speed;
    private float distance;
    private float interval;

    private void Start()
    {
        StartCoroutine(SpawnObject());
        uiManager = UIManager.instance; 
    }

    private void LateUpdate()
    {
        if(spawnedObject != null)
        {
            MoveObject();
            CheckDistance();
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
            spawnedObject = PoolingObjects.SpawnObject(prefab, prefab.transform.position, Quaternion.identity, PoolingObjects.PoolType.GameObject); 
            yield return new WaitForSeconds(interval);
        }
    }

    private void MoveObject()
    { 
        if (uiManager!= null)
        {
            speed = uiManager.speed;
        }
        spawnedObject.transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void CheckDistance()
    { 
        if (uiManager!= null)
        { 
            distance = uiManager.distance;
        }
        if(spawnedObject.transform.position.z >= distance)
        {
            PoolingObjects.ReturnObjectToPool(spawnedObject);
        }
    }
}

