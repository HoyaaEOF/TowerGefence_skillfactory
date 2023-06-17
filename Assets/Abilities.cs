using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using SpaceShooter;
using UnityEngine.EventSystems;

namespace TowerDefence
{
    public class Abilities : MonoSingleton<Abilities>
    {
        [Serializable]
        public class FireAbility
        {
            [SerializeField] public int m_Cost = 5;
            [SerializeField] private int m_Damage = 5;
            public int upgradeLevel;

            public void ChangeDamagPower()
            {
                m_Damage += 5 * (upgradeLevel - 1);
            }

            public void Use()
            {
                ClickProtection.Instance.Activate((Vector2 v) =>
                {
                    Vector3 position = v;
                    position.z = -Camera.main.transform.position.z;
                    position = Camera.main.ScreenToWorldPoint(position);
                    foreach (var collider in Physics2D.OverlapCircleAll(position, 5))
                    {
                        if (collider.transform.parent.TryGetComponent<Enemy>(out var enemy))
                        {
                            enemy.TakeDamage(m_Damage, TDProjectile.DamageType.Magic);
                        }
                    }
                });
            }

            public void Buy()
            {
                Use();
                TDPlayer.Instance.ChangeMana(-m_Cost);
            }
        }

        [Serializable]
        public class TimeAbility
        {
            [SerializeField] public int m_Cost = 10;
            [SerializeField] private float m_CoolDown = 15f;
            [SerializeField] private float m_Duration = 5f;
            public int upgradeLevel;
            [HideInInspector] public bool isCooldown;

            public void ChangeCoolDownPeriod()
            {
                m_CoolDown -= 5 * (upgradeLevel - 1);
            }

            public void Use()
            {
                void Slow(Enemy ship)
                {
                    ship.GetComponent<SpaceShip>().HalfMaxLinearVelocity();
                }

                foreach (var ship in FindObjectsOfType<SpaceShip>())
                {
                    ship.HalfMaxLinearVelocity();
                }

                EnemyWaveManager.OnEnemySpawn += Slow;

                IEnumerator Restore()
                {
                    yield return new WaitForSeconds(m_Duration);
                    foreach (var ship in FindObjectsOfType<SpaceShip>())
                    {
                        ship.RestoreMaxLinearVelocity();
                    }
                    EnemyWaveManager.OnEnemySpawn -= Slow;
                }

                Instance.StartCoroutine(Restore());

                IEnumerator TimeAbilityButton()
                {
                    Instance.TimeButton.interactable = false;
                    isCooldown = true;
                    yield return new WaitForSeconds(m_CoolDown);
                    if(TDPlayer.Instance.Mana >= m_Cost)Instance.TimeButton.interactable = true;
                    isCooldown = false;
                }

                Instance.StartCoroutine(TimeAbilityButton());
            }
            public void Buy()
            {
                Use();
                TDPlayer.Instance.ChangeMana(-m_Cost);
            }
        }

        [SerializeField] private FireAbility m_FireAbility;
        public void BuyFireAbility() => m_FireAbility.Buy();
        
        [SerializeField] private TimeAbility m_TimeAbility;
        public void BuyTimeAbility() => m_TimeAbility.Buy();

        [SerializeField] private Button TimeButton;
        [SerializeField] private Image TargetCircle;

        [SerializeField] private UpgradeAsset m_FireAbilityAsset;
        [SerializeField] private GameObject m_FireAbilityGO;
        [SerializeField] private UpgradeAsset m_TimeAbilityAsset;
        [SerializeField] private GameObject m_TimeAbilityGO;

        private void Start()
        {
            TDPlayer.Instance.ManaUpdateSubscribe(ManaAndLevelStatusCheck);
            m_FireAbilityGO.GetComponentInChildren<Text>().text = m_FireAbility.m_Cost.ToString();
            m_TimeAbilityGO.GetComponentInChildren<Text>().text = m_TimeAbility.m_Cost.ToString();

            if (Upgrades.GetUpgradeLevel(m_FireAbilityAsset) < 1)
            {
                m_FireAbilityGO.SetActive(false);
            }
            if (Upgrades.GetUpgradeLevel(m_TimeAbilityAsset) < 1)
            {
                m_TimeAbilityGO.SetActive(false);
            }

            m_FireAbility.upgradeLevel = Upgrades.GetUpgradeLevel(m_FireAbilityAsset);
            m_TimeAbility.upgradeLevel = Upgrades.GetUpgradeLevel(m_TimeAbilityAsset);

            m_FireAbility.ChangeDamagPower();
            m_TimeAbility.ChangeCoolDownPeriod();
        }

        private void ManaAndLevelStatusCheck(int mana)
        {
            if (Upgrades.GetUpgradeLevel(m_FireAbilityAsset) > 0)
            {
                if (mana >= m_FireAbility.m_Cost != m_FireAbilityGO.GetComponentInChildren<Button>().interactable)
                {
                    m_FireAbilityGO.GetComponentInChildren<Button>().interactable = !m_FireAbilityGO.GetComponentInChildren<Button>().interactable;
                    m_FireAbilityGO.GetComponentInChildren<Text>().color = m_FireAbilityGO.GetComponentInChildren<Button>().interactable ? Color.white : Color.red;
                }
            }
            if (Upgrades.GetUpgradeLevel(m_TimeAbilityAsset) > 0)
            {
                if (mana >= m_TimeAbility.m_Cost)
                {
                    if (!m_TimeAbility.isCooldown)
                    {
                        TimeButton.interactable = true;
                    }
                }
                else
                {
                    if (!m_TimeAbility.isCooldown)
                    {
                        TimeButton.interactable = false;
                    }
                }

                m_TimeAbilityGO.GetComponentInChildren<Text>().color = (mana >= m_TimeAbility.m_Cost) ? Color.white : Color.red;
            }
        }

        private void OnDestroy()
        {
            TDPlayer.ManaUpdateUnsubscribe(ManaAndLevelStatusCheck);
        }
    }
}