using UnityEngine;
using UnityEngine.SceneManagement;  //LoadScene을 사용하기 위해 SceneMnagement를 임포트함
public class TitleDirector : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.anyKeyDown)
        {
            SceneManager.LoadScene("MenuScene");
            //SceneManager.LoadScene(0);
            //마우스가 클릭된 것을 감지하면, SceneManager 클래스의 LoadScene 메서드를 사용해 씬 전환
        }
    }
}
