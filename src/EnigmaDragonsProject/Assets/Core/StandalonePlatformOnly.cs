using UnityEngine;

public sealed class StandalonePlatformOnly : MonoBehaviour
{
    void Awake()
    {
        if (Application.isMobilePlatform || Application.platform == RuntimePlatform.WebGLPlayer)
        {
            Log.Info("Destroying Object", gameObject);
            Destroy(gameObject);
        }
    }
}
