using UnityEngine;
using SpaceShooter;

namespace TowerDefence
{
    public class LevelWaveCondition : MonoBehaviour, ILevelCondition
    {
        private bool isCompleted;

        public bool IsCompleted { get { return isCompleted; } }

        private void Start()
        {
            FindObjectOfType<EnemyWaveManager>().OnAllWavesDead += () => 
            { 
                isCompleted = true; 
            };
        }
    }
}