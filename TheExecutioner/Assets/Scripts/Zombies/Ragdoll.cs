using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public Rigidbody[] rigidbodies;
    private void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        _animator = GetComponent<Animator>();
        ActivateRagDoll();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.F2))
        {
            DeactivateRagdoll();
            
        }if (Input.GetKey(KeyCode.F3))
        {
            ActivateRagDoll();
        }
    }
    public void DeactivateRagdoll()
    {
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = true;
        }
    
        _animator.enabled = true;
    }

    public void ActivateRagDoll()
    {
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = false;
        }

        _animator.enabled = false;
    }

    public void ApplyForce(Vector3 agentConfigDieForce)
    {
        var rigidbody = _animator.GetBoneTransform(HumanBodyBones.Hips).GetComponent<Rigidbody>();
        rigidbody.AddForce(agentConfigDieForce,ForceMode.VelocityChange);
    }
}
