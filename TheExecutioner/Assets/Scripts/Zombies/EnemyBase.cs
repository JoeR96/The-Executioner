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

    protected AiAgent _aiAgent;
    protected NavMeshAgent _navMeshAgent;
    private StateMachine _stateMachine;
 
    protected bool _isMelee;

    private void Awake()
    {
        _aiAgent = GetComponent<AiAgent>();
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
    
    bool isOnMesh;
    protected void Update()
    {
        
        if (Input.GetKey(KeyCode.F1))
        {
            _navMeshAgent.enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            Debug.Log(IsAgentOnNavMesh());
        }

        
        if (IsAgentOnNavMesh() && isOnMesh == false)
        {
            isOnMesh = true;
              ActivateZombie();
          }
        
    }
    
    public void ActivateZombie( )
    {
        _navMeshAgent.enabled = true;
        GetComponent<Ragdoll>().DeactivateRagdoll();
        
    }
    
    public void TakeDamage(float damage, Vector3 direction)
    {
        _healthSystem.TakeDamage(damage);
        if (_healthSystem.CurrentHealth < 0)
        {
            Die(direction);
            PlayDeathParticles();
        }
    }
    
    private void PopulateLimbDictionary()
    {
        foreach (var gameobject in _limbs)
        {
            _destructibleLimbs.Add(gameobject.transform.name,gameobject.transform);
        }
    }


    protected virtual void Die(Vector3 direction)
    {
        DeathState deathState = _aiAgent.StateMachine.GetState(StateId.DeathState) as DeathState;
        if (deathState != null) deathState.Direction = direction;
        _aiAgent.StateMachine.ChangeState(StateId.DeathState);
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
    
    public LimbParticleLocation[] _LimbParticleLocations;
    public ParticleSystem _bloodSplat;
    private void PlayParticleAtLimb(string name)
    {
        var limb = Array.Find(_LimbParticleLocations, 
            zombieLimb => zombieLimb.Name == name);
        var t = limb.Location;
        
        _bloodSplat.transform.SetParent(t);
        _bloodSplat.transform.SetPositionAndRotation(t.position,t.rotation);
        _bloodSplat.Play();

    }

    [Serializable]
    public struct LimbParticleLocation
    {
        public string Name;
        public Transform Location;
    }
    public void DestroyLimb(string name,Vector3 direction)
    {
        var transformTarget = _destructibleLimbs[name];
        Debug.Log(transformTarget);
        StartCoroutine(ScaleComponent(transformTarget, 
            new Vector3(0,0,0), 0.25f));
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
                AudioManager.Instance.PlaySound("HeadshotSplat");
            }
        }

        PlayParticleAtLimb(name);
        StartCoroutine(DestroyObject(limb,5f));
    }

    private IEnumerator DestroyObject(GameObject toDestroy,float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(toDestroy);
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
        var startSize = target.localScale;
        float timer = 0;
        float duration = _explosionScaleTime;
        while (timer < duration)
        {
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
    void TakeDamage(float damage, Vector3 direction);
}

public interface IDestroyLimb
{
    void DestroyLimb(string name,Vector3 direction);

}

