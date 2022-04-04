using UnityEngine;

public class ButtonHover : MonoBehaviour {
    private Camera mainCamera;

    private void Start() 
    {
        mainCamera = Camera.main;
    }

    public void OnButtonHover()
    {
        Debug.Log("Button hover");
        Message.Publish(new PlaySoundRequested(GameSounds.ButtonHover, mainCamera.transform.position));
    }
}
