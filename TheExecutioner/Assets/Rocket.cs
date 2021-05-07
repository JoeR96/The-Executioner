using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private float explosionForce;
    [SerializeField] private Collider collider;
    [SerializeField] private ParticleSystem explosionParticle;
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

    public void SetActiveRocket()
    {
        collider.enabled = true;
        RocketIsActive = true;
        gameObject.layer = 12;
    }

    private void Explosion()
    {
        AudioManager.Instance.PlaySound("RpgExplosion");
        foreach (var mesh in meshRenders)
        {
            mesh.enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
        }
            
        rb.constraints = RigidbodyConstraints.FreezeAll;
        Vector3 explosionCentre = transform.position;
        float explosionRadius = 3f;
        Collider[] colliders = Physics.OverlapSphere(explosionCentre, explosionRadius);
        foreach (var hit in colliders)
        {
            var x = hit.GetComponentInParent<ITakeDamage>();
            x?.TakeDamage(125,explosionCentre);
            
            var rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                AudioManager.Instance.PlaySound("RpgExplosion");
                explosionParticle.Play();
                GetComponent<MeshRenderer>().enabled = false;
                rb.AddExplosionForce(50f,transform.position,25f,1f);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(RocketIsActive && !other.collider.CompareTag("Rocket") && !other.collider.CompareTag("Player"))
            Explosion();
    }
}
