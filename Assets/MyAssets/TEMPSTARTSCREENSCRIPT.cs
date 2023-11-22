using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TEMPSTARTSCREENSCRIPT : MonoBehaviour
{
    public void IntroBtn()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void StartBtn()
    {
        SceneManager.LoadScene("Introduction");
    }

    public void BackBtn()
    {
        SceneManager.LoadScene("Level_5");
    }


}
