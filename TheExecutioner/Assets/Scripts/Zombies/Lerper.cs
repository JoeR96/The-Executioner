using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Lerper : EnemyBase
{
    [SerializeField] protected float _jumpTriggerDistance;
    [SerializeField] protected float _meleeTriggerDistance;
    [SerializeField] protected float _jumpSpeed;
    private bool _isJumping;

    private void Update()
    {
        base.Update();
        SetState();
    }
    // Update is called once per frame
    protected void SetState()
    {
        
        if (CheckDistance() <= _jumpTriggerDistance && !_isJumping)
        {
            SetJumpState();
        }
        if (CheckDistance() <= _meleeTriggerDistance && !_isMelee)
        {
            SetMeleeAttackState();
        }
    }

    private void SetJumpState()
    {
        _isJumping = true;
        _animator.SetBool("IsInJumpRange", true);
        _navMeshAgent.speed = _jumpSpeed;
        
    }

    private void PlayDeathParticles()
    {
        foreach (var particle in _particleSystem)
        {
            particle.Play();
        }
        StartCoroutine(Destroy());
    }
    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1f);
        var root = _rootComponent.transform;
        StartCoroutine(ScaleComponent(root, new Vector3(0,0,0), _jumpSpeed));
        Destroy(gameObject);
    }
    private void ScaleRootComponents()
    {
        var root = _rootComponent.transform;
        StartCoroutine(ScaleComponent(root, _explosionScaleSize, _jumpSpeed));
    }
    private IEnumerator ScaleComponent(Transform target, Vector3 targetSize, float speed)
    {
        var startSize = target.localScale;
        float timer = 0;
        float duration = _explosionScaleTime;
        while (timer < duration)
        {
            float percentage = Mathf.Min(timer / duration, 1);
            timer += Time.deltaTime;
            target.localScale = Vector3.Lerp(startSize, targetSize,percentage);
            yield return null;
        }

        if (_isMelee)
        {
            PlayDeathParticles();
        }
    }
    private void SetExplosionScaleState()
    {

        _navMeshAgent.speed = 0f;
        ScaleRootComponents();
    }

    private void SetMeleeAttackState()
    {
        SetExplosionScaleState();
        _isMelee = true;
        _animator.SetBool("IsInMeleeRange",true);
        _animator.Play("ZombieHeadButt");
        
    }
    private void JumpToTarget(Transform targetTransform)
    {
        
    }
    private float CheckDistance()
    {
        float dist = 1f;
       // var dist = Vector3.Distance(transform.position, _playerTransform.position);
        return dist;
    }


    // public void DestroyLimb(string name,Vector3 direction)
    // {
    //
    //     var transformTarget = _destructibleLimbs[name];
    //     StartCoroutine(ScaleComponent(transformTarget, direction, 0.25f));
    //     var t = _destructibleLimbs[name];
    //     var limb = GameManager.instance.LimbSpawner.ReturnLimb(name);
    //     limb.transform.position = _destructibleLimbs[name].transform.position;
    //     limb.GetComponent<Rigidbody>().AddForce(direction);
    //
    // }

}
