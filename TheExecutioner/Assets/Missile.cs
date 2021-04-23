using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] protected Rigidbody _rb;
    [SerializeField] protected GameObject _explosionParticle;
    [SerializeField] protected GameObject _firingParticle;
    [SerializeField] protected ParticleSystem _particleSystem;
    [SerializeField] private string _impactSound;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //rb.MovePosition(transform.position + _rb.velocity * Time.deltaTime );
        //_rb.AddForce(-transform.position);
    }

    private void OnCollisionEnter(Collision other)
    {
        
        //play particle effect

        //AudioManager.Instance.PlaySound(_impactSound);
        //add explosion force
    }
    
    private void Explosion()
    {
        AudioManager.Instance.PlaySound("RpgExplosion");
        _particleSystem.Play();
        GetComponent<MeshRenderer>().enabled = false;
        Vector3 explosionCentre = transform.position;
        float explosionRadius = 25f;
        Collider[] colliders = Physics.OverlapSphere(explosionCentre, explosionRadius);
        foreach (var hit in colliders)
        {
            var x = GetComponent<ITakeDamage>();
            if (x != null)
                x.TakeDamage(125f,transform.up);
            var rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(50f,transform.position,25f,1f);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Explosion();
    }
}
