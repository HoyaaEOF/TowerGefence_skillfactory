using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{

    public class NextWaveGUI : MonoBehaviour
    {
        [SerializeField] private Text bonusAmount;
        private EnemyWaveManager manager;
        private float timeToNextWave;

        void Start()
        {
            manager = FindObjectOfType<EnemyWaveManager>();
            EnemyWave.OnWavePrerape += (float time) =>
            {
                timeToNextWave = time;
            };
        }

        private void Update()
        {
            var bonus = (int)timeToNextWave;
            if (bonus < 0) bonus = 0;
            bonusAmount.text = bonus.ToString();
            timeToNextWave -= Time.deltaTime;
        }

        public void CallWave()
        {
            manager.ForceNextWave();
        }
    }
}