using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    private Animator animator;
    private string levelToLoad = "Scene_Game";

    protected virtual void OnEnable()
    {
        Actions.makeTransition += FadeToLevel;

        animator = GetComponent<Animator>();
    }

    protected virtual void OnDisable()
    {
        Actions.makeTransition -= FadeToLevel;
    }

    private void FadeToLevel(string levelName)
    {
        levelToLoad = levelName;

        if (animator != null)
        {
            animator.SetTrigger("FadeOut");
        }
    }

    public void OnFadeOutCompleted()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}