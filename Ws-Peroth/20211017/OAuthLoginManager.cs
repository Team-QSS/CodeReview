using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class OAuthLoginManager
{
    public string AuthCode { get; set; }

    /// <summary>
    /// nextAction : Authenticate 완료 시 실행할 함수. bool 매개변수는 Authenticate 작업의 성공 여부를 뜻함.
    /// </summary>
    /// <param name="nextAction"></param>
    public void AuthenticateOAuth(System.Action<bool> nextAction)
    {
        PrintLog.instance.LogString += "Start GooglePlayLogin";
        var config =
           new PlayGamesClientConfiguration.Builder()
           .RequestServerAuthCode(false /* Don't force refresh */)
           .Build();

        PlayGamesPlatform.InitializeInstance(config);
        var x = PlayGamesPlatform.Activate();

        UnityEngine.Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                AuthCode =
                PlayGamesPlatform.Instance.GetServerAuthCode();
                PrintLog.instance.LogString += "[Google Login Success]";
            }
            else
            {
                AuthCode = "";
                PrintLog.instance.LogString += "[Google Login Failed] : success is false";
            }

            nextAction(success);
        });
    }
}
