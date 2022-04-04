using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        Message.Publish(new PlaySoundRequested(GameSounds.GameOver, mainCamera.transform.position));
    }
}
