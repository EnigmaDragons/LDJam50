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

    private void OnEnable() {
        Time.timeScale = 0;
    }

    private void OnDisable() 
    {
        Reset();
        Time.timeScale = 1;
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

    public void Reset() 
    {
        buttonGroup.SetActive(true);
        optionsGroup.SetActive(false);
    }
}
