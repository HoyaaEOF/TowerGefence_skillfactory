using UnityEngine;
using SpaceShooter;

namespace TowerDefence
{

    public class Tower : MonoBehaviour
    {
        [SerializeField] private float m_Radius;
        [SerializeField] private float m_Lead;
        private Turret[] turrets;
        private Rigidbody2D target = null;

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
                if (Vector3.Distance(target.transform.position, transform.position) <= m_Radius)
                {
                    foreach (var turret in turrets)
                    {
                        turret.transform.up = target.transform.position - turret.transform.position + (Vector3)target.velocity * m_Lead;
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
                    target = enter.transform.root.GetComponent<Rigidbody2D>();
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