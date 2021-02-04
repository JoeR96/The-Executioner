using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody[] rigidbodies;
    private void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        DeactivateRagdoll();
        _animator = GetComponent<Animator>();
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
}
