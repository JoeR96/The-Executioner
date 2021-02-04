using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private Rigidbody[] rigidbodies;
    private void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        ActivateRagDoll();
    }
    public void ActivateRagDoll()
    {
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = true;
        }

        _animator.enabled = true;
    }

    public void DeactivateRagdoll()
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
