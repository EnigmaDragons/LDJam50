using UnityEngine;

public class FixedGlobalRotation : MonoBehaviour
{
    [SerializeField] private Vector3 eulerAngles;

    private Quaternion _rotation;
    
    private void Awake()
    {
        _rotation = Quaternion.Euler(eulerAngles);
    }
    
    private void LateUpdate()
    {
        transform.rotation = _rotation;
    }
}

