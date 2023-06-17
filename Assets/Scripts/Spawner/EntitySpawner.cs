using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefence;

namespace SpaceShooter
{

    public class EntitySpawner : Spawner
    {
        [SerializeField] private GameObject[] m_EntityPrefabs;

        
        /*var e = Instantiate(prefabToSpawn);

        [SerializeField] private Path m_Path;

        if(e.TryGetComponent<TDPatrolController>(out var AIController))
{
            ai.SetPath(m_path);
        }
    if(e.TryGetComponent<Enemy>(out var en))
    {
        en.Use(m_EnemySettings[Random.Range(0,m_EnemySettings.Length)]);
    }*/
        protected override GameObject GenerateSpawnedEntity()
        {
            return Instantiate(m_EntityPrefabs[Random.Range(0, m_EntityPrefabs.Length)]);
        }
    }
}