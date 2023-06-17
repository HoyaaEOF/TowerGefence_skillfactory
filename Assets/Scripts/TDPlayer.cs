using UnityEngine;
using SpaceShooter;
using System;

namespace TowerDefence
{

    public class TDPlayer : Player
    {
        public static new TDPlayer Instance { get { return Player.Instance as TDPlayer; } }

        [SerializeField] private int m_Mana;
        public int Mana => m_Mana;

        private event Action<int> OnGoldUpdate;
        public void GoldUpdateSubscribe(Action<int> act)
        {
            OnGoldUpdate += act;
            act(Instance.m_gold);
        }
        public event Action<int> OnLifeUpdate;
        public void LifeUpdateSubscribe(Action<int> act)
        {
            OnLifeUpdate += act;
            act(Instance.NumLives);
        }
        public event Action<int> OnManaUpdate;
        public void ManaUpdateSubscribe(Action<int> act)
        {
            OnManaUpdate += act;
            act(Instance.m_Mana);
        }

        [SerializeField] private int m_gold;

        public void ChangeGold(int change)
        {
            m_gold += change;
            OnGoldUpdate(m_gold);
        }

        public void ChangeLife(int change)
        {
            TakeDamage(change);
            OnLifeUpdate(NumLives);
        }

        public void ChangeMana(int change)
        {
            m_Mana += change;
            OnManaUpdate(m_Mana);
        }

        [SerializeField] private Tower m_towerPrefab;
        public void TryBuild(TowerAsset towerAsset, Transform m_buildSite)
        {
            var tower = Instantiate(m_towerPrefab, m_buildSite.position, Quaternion.identity);
            tower.GetComponentInChildren<SpriteRenderer>().sprite = towerAsset.sprite;
            tower.transform.Find("Turret").GetComponent<Turret>().m_TurretProperties = towerAsset.turretProperties;
            Destroy(m_buildSite.gameObject);
            tower.Use(towerAsset);
            ChangeGold(-(towerAsset.goldCost));
        }

        [SerializeField] private UpgradeAsset healthUpgrade;

        private void Start()
        {
            var level = Upgrades.GetUpgradeLevel(healthUpgrade);
            TakeDamage(-level * 5);
        }

        public static void GoldUpdateUnsubscribe(Action<int> action)
        {
            Instance.OnGoldUpdate -= action;
        }

        public static void LifeUpdateUnsubscribe(Action<int> action)
        {
            Instance.OnLifeUpdate -= action;
        }

        public static void ManaUpdateUnsubscribe(Action<int> action)
        {
            Instance.OnManaUpdate -= action;
        }
    }
}