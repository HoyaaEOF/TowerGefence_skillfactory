using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class TextUpdate : MonoBehaviour
    {
        public enum UpdateSource
        {
            Gold,
            Life,
            Mana
        }

        public UpdateSource source; 

        private Text m_text;

        void Start()
        {
            m_text = GetComponent<Text>();

            switch (source)
            {
                case UpdateSource.Gold:
                    TDPlayer.Instance.GoldUpdateSubscribe(UpdateText);
                    break;
                case UpdateSource.Life:
                    TDPlayer.Instance.LifeUpdateSubscribe(UpdateText);
                    break;
                case UpdateSource.Mana:
                    TDPlayer.Instance.ManaUpdateSubscribe(UpdateText);
                    break;
            }

            
        }

        // Update is called once per frame
        private void UpdateText(int life)
        {
            m_text.text = life.ToString();
        }

        private void OnDestroy()
        {
            TDPlayer.GoldUpdateUnsubscribe(UpdateText);
            TDPlayer.LifeUpdateUnsubscribe(UpdateText);
            TDPlayer.ManaUpdateUnsubscribe(UpdateText);
        }
    }
}