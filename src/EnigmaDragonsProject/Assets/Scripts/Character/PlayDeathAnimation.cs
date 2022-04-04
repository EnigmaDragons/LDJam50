using System.Collections;
using UnityEngine;

public class PlayDeathAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private float _delay = 1;
    private float _duration = 2.4f;
    
    private void Start()
    {   
        StartCoroutine(Go());
    }

    private IEnumerator Go()
    {
        yield return new WaitForSeconds(_delay);
        animator.SetTrigger("IsDead");
        yield return new WaitForSeconds(_duration);
        animator.enabled = false;
    }
}