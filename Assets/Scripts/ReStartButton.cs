using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReStartButton : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void OnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
