using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    void Start()
    {
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