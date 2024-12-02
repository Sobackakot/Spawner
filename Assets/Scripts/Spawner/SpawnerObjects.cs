using System.Collections;
using UnityEngine;

public class SpawnerObjects : MonoBehaviour
{
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
    }
    private void GetFromPoolAfterDelay()
    {
        MoveObjectPrefub newObject = pool.Spawn(spawnTransform.position, Quaternion.identity);
        newObject.InitializeMove(speedMove, distance);
        Debug.Log("Spawn");
    } 
}
