using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{

    public class BuyUpgrade : MonoBehaviour
    {
        [SerializeField] private Image m_UpgradeIcon;
        [SerializeField] private Text level, costText;
        private int costNumder = 0;
        [SerializeField] private Button buyButton;
        [SerializeField] public UpgradeAsset asset;

        public void Initialize()
        {
            m_UpgradeIcon.sprite = asset.sprite;
            var savedLevel = Upgrades.GetUpgradeLevel(asset);
            if (savedLevel >= asset.costByLevel.Length)
            {
                level.text = $"{savedLevel} lvl (max)";
                buyButton.interactable = false;
                buyButton.transform.Find("Image (1)").gameObject.SetActive(false);
                buyButton.transform.Find("Text").gameObject.SetActive(false);
                costText.gameObject.SetActive(false);
                costNumder = int.MaxValue;
            }
            else
            {
                costNumder = asset.costByLevel[savedLevel];
                costText.text = costNumder.ToString();
                level.text = (savedLevel + 1).ToString() + " lvl";
            }
        }

        public void CheckCost(int money)
        {
            buyButton.interactable = money >= costNumder;
        }

        public void Buy()
        {
            Upgrades.BuyUpgrade(asset);
            Initialize();
        }
    }
}