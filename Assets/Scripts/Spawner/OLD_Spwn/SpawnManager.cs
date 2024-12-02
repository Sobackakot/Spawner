using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    private Queue<GameObject> spawnedObjects = new Queue<GameObject>(); 
    private float speed;
    private float distance;
    private float interval;

    
}

