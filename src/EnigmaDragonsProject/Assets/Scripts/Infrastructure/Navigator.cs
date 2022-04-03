using UnityEngine;

[CreateAssetMenu]
public sealed class Navigator : ScriptableObject
{
    [SerializeField] private bool loggingEnabled;

    public void NavigateToVictoryScene() => NavigateTo("Victory");
    public void NavigateToGameOverScene() => NavigateTo("GameOver");
    public void NavigateToGameScene() => NavigateTo("GameSceneNewGarden");
    public void NavigateToMainMenu() => NavigateTo("MainMenu");
    public void NavigateToScene(string sceneName) => NavigateTo(sceneName);

    private void NavigateTo(string sceneName)
    {
        if (loggingEnabled)
            Log.Info($"Navigating to {sceneName}");
        Message.Publish(new NavigateToSceneRequested(sceneName));
    }
}
