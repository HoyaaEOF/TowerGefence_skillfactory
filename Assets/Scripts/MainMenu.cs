using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace TowerDefence
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button continueButton;
        [SerializeField] private GameObject confirmationWindow;
        private void Start()
        {
            continueButton.interactable = FileHandler.HasFile(MapCompletion.filename);
            if (!FileHandler.HasFile(MapCompletion.filename))
            {
                continueButton.GetComponentInChildren<Text>().color = new Color(255,255,255,150);
            }
            else
            {
                continueButton.GetComponentInChildren<Text>().color = new Color(255, 255, 255, 255);
            }
        }

        public void NewGame()
        {
            if (FileHandler.HasFile(MapCompletion.filename))
            {
                confirmationWindow.SetActive(true);
            }
            else
            {
                ResetProgressDataAndStart();
            }
        }

        public void Continue()
        {
            SceneManager.LoadScene(1);
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void ResetProgressDataAndStart()
        {
            FileHandler.Reset(MapCompletion.filename);
            FileHandler.Reset(Upgrades.filename);

            SceneManager.LoadScene(1);
        }
        
    }
}