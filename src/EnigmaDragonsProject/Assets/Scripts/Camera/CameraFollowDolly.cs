using UnityEngine;

public class CameraFollowDolly : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Vector3 offset;
    
    private void LateUpdate()
    {
        transform.position = target.transform.position + offset;
    }
}
