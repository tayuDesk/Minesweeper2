using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PrefsTyosoku : MonoBehaviour
{
    int TimeLimit = 60;

    public Text TimeText = null;
    public int NowTime = 0;

    public Text CountText = null;
    public int NowCount = 0;

    public Manager M_cs;

    bool isCalledOnce = false;
    bool CanSave = true;
   
    void Start()
    {
        NowTime = PlayerPrefs.GetInt("NOW_TIME", TimeLimit);
        NowCount = PlayerPrefs.GetInt("NOW_COUNT", 0);
    }

    void OnDestroy()
    {
        if (CanSave)
        {
            PlayerPrefs.SetInt("NOW_TIME", NowTime);
            PlayerPrefs.Save();

            PlayerPrefs.SetInt("NOW_COUNT", NowCount);
            PlayerPrefs.Save();
        }
    }


    void Update()
    {
        TimeText.text = "TIME: " + NowTime.ToString();
        CountText.text = "Cleared Count: " + NowCount.ToString();

        if (!isCalledOnce)
        {
            isCalledOnce = true;
            StartCoroutine("DecreaseTime");
        }

        if (M_cs.clear)
        {
            NowCount++;
            SceneManager.LoadScene("main_tyosoku");
        }
    }

    public IEnumerator DecreaseTime()
    {
        for (int i = 0; i < TimeLimit; i++)
        {
            yield return new WaitForSeconds(1);
            NowTime--;
            
            if (NowTime <= 0)
            {
                M_cs.failed = true;
                GameObject.Find("ReturnButton").transform.position = new Vector3(1000, 600, 0);
                PlayerPrefs.DeleteKey("NOW_TIME");
                PlayerPrefs.DeleteKey("NOW_COUNT");
                CanSave = false;
                break;
            }
        }
    }

    public void OnClickReturnButtonTyosoku()
    {
        CanSave = false;
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("start");
    }

    public void OnClickResetPrefsButton()
    {
        
        PlayerPrefs.DeleteKey("NOW_TIME");
        PlayerPrefs.DeleteKey("NOW_COUNT");
    }

}
