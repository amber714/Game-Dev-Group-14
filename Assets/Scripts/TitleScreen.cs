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
<<<<<<< HEAD
=======
        // SceneManager.LoadScene("SampleScene");
>>>>>>> dfb6fa21caf5ac55d623d26e91828bd5dfdf9181
        SceneManager.LoadScene("stage_green");
    }

    public void returnButton()
    {
        SceneManager.LoadScene("stage_title");
    }
     


    public static void GameOver()
    {
        ExitButton();
        //SceneManager.LoadScene("GameOver");
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
