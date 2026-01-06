using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchUI : MonoBehaviour
{
    
    [Serializable] 
    public struct uiElements{
        public string description;
        public GameObject[] BigDataUI;
    }
   
    public uiElements[] uies;

    // Start is called before the first frame update
    private void Start()
    {
        SetUIElement(0);
    }
    
    public void SetUIElement(int idx)
    {
        for (int i = 0; i < uies.Length; i++)
        {
            uiElements currentUIE = uies[i];
            foreach (GameObject uiObj in currentUIE.BigDataUI)
            {
                if (uiObj != null)
                {
                    uiObj.SetActive(i == idx);
                }
            }
        }
 

    }
    public void ResponseTest() {
        Debug.Log("Hi!!");
    }
}
