using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{

    public partial class TowerBuyControl : MonoBehaviour
    {

        [SerializeField] private TowerAsset m_TowerAsset;
        public void SetTowerAsset(TowerAsset towerAsset) { m_TowerAsset = towerAsset; }
        [SerializeField] private Text m_text;
        [SerializeField] private Button m_button;
        [SerializeField] private Transform m_buildSite;
        public void SetBuildSite(Transform value) 
        {
            m_buildSite = value;  
        }

        /*private void Awake()
        {
            TDPlayer.GoldUpdateSubscribe(GoldStatusCheck);
        }*/

        //[SerializeField] private UpgradeAsset towerCostUpgrade;

        private void Start()
        {
            //var level = Upgrades.GetUpgradeLevel(towerCostUpgrade);
            //m_TowerAsset.goldCost -= 5 * level;
            TDPlayer.Instance.GoldUpdateSubscribe(GoldStatusCheck);
            m_text.text = m_TowerAsset.goldCost.ToString();
            m_button.GetComponent<Image>().sprite = m_TowerAsset.GUIsprite;
        }

        private void GoldStatusCheck(int gold)
        {
            if (gold >= m_TowerAsset.goldCost != m_button.interactable)
            {
                m_button.interactable = !m_button.interactable;
                m_text.color = m_button.interactable ? Color.white : Color.red;
            }
        }

        public void Buy()
        {
            TDPlayer.Instance.TryBuild(m_TowerAsset, m_buildSite);
            BuildSite.HideControls();
        }

        private void OnDestroy()
        {
            TDPlayer.GoldUpdateUnsubscribe(GoldStatusCheck);
        }
    }
}