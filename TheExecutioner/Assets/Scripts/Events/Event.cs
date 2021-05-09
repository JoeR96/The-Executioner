using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;

public class Event : MonoBehaviour, IStartEvent, IReturnEvent, IDisplayEventText
{

    public EventTargetKillCount EventTargetKillCountManager { get; private set; }
    public int EventTargetKillCountMultiplier { get; set; }
    protected EventManager EventManager;
    protected Transform EventTargetDestination{ get; private set; }
    public GameObject EventText;
    [SerializeField] private ParticleSystem destroyParticle;
    private SpawnPointManager spawnPointManager;
    private GameObject eventDestroyTrigger;
    protected GameObject activeEventGameObject;
    protected EventZombieSpawner eventZombieSpawner;
    protected int waveSpawnTotal;
    private bool eventComplete;
    private bool rewardSpawned;
    private void Awake()
    {
        spawnPointManager = GameManager.instance.GetComponentInChildren<SpawnPointManager>();
        EventManager = GameManager.instance.EventManager;
    }
    private void Start()
    {
        EventTargetKillCountManager = new EventTargetKillCount(EventTargetKillCountMultiplier,GameManager.instance.roundManager.CurrentRound);
    }
    private void Update()
    {
        if (EventTargetKillCountManager.CurrentKillCount >= EventTargetKillCountManager.TargetKillCount && eventComplete == false)
        {
            eventComplete = true;
            StartCoroutine(CompleteEvent());
        }
        if (eventDestroyTrigger == null && rewardSpawned)
            Destroy(gameObject);
    }
    /// <summary>
    /// Play the destroy text animation to destroy the text
    /// Wait 1 second
    /// destroy the event game object
    /// Spawnn a reward
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator CompleteEvent()
    {
        DestroyText();
        yield return new WaitForSeconds(2.5f);
        DisableColliderAndMesh();
        SpawnReward();
        EventManager.RemoveEvent(this);
        yield return new WaitForSeconds(2.5f);
        destroyParticle.Play();
    }
    /// <summary>
    /// Get the mesh render and disable it
    /// Find all colliders within the event game object
    /// disable said colliders
    /// </summary>
    private void DisableColliderAndMesh()
    {
        GetComponentInChildren<MeshRenderer>().enabled = false;
        var colliderArray = GetComponentsInChildren<Collider>();
        foreach (var collider in colliderArray)
            collider.enabled = false;
    }

    /// <summary>
    /// Spawn a reward at the the target location
    /// </summary>
    private void SpawnReward()
    {
        eventDestroyTrigger = spawnPointManager.SpawnWeapon(3, transform);
        rewardSpawned = true;
    }

    public int progress { get; set; }
    public virtual void StartEvent( )
    {
        SetEventDestination();
        activeEventGameObject = gameObject;
        transform.position = EventTargetDestination.position;
        AddEventTransformsToMaster();
        AddEventToList();
        eventZombieSpawner = new EventZombieSpawner(waveSpawnTotal,transform);
        eventZombieSpawner.SpawnZombiesTargetingEvent();
    }

    private void AddEventToList()
    {
        EventManager.AddEvent(this);
    }

    public void SetEventDestination()
    {
        EventTargetDestination = EventManager.ReturnAvailableEventLocation();
    }

    public void AddEventTransformsToMaster()
    {
        EventManager.AddEventDestinationToList(EventTargetDestination);
        EventManager.AddEventTransformObjectToList(activeEventGameObject);
        Debug.Log("Event should add");
    }

    public virtual void OnTriggerEnter(Component other)
    {
        var enemy = other.GetComponentInParent<IIsInEventArea>();
        enemy?.IsInArea(true);
    }
    
    private void OnTriggerExit(Component other)
    {
        var enemy = other.GetComponentInParent<IIsInEventArea>();
        enemy?.IsInArea(false);
    }

    
    private IEnumerator ScaleComponent( )
    {
        var target = EventText.GetComponent<RectTransform>();
        var startSize = transform.localScale;
        float timer = 0;
        float duration = 1;
        while (timer < duration)
        {
            float percentage = Mathf.Min(timer / duration, 1);
            timer += Time.deltaTime;
            target.localScale = Vector3.Lerp(startSize, new Vector3(1,0,1),percentage);
            yield return null;
        }
        Destroy(EventText);
    }
    
    public Event ReturnActiveEvent()
    {
        return this;
    }

    public void SetText(GameObject o)
    {
        EventText = o;
    }

    public void DestroyText()
    {
        StartCoroutine(ScaleComponent());
    }
}
