using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlowerBehaviour : MonoBehaviour
{
    [SerializeField] private FloatVariable ProjectileCooldown;

    private Animator FlowerAnimator;

    private static readonly int BOOL_IS_PUSHING = Animator.StringToHash("IsPushing");
    private static readonly int BOOL_IS_SHOOTING = Animator.StringToHash("IsShooting");
    private static readonly int FLOAT_SPEED = Animator.StringToHash("Speed");

    void Awake()
    {
        FlowerAnimator = GetComponent<Animator>();
    }

    // The methods are going to be called from game event listeners and player input

    public void FlowerAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            FlowerAttackAnimation();
        }
    }
    
    public void FlowerAttackOnTouch()
    {
        FlowerAttackAnimation();
    }

    private void FlowerAttackAnimation()
    {
        FlowerAnimator.SetBool(BOOL_IS_SHOOTING, true);
        FlowerAnimator.SetFloat(FLOAT_SPEED, (1 / ProjectileCooldown.Value));
    }

    public void IsPushing()
    {
        FlowerAnimator.SetBool(BOOL_IS_PUSHING, true);
    }

    public void StopPushing()
    {
        FlowerAnimator.SetBool(BOOL_IS_PUSHING, false);
    }

    public void StopShooting()
    {
        FlowerAnimator.SetBool(BOOL_IS_SHOOTING, false);
    }
}
