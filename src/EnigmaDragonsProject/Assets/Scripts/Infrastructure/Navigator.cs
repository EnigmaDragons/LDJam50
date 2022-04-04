using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public sealed class Navigator : ScriptableObject
{
    [SerializeField] private bool loggingEnabled;

    public void NavigateToVictoryScene() => NavigateTo("CreditsScene");
    public void NavigateToGameOverScene() => NavigateToSync("GameOver");
    public void NavigateToGameScene() => NavigateTo("GameSceneNewGarden");
    public void NavigateToMainMenu() => NavigateTo("MainMenu");
    public void NavigateToScene(string sceneName) => NavigateTo(sceneName);

    private void NavigateToSync(string sceneName)
    {
        if (loggingEnabled)
            Log.Info($"Navigating to {sceneName}");
        SceneManager.LoadScene(sceneName);
    }
    
    private void NavigateTo(string sceneName)
    {
        if (loggingEnabled)
            Log.Info($"Navigating to {sceneName}");
        Message.Publish(new NavigateToSceneRequested(sceneName));
    }
    
    public void ExitGame()
    {     
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
#else
        Application.Quit();
#endif
    }
}
