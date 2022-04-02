using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    private void Start() {
        pausePanel.SetActive(false);
    }

    private void Update()
    {
        Keyboard keyboard = Keyboard.current;
        Gamepad gamepad = Gamepad.current;

        if (keyboard == null) return;
        HandleKeyPress(keyboard.escapeKey.wasPressedThisFrame, pausePanel);

        if (gamepad == null) return;
        HandleKeyPress(gamepad.startButton.wasPressedThisFrame, pausePanel);
    }

    private void HandleKeyPress(bool wasKeyPressed, GameObject panel)
    {
        if (wasKeyPressed)
        {
            if (panel.activeSelf)
            {
                panel.SetActive(false);
                return;
            }
            panel.SetActive(true);
        }
    }
}