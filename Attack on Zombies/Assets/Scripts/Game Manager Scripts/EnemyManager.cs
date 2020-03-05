using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public static EnemyManager instance;
    [SerializeField]
    private GameObject zombie_prefab;

    public Transform[] spawn_points;

    [SerializeField]
    private int zombie_count;
    private int initial_zombie_count;
    public float grace_period = 10f;
    

    void Awake()
    {
        make_Instance();
    }

    void Start()
    {
        initial_zombie_count = zombie_count;
        Spawn_Enemies();
        StartCoroutine("CheckToSpawnEnemies");
    }

    void make_Instance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Spawn_Enemies()
    {
        int index = 0;

        for (int i = 0; i < zombie_count; i++)
        {
            if (index >= spawn_points.Length)
            {
                index = 0;
            }

            Instantiate(zombie_prefab, spawn_points[index].position, Quaternion.identity);
            index++;
        }
        zombie_count = 0;
    }

    public void Enemy_Died()
    {
        zombie_count++;
        if(zombie_count > initial_zombie_count)
        {
            zombie_count = initial_zombie_count;
        }
    }
    IEnumerator CheckToSpawnEnemies()
    {
        yield return new WaitForSeconds(grace_period);
        Spawn_Enemies();
        StartCoroutine("CheckToSpawnEnemies");
    }

    public void StopSpawning()
    {
        StopCoroutine("CheckToSpawnEnemies");
    }
}
