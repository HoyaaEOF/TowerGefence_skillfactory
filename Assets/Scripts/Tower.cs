using UnityEngine;
using SpaceShooter;

namespace TowerDefence
{

    public class Tower : MonoBehaviour
    {
        [SerializeField] private float m_Radius;
        private Turret[] turrets;
        private Destructible target = null;

        private void Start()
        {
            turrets = GetComponentsInChildren<Turret>();
        }

        public void Use(TowerAsset asset)
        {
            var buildSite = GetComponentInChildren<BuildSite>();
            buildSite.SetBuildableTowers(asset.m_UpgradesTo);
        }

        private void Update()
        {
            if (target)
            {
                Vector2 targetVector = target.transform.position - transform.position;
                if (targetVector.magnitude <= m_Radius)
                {
                    foreach (var turret in turrets)
                    {
                        turret.transform.up = targetVector;
                        turret.Fire();
                    }
                }
                else
                {
                    target = null;
                }
            }
            else
            {
                var enter = Physics2D.OverlapCircle(transform.position, m_Radius);
                if (enter)
                {
                    target = enter.transform.root.GetComponent<Destructible>();
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;

            Gizmos.DrawWireSphere(transform.position, m_Radius);
        }
    }
}