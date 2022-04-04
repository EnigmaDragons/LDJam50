using UnityEngine;

public class WindowPointerController : MonoBehaviour
{
    [SerializeField] private WindowPointer pointer;
    
    public void SpawnPointer(Vector3 targetPosition)
    {
        var instance = Instantiate(pointer, new Vector3(0, 0, 0), Quaternion.identity, gameObject.transform);
        instance.Show(targetPosition);
    }
}
