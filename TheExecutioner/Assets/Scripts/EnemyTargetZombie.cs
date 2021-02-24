using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetZombie : EnemyBase
{
    [SerializeField] private Animator _environmentAnimator;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        _aiAgent.StateMachine.ChangeState(StateId.Target);
    }

    protected override void Die(Vector3 direction)
    {
 
        _environmentAnimator.SetBool("TargetIsHit", true);
    }
}
