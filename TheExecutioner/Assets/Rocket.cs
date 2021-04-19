using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private float explosionForce;

    [SerializeField] private ParticleSystem explosionParticle;

    private Rigidbody rb;
    private bool RocketIsActive { get; set; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        RocketIsActive = false;
    }

    public void SetActiveRocket()
    {
        RocketIsActive = true;
        gameObject.layer = 12;
    }


    // Start is called before the first frame update
    private void Explosion()
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
        Vector3 explosionCentre = transform.position;
        float explosionRadius = 3f;
        Collider[] colliders = Physics.OverlapSphere(explosionCentre, explosionRadius);
        foreach (var hit in colliders)
        {
           Debug.Log(hit.name);
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

    private void OnTriggerEnter(Collider other)
    {
        if(RocketIsActive && !other.CompareTag("Rocket"))
            Explosion();
    }
}
