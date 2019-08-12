using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class Login : MonoBehaviour
{
    public InputField username;
    public InputField email;
    public InputField password;

    public InputField sIUsername;
    public InputField sIPassword;
    public Text sInotification;

    public Text cAnotification;

    
    IEnumerator CreateUser(string username, string email, string password)
    {
        string createUserURL = "http://localhost/nsirpg/InsertUser.php";
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("email", email);
        form.AddField("password", password);
        UnityWebRequest webRequest = UnityWebRequest.Post(createUserURL, form);
        yield return webRequest.SendWebRequest();
        cAnotification.text = webRequest.downloadHandler.text;

      
    }
    IEnumerator SignIn(string username, string password)
    {
        string loginURL = "http://localhost/nsirpg/Login.php";
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        UnityWebRequest webRequest = UnityWebRequest.Post(loginURL, form);
        yield return webRequest.SendWebRequest();
        sInotification.text = webRequest.downloadHandler.text;
        Debug.Log(webRequest.downloadHandler.text);
        if (webRequest.downloadHandler.text == "Successful")
        {
            SceneManager.LoadScene(sceneBuildIndex: 1);
        }
    }
    public void CreateNewUser()
    {
        cAnotification.text = "";
       StartCoroutine(CreateUser(username.text, email.text, password.text));
    }

    public void SignIn()
    {
        sInotification.text = "";
        StartCoroutine(SignIn(sIUsername.text, sIPassword.text));
      
    }

}
