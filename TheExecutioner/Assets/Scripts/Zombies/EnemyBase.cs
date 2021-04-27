﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class EnemyBase : MonoBehaviour, ITakeDamage, IDestroyLimb, IIsInEventArea
{
    [SerializeField] private ParticleSystem spawnParticle;
    [SerializeField] protected GameObject _rootComponent;
    [SerializeField] protected GameObject _zombieSkinContainer;
    [SerializeField] protected Vector3 _explosionScaleSize;
    [SerializeField] protected float _explosionScaleTime;
    [SerializeField] protected float _maxHealth;

    public float EnemyHealth;
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
        foreach (Transform transform in _rootComponent.transform)
        {
            transform.localScale = Vector3.one;
        }
    }

    private Timer timer;
    private void OnEnable()
    {
        ActivateZombie();
        healthSystem = new HealthSystem(_maxHealth,_maxHealth);
        
    }
    private void Start()
    {
        _aiAgent.StateMachine.ChangeState(StateId.ChasePlayer);
        spawnParticle.Play();
        SetRandomAnimTime();
        SetRandomSkin();
        timer = new Timer(5f);
    }

    
    bool time = false;
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
        
        EnemyHealth = healthSystem.CurrentHealth;
        if (IsAgentOnNavMesh() && isOnMesh == false && isAlive)
        {
            isOnMesh = true;
              ActivateZombie();
          }
        
        if(_animator.enabled == false && _aiAgent.navMeshAgent.enabled == false && timer.TimerIsOver())
        {
 
            Invoke("KillZombie",2.5f);
        }

    }

    private void KillZombie()
    {
        GameManager.instance.EventManager.zombieSpawner.RemoveZombieFromList(gameObject);
        Destroy(gameObject);
        if (InEvent)
        {
            var x = Physics.OverlapSphere(transform.position, 3f);
            foreach (var events in x)
            {
                if (events.GetComponent<Event>() != null)
                {
                    events.GetComponent<Event>().EventTargetKillCountManager.IncreaseKillCount();
                }
            }
        }
            
        //ObjectPooler.instance.ReturnObject(gameObject,ZombieType);
    }
    public void ActivateZombie( )
    {
        _aiAgent.Animator.enabled = true;
        _aiAgent.navMeshAgent.enabled = true;
        _aiAgent.Ragdoll.DeactivateRagdoll();
    }
    public void DeactivateZombie( )
    {
        _aiAgent.navMeshAgent.enabled = false;
        GetComponent<Ragdoll>().ActivateRagDoll();
    }
    public void TakeDamage(float damage, Vector3 direction)
    {
        healthSystem.TakeDamage(damage);
        if (healthSystem.CurrentHealth < 0)
        {
            if (InEvent)
            {
                _aiAgent.StateMachine.ChangeState(StateId.DeathState);
            }
            GameManager.instance.ZombieManager.ZombieSpawner.RemoveZombieFromList(gameObject);
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
        StartCoroutine(ScaleComponent(root, _explosionScaleSize, 0.5f));
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
        var transformTarget = LimbManager.DestructibleLimbs[limbName];
       

        
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

            if (random == 1)
            {
                AudioManager.Instance.PlaySound("HeadshotSplat");
            }
        }

        LimbManager.PlayParticleAtLimb(limbName);
        StartCoroutine(LimbManager.RemoveLimb(limb,5f));
    }

    public IEnumerator Die()
    {
        yield return new WaitForSeconds(2f);
        //ObjectPooler.instance.ReturnObject(gameObject,ZombieType);
    }
}

public interface ITakeDamage
{ 
    void TakeDamage(float damage, Vector3 direction);
   
}


