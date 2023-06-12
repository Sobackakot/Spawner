
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PoolingObjects : MonoBehaviour
{
    public static List<PoolObjectInfo> ObjectPools = new List<PoolObjectInfo>();
    private GameObject _objectPoolEmptyHolder;
    private static GameObject _gameObjectsEmpty;
    public enum PoolType
    {
        GameObject,
        None
    }
    public static PoolType PoolingType;
    private void Awake()
    {
        SetupEmpties();
    }
    private void SetupEmpties()
    {
        _objectPoolEmptyHolder = new GameObject("Pooled Objects");
        _gameObjectsEmpty = new GameObject("GameObject");
        _gameObjectsEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);
    }
    public static GameObject SpawnObject (GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation, PoolType poolType = PoolType.None)
    {
        PoolObjectInfo pool = ObjectPools.Find(p=>p.LookupString==objectToSpawn.name);
        if (pool == null)
        {
            pool = new PoolObjectInfo() { LookupString= objectToSpawn.name };
            ObjectPools.Add(pool);
        }
        GameObject spawnableObject = pool.InactiveObject.FirstOrDefault();
        if (spawnableObject == null)
        {
            GameObject parentObject = SetParentObject(poolType);
            spawnableObject = Instantiate(objectToSpawn, spawnPosition, spawnRotation); 
            if(parentObject != null)
            {
                spawnableObject.transform.SetParent(parentObject.transform);
            } 
        }
        else
        {   
            spawnableObject.transform.position = spawnPosition;
            spawnableObject.transform.rotation = spawnRotation;
            pool.InactiveObject.Remove(spawnableObject);
            spawnableObject.SetActive(true);
        }
        return spawnableObject;
    }
    public static void ReturnObjectToPool(GameObject obj)
    {
        string goName = obj.name.Substring(0, obj.name.Length - 7);
        PoolObjectInfo pool = ObjectPools.Find(p=>p.LookupString==goName);
        if (pool == null)
        {
            Debug.Log("Trying to release an object that is not  pooled: " + obj.name);
        }
        else
        {
            obj.SetActive(false);
            pool.InactiveObject.Add(obj);
        }
    }
    private static GameObject SetParentObject(PoolType poolType)
    {
        switch (poolType)
        {
            case PoolType.GameObject:
                return _gameObjectsEmpty;
            case PoolType.None:
                return null;
            default:
                return null; 
        }
    }
  
}
public class PoolObjectInfo
{
    public string LookupString;
    public List<GameObject> InactiveObject = new List<GameObject>();
}
