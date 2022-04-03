using UnityEngine;

public class Rotating : MonoBehaviour
{
    [SerializeField] private float rotationsPerMinute;
    
    private void Update()
    {
        transform.Rotate(0f,6f*rotationsPerMinute*Time.deltaTime,0f);
    }
}