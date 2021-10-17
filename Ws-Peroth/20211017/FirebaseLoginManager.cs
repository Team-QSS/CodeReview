using Firebase.Auth;

public class FirebaseLoginManager
{
    public FirebaseAuth Auth { get; set; }
    public FirebaseUser User { get; set; }
    public Credential Credential { get; set; }

    public Credential GetCredential(string authCode)
    {
        PrintLog.instance.LogString += "Start GetCredential()";
        Auth = FirebaseAuth.DefaultInstance;
        Credential = PlayGamesAuthProvider.GetCredential(authCode);

        if (Credential == null)
        {
            PrintLog.instance.LogString += "[Credential Failed] : credential is null";
        }

        PrintLog.instance.LogString += $"Provider = {Credential.Provider}";
        return Credential;
    }

    /// <summary>
    /// nextAction : Login 완료 시 실행할 함수. bool 매개변수는 SignInWithCredentialAsync 작업의 성공 여부를 뜻함.
    /// </summary>
    /// <param name="nextAction"></param>
    public void SignInFirebase(Credential credential, System.Action<bool> nextAction)
    {
        PrintLog.instance.LogString += $"Start LoginFirebase()";

        
        Auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            // 아래의 부분을 호출하지 않음
            // 그러나 Firebase Console을 확인해 보면 계정이 성공적으로 추가되어있음
            User = task.Result;
            nextAction(task.IsCompleted);
        });

        // Auth.SignInWithCredentialAsync(credential).ContinueWith(task => nextAction(task.IsCompleted));
    }

    /// <summary>
    /// 로그인이 성공적으로 완료 되었는지 확인하기 위한 예제 함수
    /// </summary>
    public void DisplayUserData(bool success)
    {
        PrintLog.instance.LogString += "Start DisplayUserData()";

        if (!success)
        {
            PrintLog.instance.LogString += "[SignIn Failed] : success is false";
            return;
        }

        User = Auth.CurrentUser;

        if (User != null)
        {
            string playerName = User.DisplayName;

            // The user's Id, unique to the Firebase project.
            // Do NOT use this value to authenticate with your backend server, if you
            // have one; use User.TokenAsync() instead.
            string uid = User.UserId;
            PrintLog.instance.LogString += $"UID = {uid}";
            PrintLog.instance.LogString += $"PlayerName = {playerName}";
        }
        else
        {
            PrintLog.instance.LogString += "[SignIn Failed] : User is null";
        }
    }
}
