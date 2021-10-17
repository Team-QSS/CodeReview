using UnityEngine;

public class TryLogin : MonoBehaviour
{
    void Start()
    {
        PrintLog.instance.LogString += "Start LoginOAuth";
        LoginManager.LoginOAuth(CheckLoginResult);
    }

    private void CheckLoginResult(bool result)
    {
        PrintLog.instance.LogString += "Start CheckLoginResult";
        if (result)
        {
            PrintLog.instance.LogString += "[OAuth Login Success]";
            LoginManager.SetFirebaseCredential();
            LoginManager.SignInFirebase(CheckLoginSuccess);
        }
        else
        {
            PrintLog.instance.LogString += "[Login Failed] : LoginOAuth result is false";
        }
    }

    private void CheckLoginSuccess(bool success)
    {
        PrintLog.instance.LogString += "Start CheckLoginSuccess";
        if (success)
        {
            PrintLog.instance.LogString += $"[Login Success]";
        }
        else
        {
            PrintLog.instance.LogString += $"[Login Failed]";
        }
    }
}
