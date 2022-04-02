using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject buttonGroup;
    [SerializeField] private GameObject optionsGroup;
    
    private void Start() 
    {
        Reset();
    }

    private void OnDisable() 
    {
        Reset();
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
    }

    public void OpenOptions()
    {
        buttonGroup.SetActive(false);
        optionsGroup.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Reset() 
    {
        buttonGroup.SetActive(true);
        optionsGroup.SetActive(false);
    }
}
