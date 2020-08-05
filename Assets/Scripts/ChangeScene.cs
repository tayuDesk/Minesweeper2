using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    
   

    public void OnClickGameStart()
    {
        SceneManager.LoadScene("select");
    }

    public void OnClickReturnButton()
    {
        SceneManager.LoadScene("start");
    }

    public void OnClickSyokyu()
    {
        SceneManager.LoadScene("main_syokyu");
    }

    public void OnClickTyukyu()
    {
        SceneManager.LoadScene("main_tyukyu");
    }

    public void OnClickJokyu()
    {
        SceneManager.LoadScene("main_jokyu");
    }

    public void OnClick2020()
    {
        SceneManager.LoadScene("main_2020");
    }

    public void OnClickTyosoku()
    {
        SceneManager.LoadScene("taiki_tyosoku");
    }

}
