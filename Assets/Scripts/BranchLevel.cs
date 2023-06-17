using System;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    [RequireComponent(typeof(MapLevel))]
    public class BranchLevel : MonoBehaviour
    {
        [SerializeField] private Text m_PointText;
        [SerializeField] private MapLevel m_RootLevel;
        [SerializeField] private int m_NeedPoints = 3;
        [SerializeField] private GameObject m_LockPanel;

        private void Start()
        {
            if (m_NeedPoints <= MapCompletion.Instance.TotalScore)
            {
                m_LockPanel.gameObject.SetActive(false);
                GetComponent<MapLevel>().Initialise();
            }
        }

        internal void TryActivate()
        {

            gameObject.SetActive(m_RootLevel.IsComplete);
            if (m_NeedPoints > MapCompletion.Instance.TotalScore)
            {
                m_PointText.text = m_NeedPoints.ToString();
            }
            else
            {
                m_LockPanel.gameObject.SetActive(false);
                GetComponent<MapLevel>().Initialise();
            }
        }
    }
}