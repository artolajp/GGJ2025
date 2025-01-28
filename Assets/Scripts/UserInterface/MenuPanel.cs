using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GGJ2025;

public class MenuPanel : MonoBehaviour
{
    [SerializeField] private Button startButton;

    void Start()
    {
        startButton.onClick.AddListener(GoToGame);

        if (Input.anyKey)
        {
            GoToGame();
        }
    }

    private void GoToGame()
    {
        SceneManager.LoadScene("Scene_Game");
    }
}
