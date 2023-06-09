using System.Collections; 
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    private GameObject spawnedObject;
    private UIManager uiManager;
    private float speed = 15f;
    private float distance = 20f;
    private float interval = 2f;

    private void Start()
    {
        StartCoroutine(SpawnObject());
        uiManager = UIManager.instance;
    }

    private void Update()
    {
        if (spawnedObject != null)
        {
            MoveObject();
            CheckDistance();
        }
    }

    private IEnumerator SpawnObject()
    {
        while (true)
        {
            float parsedInterval;
            if (uiManager != null)
            {
                if(float.TryParse(uiManager.interval, out parsedInterval))
                {
                    interval= parsedInterval;   
                }
            }
            spawnedObject = Instantiate(prefab, transform.position, transform.rotation);
            yield return new WaitForSeconds(interval);
        }
    }

    private void MoveObject()
    {
        float parsedSpeed;
        if(uiManager!= null)
        {
            if(float.TryParse(uiManager.speed, out parsedSpeed))
            {
                speed = parsedSpeed;
            }
        }
        spawnedObject.transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void CheckDistance()
    {
        float parsedDistance;
        if(uiManager!= null)
        {
            if (float.TryParse(uiManager.distance, out parsedDistance))
            {
                distance = parsedDistance; 
            }
        }
        if (spawnedObject.transform.position.z >= distance)
        {
            Destroy(spawnedObject);
        }
    }
}

