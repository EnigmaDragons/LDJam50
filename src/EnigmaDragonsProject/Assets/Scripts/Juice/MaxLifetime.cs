using System.Collections;
using UnityEngine;

public class MaxLifetime : MonoBehaviour
{
    [SerializeField] private float lifetime;
    
    private void Start()
    {
        StartCoroutine(Go());
    }

    private IEnumerator Go()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
