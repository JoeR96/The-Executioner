
using System.Collections;
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

    
    public LimbManager LimbManager { get; private set; }
    public PoolObjectType ZombieType { get;  }
    public GameObject ActiveSkin;
    internal Animator animator;
    private HealthSystem healthSystem;
    [SerializeField] protected AiAgent _aiAgent;
    [field: SerializeField]
    public float Damage { get; set; }
    public bool IsDead { get; set; }
    public bool InEvent;
    private bool isOnMesh;
    private bool isAlive = true;
    
    private void Awake()
    {
        healthSystem = new HealthSystem(_maxHealth,_maxHealth);
        animator = GetComponent<Animator>();
        LimbManager = GetComponent<LimbManager>();
       
    }
    private Timer timer;

    private void Start()
    {
        ActivateZombie();
    }

    /// <summary>
    /// OnEnable is used instead of start due to object pooling
    /// Activate a zombie
    /// </summary>
    private void OnEnable()
    {
        IsDead = false;
        healthSystem = new HealthSystem(125f,125f);
    }
    protected void Update()
    {
        if (IsAgentOnNavMesh()  == false && isAlive)
        {
            _aiAgent.navMeshAgent.set
        }
        if(animator.enabled == false && _aiAgent.navMeshAgent.enabled == false && timer.TimerIsOver() || IsDead)
        {
            healthSystem.TakeDamage(50000);
        }
    }
    private void SetRandomAnimTime()
    {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        animator.Play(state.fullPathHash, -1, Random.Range(0f, 1f));
    }
    private void SetRandomSkin()
    {
        var random = Random.Range(0, _zombieSkinContainer.transform.childCount);
        var skin = _zombieSkinContainer.transform.GetChild(random).gameObject;
        skin.SetActive(true);
        ActiveSkin = skin;
    }
    private void KillZombie()
    {
        GameManager.instance.roundManager.zombieSpawner.RemoveZombieFromList(gameObject);
        ResetLimbs();
        Destroy(gameObject);
    }
    /// <summary>
    /// Activate a zombie from its ragdoll steal
    /// Enable the animator
    /// Enable the navmeshAgent
    /// </summary>
    public void ActivateZombie( )
    {
        healthSystem = new HealthSystem(_maxHealth,_maxHealth);
        timer = new Timer(5f);
        IsDead = false;
       
        SetRandomAnimTime();
        spawnParticle.Play();
        SetRandomSkin();
        _aiAgent.StateMachine.ChangeState(StateId.ChasePlayer);
        _aiAgent.Ragdoll.DeactivateRagdoll();
    }
    /// <summary>
    /// Deactivate the Zombie
    /// disable the navmesh agent
    /// Activate the ragdoll
    /// </summary>
    public void DeactivateZombie( )
    {
        _aiAgent.navMeshAgent.enabled = false;
        GetComponent<Ragdoll>().ActivateRagDoll();
    }
    public void TakeDamage(float damage, Vector3 direction)
    {
        //&& timer.TimerIsOver()
        healthSystem.TakeDamage(damage);
        if(healthSystem.BelowPercent(1) && IsDead != true)
        {
            IsDead = true;
            _aiAgent.StateMachine.ChangeState(StateId.DeathState);
            Invoke("KillZombie",2.5f);
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
    }
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
            AudioManager.Instance.PlaySound("HeadshotSplat");
            
            var random = Random.Range(0, 2);
            if (random == 1)
            {
                healthSystem.TakeDamage(50000);
                _aiAgent.Ragdoll.ActivateRagDoll();
            }
       
        }
        else
        {
            AudioManager.Instance.PlaySound("LimbSplat");
        }

        LimbManager.PlayParticleAtLimb(limbName);
        StartCoroutine(LimbManager.RemoveLimb(limb,5f));
    }

    public void ResetLimbs()
    {
        foreach (var child in LimbManager.Limbs)
        {
            child.transform.localScale = Vector3.one;
        }
    }
}
public interface ITakeDamage
{ 
    void TakeDamage(float damage, Vector3 direction);
}


