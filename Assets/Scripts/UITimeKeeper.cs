using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimeKeeper : MonoBehaviour
{

    bool CanAddTime = false;
    public GameObject Manager;

    float NowTimeFloat = 0.0f;
    int NowTimeInt = 0;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Manager.GetComponent<Manager>().ClikedFIrst)
        {
            CanAddTime = true;
        }

        if (Manager.GetComponent<Manager>().GameIsOver)
        {
            CanAddTime = false;
        }

        if (CanAddTime)
        {
            NowTimeFloat += Time.deltaTime;
            NowTimeInt = (int)NowTimeFloat;
        }
        GetComponent<Text>().text = NowTimeInt.ToString();
    }
}
