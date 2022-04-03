
using UnityEngine;

public class PlayerMasterOrchestrator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerWater water;

    void Awake()
    {
        water.SetAnimator(animator);
    }
}