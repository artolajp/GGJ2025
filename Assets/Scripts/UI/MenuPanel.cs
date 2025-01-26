using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GGJ2025
{
    public class MenuPanel : MonoBehaviour
    {
        [SerializeField] private Button startButton;

        void Start()
        {
            startButton.onClick.AddListener(GoToGame);
        }
        private void GoToGame()
        {
            SceneManager.LoadScene("Game");
        }
    }
}
