using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public GameObject SignInPanel;
    public GameObject CreatePanel;
    public GameObject ForgotPanel;
  
    public void CreateClick()
    {
        SignInPanel.SetActive(false);
        CreatePanel.SetActive(true);
    }
    public void ForgotClick()
    {
        SignInPanel.SetActive(false);
        ForgotPanel.SetActive(true);
    }
    public void CreateBack()
    {
        
        CreatePanel.SetActive(false);
        SignInPanel.SetActive(true);
    }
    public void ForgotBack()
    {

        ForgotPanel.SetActive(false);
        SignInPanel.SetActive(true);
    }

}
