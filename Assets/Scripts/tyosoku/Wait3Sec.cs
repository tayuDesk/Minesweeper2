using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Wait3Sec : MonoBehaviour
{
    public GameObject text;
    
    void Start()
    {
            
    }

    
    void Update()
    {
        
    }

    public void OnClickStartGame()
    {
        StartCoroutine("Wait3secAndGoGame");
    }

    public IEnumerator Wait3secAndGoGame()
    {
        text.GetComponent<Text>().text = "3";

        yield return new WaitForSeconds(1);

        text.GetComponent<Text>().text = "2";

        yield return new WaitForSeconds(1);

        text.GetComponent<Text>().text = "1";

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("main_tyosoku");
    }


}
