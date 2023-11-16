using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TEMPSTARTSCREENSCRIPT : MonoBehaviour
{
    public void StartBtn()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void BackBtn()
    {
        SceneManager.LoadScene("Level_5");
    }


}
