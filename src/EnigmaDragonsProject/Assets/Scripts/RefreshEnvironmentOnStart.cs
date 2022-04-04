
using UnityEngine;

public class RefreshEnvironmentOnStart : MonoBehaviour
{
    private void Start()
    {
        DynamicGI.UpdateEnvironment();
    }
}
