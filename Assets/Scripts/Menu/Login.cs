using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

#region Sending Emails
using System;
using System.Net;
using System.Net.Security;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
#endregion

public class Login : MonoBehaviour
{
    public InputField username;
    public InputField email;
    public InputField password;

    public InputField sIUsername;
    public InputField sIPassword;
    public Text sInotification;

    public Text cAnotification;

    public Text fANotification;
    public InputField fAemail;

    public Text codeText;

    public GameObject signInPanel;
    public GameObject NewPasswordPanel;


    private string characters = "0123456789abcdefghijklmnopqrstuvwxABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public static string code = "";
    private string _username;

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
    IEnumerator ForgotUser(InputField email)
    {
        Debug.Log(email.text);
        string checkemailURL = "http://localhost/nsirpg/CheckEmail.php";
        WWWForm form = new WWWForm();
        form.AddField("email_Post", email.text);
        UnityWebRequest webRequest = UnityWebRequest.Post(checkemailURL,form);
        yield return webRequest.SendWebRequest();
        if (webRequest.downloadHandler.text == "User not found")
        {
            fANotification.text = webRequest.downloadHandler.text;
        }
        else
        {
            _username = webRequest.downloadHandler.text;
            SendEmail(email);
        }
    }
    void SendEmail(InputField email)
    {
        CreateCode();
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress("sqlunityclasssydney@gmail.com");
        mail.To.Add(email.text);
        mail.Subject = "NSIRPG Password Reset";
        mail.Body = "Hello " + _username+"\nReset using this code: "+ code;
        // Connect to google
        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        // able to send through ports
        smtpServer.Port = 25;
        // login to google
        smtpServer.Credentials = new NetworkCredential("sqlunityclasssydney@gmail.com", "sqlpassword") as ICredentialsByHost;
        smtpServer.EnableSsl = true;

        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate cert, X509Chain chain, SslPolicyErrors policyErrors)
        { return true; };
        // send message
        smtpServer.Send(mail);
        Debug.Log("Sending Email");
    }
    
    public void SubmitEmail(InputField email)
    {
        fANotification.text = "";
        StartCoroutine(ForgotUser(email));
    }
    void CreateCode()
    {
        for (int i = 0; i < 6; i++)
        {
            int a = UnityEngine.Random.Range(0, characters.Length);
            code = code + characters[a];
        }

        Debug.Log(code);
    }
    public void ResetPassword(InputField password)
    {
        StartCoroutine(PasswordReset(password));
    }
    IEnumerator PasswordReset(InputField password)
    {
        string resetURL = "http://localhost/nsirpg/updatepassword.php";
        WWWForm form = new WWWForm();
        form.AddField("password_Post", password.text);
        form.AddField("username_Post", _username);
        UnityWebRequest webRequest = UnityWebRequest.Post(resetURL, form);
        yield return webRequest.SendWebRequest();
        if (webRequest.downloadHandler.text == "Password Changed")
        {
            //load to Main Page
            NewPasswordPanel.SetActive(false);
            signInPanel.SetActive(true);
            Debug.Log("Changed");
        }
     
    }
}
