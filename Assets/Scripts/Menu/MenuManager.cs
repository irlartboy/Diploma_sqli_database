using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public GameObject SignInPanel;
    public GameObject CreatePanel;
    public GameObject ForgotPanel;
    public GameObject CodePanel;
    public GameObject newPasswordPanel;

    public GameObject resetButton;
    public InputField newPassword, checkPassword;
  
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
    public void NewPasswordBack()
    {
        newPasswordPanel.SetActive(false);
        ForgotPanel.SetActive(true);
    }
    public void GotCodeBack()
    {
        CodePanel.SetActive(false);
        ForgotPanel.SetActive(true);
    }
    public void GotCode()
    {
        ForgotPanel.SetActive(false);
        CodePanel.SetActive(true);
    }
    public void Next(InputField codeField)
    {
        if(codeField.text == Login.code)
        {
            CodePanel.SetActive(false);
            newPasswordPanel.SetActive(true);
        }
        else
        {
            CodePanel.SetActive(true);
        }
    }
    public void ShowReset()
    {
        if(newPassword.text.Length > 0 && newPassword.text == checkPassword.text)
        {
            resetButton.SetActive(true);
        }
        else
        {
            resetButton.SetActive(false);
        }
    }
}
