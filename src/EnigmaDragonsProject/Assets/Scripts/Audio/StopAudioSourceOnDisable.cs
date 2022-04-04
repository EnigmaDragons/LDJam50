using UnityEngine;

public class StopAudioSourceOnDisable : MonoBehaviour
{
    [SerializeField] private AudioSource src;

    private void OnDisable()
    {
        src.Stop();
    }
}
