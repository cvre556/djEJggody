using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneDirector : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void StartTutorial()
    {
        SceneManager.LoadScene("TutorialScene");
    }
}