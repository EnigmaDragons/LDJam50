using UnityEngine;

public class GoToSceneOnStart : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private Navigator navigator;

    private void Start() => navigator.NavigateToScene(sceneName);
}