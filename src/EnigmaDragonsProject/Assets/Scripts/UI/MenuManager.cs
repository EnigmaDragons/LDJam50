using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    private Camera mainCamera;

    private void Awake() 
    {
        pausePanel.SetActive(false);
        mainCamera = Camera.main;
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
            Message.Publish(new PlaySoundRequested(GameSounds.MenuToggle, mainCamera.transform.position));
            panel.SetActive(!panel.activeSelf);
        }
    }
}