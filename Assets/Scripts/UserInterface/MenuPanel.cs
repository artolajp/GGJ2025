using UnityEngine;

public class MenuPanel : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }

        if (Input.anyKey)
        {
            GameData.ResetGameData();
            Actions.makeTransition?.Invoke("Scene_Game");
        }
    }
}