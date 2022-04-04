using Sirenix.OdinInspector;
using UnityEngine;

public class IntroController : MonoBehaviour
{
    [SerializeField] private InitialTutorialState tutorialState;
    [SerializeField] private GameObject target;

    private void Start()
    {
        Time.timeScale = 1;
        target.SetActive(!tutorialState.HasEverViewedTutorial);
    }

    [Button]
    public void Reset() => tutorialState.Clear();
}
