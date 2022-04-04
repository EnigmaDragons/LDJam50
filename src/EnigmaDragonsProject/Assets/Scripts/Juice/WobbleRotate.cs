
using UnityEngine;

public class WobbleRotate : MonoBehaviour
{
    [SerializeField] private Vector3 factor;

    private void Update()
    {
        var sine = Mathf.Sin(Time.deltaTime);
        transform.rotation = Quaternion.Euler(factor.x * sine, factor.y * sine, factor.z * sine);
    }
}
