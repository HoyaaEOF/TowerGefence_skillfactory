using UnityEngine;
using UnityEngine.Events;
using SpaceShooter;

namespace TowerDefence
{

    public class TDPatrolController : AIController
    {
        private Path m_path;
        private int pathIndex;
        [SerializeField] private UnityEvent OnPathEnd;
        public void SetPath(Path newPath)
        {
            m_path = newPath;
            pathIndex = 0;
            SetPatrolBehaviour(m_path[pathIndex]);
        }

        protected override void GetNewPoint()
        {
            if (m_path.Length > ++pathIndex) SetPatrolBehaviour(m_path[pathIndex]);
            else
            {
                OnPathEnd.Invoke();
                Destroy(gameObject);
            }

            
        }
    }
}