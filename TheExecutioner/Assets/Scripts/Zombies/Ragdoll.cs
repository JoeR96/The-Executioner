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
        ActivateRagDoll();
    }
    /// <summary>
    /// Turn off the ragdoll mode
    /// Loop through all rigidbodies in the game object
    /// These rigidbodies are the ragdoll colliders on each root component of the target
    /// Set all rigidbodies to kinematic
    /// enable the animator and navmesh agent
    /// Set a random animation time to desynchronize the timings of the enemy animations
    /// </summary>
    public void DeactivateRagdoll()
    {
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = true;
        } 
        // _navMeshAgent.enabled = true;
        // _animator.enabled = true;
        // _animator.SetFloat("AnimTime", Random.Range(0.0f,1.0f));
        
    }
    /// <summary>
    /// Turn on ragdoll mode
    /// Loop through all rigidbodies in the game object
    /// These rigidbodies are the ragdoll colliders on each root component of the target
    /// Set all rigidbodies kinematic to disabled
    /// disable the animator and navmesh agent
    /// </summary>
    public void ActivateRagDoll()
    {
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = false;
        }

        _navMeshAgent.enabled = false;
        _animator.enabled = false;
    }
    /// <summary>
    /// Apply a force on death
    /// Target the root component to return a rigidbody to add the force to
    /// Add a force from a supplied value 
    /// </summary>
    /// <param name="agentConfigDieForce"></param>
    public void ApplyForce(Vector3 agentConfigDieForce)
    {
        var rigidbody = _animator.GetBoneTransform(HumanBodyBones.Hips).GetComponent<Rigidbody>();
        rigidbody.AddForce(agentConfigDieForce,ForceMode.VelocityChange);
    }
}
