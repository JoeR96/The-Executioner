using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyBase : MonoBehaviour, ITakeDamage, IDestroyLimb
{
    
    [SerializeField] protected ParticleSystem[] _particleSystem;
    [SerializeField] protected GameObject[] _limbs;
    [SerializeField] protected GameObject _rootComponent;
    [SerializeField] protected GameObject _zombieSkin;
    [SerializeField] protected Vector3 _explosionScaleSize;
    [SerializeField] protected float _explosionScaleTime;
    [SerializeField] protected float _maxHealth;
    private Dictionary<string,Transform> _destructibleLimbs = new Dictionary<string, Transform>();
    private Dictionary<String,ParticleSystem> _destructibleLimbParticle = new Dictionary<string, ParticleSystem>();
    protected Animator _animator;
    protected HealthSystem _healthSystem;
    
    
    protected NavMeshAgent _navMeshAgent;
    private StateMachine _stateMachine;
 
    protected bool _isMelee;

    private void Awake()
    {
        
        _healthSystem = new HealthSystem(_maxHealth,_maxHealth);
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        AnimatorStateInfo state = _animator.GetCurrentAnimatorStateInfo(0);
        _animator.Play(state.fullPathHash, -1, Random.Range(0f, 1f));
    }
    protected virtual void Start()
    {
        PopulateLimbDictionary();
    }
    protected void Update()
    {
        if (Input.GetKey(KeyCode.F1))
        {
            _navMeshAgent.enabled = true;
        }
        if (Input.GetKey(KeyCode.F5))
        {
            Debug.Log(IsAgentOnNavMesh());
        }

        // if (IsAgentOnNavMesh())
        // {
        //     ActivateZombie();
        // }
        
    }

    public void ActivateZombie( )
    {
        GetComponent<EnemyBase>().ActivateEnemy();
        GetComponent<Ragdoll>().DeactivateRagdoll();
    }
    
    public void TakeDamage(float damage)
    {
        _healthSystem.TakeDamage(damage);
        if (_healthSystem.CurrentHealth < 0)
        {
            PlayDeathParticles();
            _animator.SetBool("IsDead",true);
            StartCoroutine(WaitForSecond(2.2f ,-1f));
        }
    }
    
    private void PopulateLimbDictionary()
    {
        foreach (var gameobject in _limbs)
        {
            _destructibleLimbs.Add(gameobject.transform.name,gameobject.transform);
        }
    }
    
    public void DestroyLimb(string name,Vector3 direction)
    {
 
             Debug.Log(name);
        var transformTarget = _destructibleLimbs[name];
        Debug.Log(transformTarget);
        StartCoroutine(ScaleComponent(transformTarget, new Vector3(0,0,0), 0.25f));
        var t = _destructibleLimbs[name];
        var limb = GameManager.instance.LimbSpawner.ReturnLimb(name);
        limb.transform.position = _destructibleLimbs[name].transform.position;
        
        limb.GetComponent<Rigidbody>().AddForce(direction);
        if (name == "Head")
        {
            var random = Random.Range(0, 3);
            Debug.Log(random);
            if (random == 1)
            {
                _animator.SetBool("DiedByHeadshot", true);
                StartCoroutine(WaitForSecond(2f, 0.9f));
            }
        }
    }

    private IEnumerator WaitForSecond(float time,float targetSize)
    {
        yield return new WaitForSeconds(time);
        _navMeshAgent.baseOffset = targetSize;
    }
    private void PlayDeathParticles()
    {
        foreach (var particle in _particleSystem)
        {
            particle.Play();
        }
        
    }

    //Scale the limb to size 0 so it is removed from the body and the animation still functions
    private IEnumerator ScaleComponent(Transform target, Vector3 targetSize, float speed)
    {
        Debug.Log("Hi");
        var startSize = target.localScale;
        float timer = 0;
        float duration = _explosionScaleTime;
        while (timer < duration)
        {
            Debug.Log("Hi");
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
    private void ScaleRootComponents()
    {
        var root = _rootComponent.transform;
        StartCoroutine(ScaleComponent(root, _explosionScaleSize, 1f));
    }

    public void ActivateEnemy()
    {
        _animator.enabled = true;
        _navMeshAgent.enabled = true;
    }

    public bool IsAgentOnNavMesh()
    {
        float onMeshThreshold = 3;
        Vector3 agentPosition = transform.position;
        NavMeshHit hit;

        // Check for nearest point on navmesh to agent, within onMeshThreshold
        if (NavMesh.SamplePosition(agentPosition, out hit, onMeshThreshold, NavMesh.AllAreas))
        {
            // Check if the positions are vertically aligned
            if (Mathf.Approximately(agentPosition.x, hit.position.x)
                && Mathf.Approximately(agentPosition.z, hit.position.z))
            {
                // Lastly, check if object is below navmesh
                return agentPosition.y >= hit.position.y;
            }
        }

        return false;
    }
}
public interface ITakeDamage
{ 
    void TakeDamage(float damage);
}

public interface IDestroyLimb
{
    void DestroyLimb(string name,Vector3 direction);

}

