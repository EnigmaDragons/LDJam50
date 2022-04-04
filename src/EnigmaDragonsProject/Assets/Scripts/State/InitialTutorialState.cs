using UnityEngine;

[CreateAssetMenu]
public class InitialTutorialState : ScriptableObject
{
    [SerializeField] private bool hasEverViewedTutorial;
    
    public bool HasEverViewedTutorial => hasEverViewedTutorial;
    public void Clear() => hasEverViewedTutorial = false;
    public void MarkComplete() => hasEverViewedTutorial = true;
}
