using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class EndUiManager : MonoBehaviour
{
    public TMP_Text TextTotalScore = null;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TextTotalScore.text = "Total Score : " + UiManager.Instance.Score.ToString();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("MenuScene");
    }
}