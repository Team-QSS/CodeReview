public class LoginManager : Singleton<LoginManager>
{
    public static OAuthLoginManager googlePlayLoginManager = new OAuthLoginManager();
    public static FirebaseLoginManager firebaseLoginManager = new FirebaseLoginManager();

    protected override void Awake()
    {
        dontDestroyOnLoad = true;
        base.Awake();
    }

    /// <summary>
    /// nextAction : LoginOAuth 완료 시 실행할 함수. bool 매개변수는 Authenticate 작업의 성공 여부를 뜻함.
    /// </summary>
    /// <param name="nextAction"></param>
    public static void LoginOAuth(System.Action<bool> nextAction)
    {
        googlePlayLoginManager.AuthenticateOAuth(nextAction);
    }

    /// <summary>
    /// Credential을 가져오기 위해서 먼저 OAuth에 로그인 할 필요가 있음. LoginOAuth() 호출
    /// </summary>
    /// <returns></returns>
    public static void SetFirebaseCredential()
    {
        var authCode = googlePlayLoginManager.AuthCode;
        firebaseLoginManager.GetCredential(authCode);
    }

    /// <summary>
    /// nextAction : SignInFirebase 완료 시 실행할 함수. bool 매개변수는 Authenticate 작업의 성공 여부를 뜻함.
    /// SetFirebaseCredential을 통해 먼저 Credential을 설정할 필요가 있음. SetFirebaseCredential() 호출
    /// </summary>
    /// <param name="nextAction"></param>
    public static void SignInFirebase(System.Action<bool> nextAction)
    {
        firebaseLoginManager.SignInFirebase(firebaseLoginManager.Credential, nextAction);
    }
}
