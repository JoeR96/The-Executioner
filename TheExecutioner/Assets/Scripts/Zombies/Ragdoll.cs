using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private NavMeshAgent _navMeshAgent;
    public Rigidbody[] rigidbodies;
    private void Awake()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        Debug.Log(_navMeshAgent);
        ActivateRagDoll();
    }
    
    public void DeactivateRagdoll()
    {
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = true;
        }


        _navMeshAgent.enabled = true;
        _animator.enabled = true;
       // _animator.SetFloat("AnimTime", Random.Range(0.0f,1.0f));
        
    }
    
    private IEnumerator RestartZombie()
    {
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = true;
        }

        _navMeshAgent.enabled = true;
        _animator.enabled = true;
        _animator.SetFloat("AnimTime", Random.Range(0.0f,1.0f));
        yield return new WaitForSeconds(2f);
        DeactivateRagdoll();
        
    }
    public void ActivateRagDoll()
    {
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = false;
        }

        _navMeshAgent.enabled = false;
        _animator.enabled = false;
    }

    public void ApplyForce(Vector3 agentConfigDieForce)
    {
        var rigidbody = _animator.GetBoneTransform(HumanBodyBones.Hips).GetComponent<Rigidbody>();
        rigidbody.AddForce(agentConfigDieForce,ForceMode.VelocityChange);
    }
}
