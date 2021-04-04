using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyBase : MonoBehaviour, ITakeDamage, IDestroyLimb, IIsInEventArea
{
    [SerializeField] private ParticleSystem spawnParticle;
    [SerializeField] protected GameObject _rootComponent;
    [SerializeField] protected GameObject _zombieSkinContainer;
    [SerializeField] protected Vector3 _explosionScaleSize;
    [SerializeField] protected float _explosionScaleTime;
    [SerializeField] protected float _maxHealth;

    [field: SerializeField]
     public float Damage { get; set; }

     public LimbManager LimbManager { get; private set; }
     public PoolObjectType ZombieType { get; private set; }

     public GameObject ActiveSkin;
     protected Animator _animator;
    protected HealthSystem healthSystem;
    protected AiAgent _aiAgent;
    
    private void Awake()
    {
        _aiAgent = GetComponent<AiAgent>();
        healthSystem = new HealthSystem(_maxHealth,_maxHealth);
        _animator = GetComponent<Animator>();
        LimbManager = GetComponent<LimbManager>();
    }

    private void Start()
    {
        spawnParticle.Play();
        SetRandomAnimTime();
        SetRandomSkin();
    }

    private void SetRandomAnimTime()
    {
        AnimatorStateInfo state = _animator.GetCurrentAnimatorStateInfo(0);
        _animator.Play(state.fullPathHash, -1, Random.Range(0f, 1f));
    }
    private void SetRandomSkin()
    {
        var random = Random.Range(0, _zombieSkinContainer.transform.childCount);
        var skin = _zombieSkinContainer.transform.GetChild(random).gameObject;
        skin.SetActive(true);
        ActiveSkin = skin;
    }
    bool isOnMesh;
    private bool isAlive = true;
    protected void Update()
    {
        if (IsAgentOnNavMesh() && isOnMesh == false && isAlive)
        {
            isOnMesh = true;
              ActivateZombie();
          }
    }
    
    public void ActivateZombie( )
    {
        _aiAgent.navMeshAgent.enabled = true;
        GetComponent<Ragdoll>().DeactivateRagdoll();
        
    }
    
    public void TakeDamage(float damage, Vector3 direction)
    {
        healthSystem.TakeDamage(damage);
        if (healthSystem.CurrentHealth < 0)
        {
            _aiAgent.StateMachine.ChangeState(StateId.DeathState);
           
        }
    }
    


    public bool InEvent;
    
   

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
    }


    private  EnemyPartices _enemyPartices = new EnemyPartices();


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
    
    public void IsInArea(bool state)
    {
        InEvent = state;
    }


    public void DestroyLimb(string limbName,Vector3 direction)
    {
        Debug.Log(limbName);
        Debug.Log(LimbManager.DestructibleLimbs.Count);
        var transformTarget = LimbManager.DestructibleLimbs[limbName];
       
        Debug.Log(transformTarget.name);
        
        StartCoroutine(ScaleComponent(transformTarget, 
            new Vector3(0,0,0), 0.25f));
        var t = LimbManager.DestructibleLimbs[limbName];
        var limb = GameManager.instance.LimbSpawner.ReturnLimb(limbName);
        limb.transform.position = LimbManager.DestructibleLimbs[limbName].transform.position;
        
        limb.GetComponent<Rigidbody>().AddForce(-direction);
        if (limbName == "Head")
        {
            _aiAgent.Ragdoll.ActivateRagDoll();
            var random = Random.Range(0, 3);
            Debug.Log(random);
            if (random == 1)
            {
                _animator.SetBool("DiedByHeadshot", true);
                AudioManager.Instance.PlaySound("HeadshotSplat");
            }
        }

        LimbManager.PlayParticleAtLimb(limbName);
        StartCoroutine(LimbManager.RemoveLimb(limb,5f));
    }


}
public interface ITakeDamage
{ 
    void TakeDamage(float damage, Vector3 direction);
}


