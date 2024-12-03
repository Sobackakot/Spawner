using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerObjects : MonoBehaviour
{
    private List<MoveObjectPrefub> activeObjects = new List<MoveObjectPrefub>();
    private PoolSystem pool;
    private UIManager ui;
    private Transform spawnTransform;

    private float interval;
    private float distance;
    private float speedMove;

    private float timer;
    private void Awake()
    {
        pool = FindObjectOfType<PoolSystem>();
        ui = FindObjectOfType<UIManager>();
        spawnTransform = GetComponent<Transform>(); 
    }

    private void Start()
    {
        activeObjects.AddRange(FindObjectsOfType<MoveObjectPrefub>());
    }
    private void Update()
    {  
        interval = ui.interval;
        distance = ui.distance;
        speedMove = ui.speed;
        if (interval <= 0) return;

            timer += Time.deltaTime;
        if(timer >= interval)
        {
            GetFromPoolAfterDelay(); 
            timer = 0;  
        } 
        for(int i = activeObjects.Count -1; i >=0; i--)
        {
            MoveObjectPrefub activeObject = activeObjects[i];
            bool isFinal = activeObject.UpdateMove();
            if (isFinal)
            {
                pool.ReturnToPool(activeObjects[i]);
                activeObjects.RemoveAt(i);
            }
        }
    }
    private void GetFromPoolAfterDelay()
    {
        MoveObjectPrefub newObject = pool.Spawn(spawnTransform.position, spawnTransform.rotation);
        newObject.InitializeMove(speedMove, distance);
        activeObjects.Add(newObject);
    } 
}
