using System;
using UnityEngine;

public class CarnivorousFlowerController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private float _timeUntilNextAttack;
    
    private GameObject _player;
    private float _attackCooldown = 3f;
    private float _attackRange = 5;
    
    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
    }

    private void Start()
    {
        animator.SetInteger("battle", 1);
    }

    private void Update()
    {
        if (_player == null)
            return;
        
        transform.LookAt(_player.transform);
        _timeUntilNextAttack = Math.Max(0, _timeUntilNextAttack - Time.deltaTime);
        if (_timeUntilNextAttack <= 0)
        {
            if (Vector3.Distance(_player.transform.position, transform.position) < _attackRange)
            {
                animator.Play("attack2");
                _timeUntilNextAttack = _attackCooldown;
            }
        }
    }
}
