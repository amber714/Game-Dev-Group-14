using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TitleScreen : MonoBehaviour
{
    public void Setup()
    {
        //gameObject.SetActive(true);
    }
    public void StartButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void returnButton()
    {
        SceneManager.LoadScene("titleScreen");
    }



    public static void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void ExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); 
#endif
    }
}
