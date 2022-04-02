using TMPro;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject buttonGroup;
    [SerializeField] private GameObject optionsGroup;
    [SerializeField] private TextMeshProUGUI titleText;
    
    private void Start() 
    {
        Reset();
    }

    private void OnEnable() 
    {
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
        titleText.text = "Options";
        buttonGroup.SetActive(false);
        optionsGroup.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Reset() 
    {
        titleText.text = "Paused";
        buttonGroup.SetActive(true);
        optionsGroup.SetActive(false);
    }
}
