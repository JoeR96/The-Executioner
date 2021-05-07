using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private float explosionForce;
    [SerializeField] private Collider collider;
    [SerializeField] private ParticleSystem explosionParticle;
    private float explosionRadius = 3f;
    private MeshRenderer[] meshRenders;
    private Rigidbody rb;
    private bool RocketIsActive { get; set; }

    private void Awake()
    {
        meshRenders = GetComponentsInChildren<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        RocketIsActive = false;
    }

    /// <summary>
    /// Enable collision components once the rocket has been fired
    /// I would like to have done this cleaner however I only realised this later in the project and there was minimal reward if any for changing the layers across the game
    /// </summary>
    public void SetActiveRocket()
    {
        collider.enabled = true;
        RocketIsActive = true;
        gameObject.layer = 12;
    }

    /// <summary>
    /// This method is played to activate an explosion
    /// Play an explosion sound
    /// Disable the mesh and collision components to allow the rest of the sequence to run
    /// Freeze the rigidbody so the explosion doesn't move
    /// 
    /// </summary>
    private void Explosion()
    {
        AudioManager.Instance.PlaySound("RpgExplosion");
        foreach (var mesh in meshRenders)
        {
            mesh.enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
        }
        explosionParticle.Play();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        Vector3 explosionCentre = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionCentre, explosionRadius);
        InteractExplosionWithColliders(colliders, explosionCentre);
    }
    /// <summary>
    /// Loop through all collisions
    /// Check for a TakeDamage interface
    /// Check for a rigidbody on collision
    /// Add force if RB is not null
    /// </summary>
    private void InteractExplosionWithColliders(Collider[] colliders, Vector3 explosionCentre)
    {
        foreach (var hit in colliders)
        {
            var x = hit.GetComponentInParent<ITakeDamage>();
            x?.TakeDamage(125, explosionCentre);

            var collisionRb = hit.GetComponent<Rigidbody>();
            if (collisionRb != null)
            {
                collisionRb.AddExplosionForce(50f, transform.position, 25f, 1f);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(RocketIsActive && !other.collider.CompareTag("Rocket") && !other.collider.CompareTag("Player"))
            Explosion();
    }
}
