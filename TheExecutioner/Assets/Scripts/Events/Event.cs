﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;

public class Event : MonoBehaviour, IStartEvent, IReturnEvent, IDisplayEventText
{
    public EventTargetKillCount EventTargetKillCountManager { get; private set; }
    public int EventTargetKillCountMultiplier { get; set; }
    protected EventManager EventManager;
    protected Transform EventTargetDestination{ get; set; }
    public GameObject EventText;
    [SerializeField] private ParticleSystem destroyParticle;
    [SerializeField] private Transform[] eventKillTransformPositions;
    [SerializeField] protected ParticleSystem LightningStrike;
    private SpawnPointManager spawnPointManager;
    protected GameObject eventDestroyTrigger;
    protected GameObject activeEventGameObject;
    protected EventZombieSpawner eventZombieSpawner;
    protected int waveSpawnTotal;
    protected bool eventComplete;
    protected bool rewardSpawned;

    public virtual void Awake()
    {
        spawnPointManager = GameManager.instance.GetComponentInChildren<SpawnPointManager>();
        EventManager = GameManager.instance.EventManager;
       
    }

    protected virtual void Start()
    {
        EventTargetKillCountManager = new EventTargetKillCount(EventTargetKillCountMultiplier,GameManager.instance.roundManager.CurrentRound);
    }

    /// <summary>
    /// Play the destroy text animation to destroy the text
    /// Wait 1 second
    /// destroy the event game object
    /// Spawnn a reward
    /// </summary>
    /// <returns></returns>
    protected virtual void EventComplete()
    {
        SpawnReward();
        LightningStrike.Play();
        AudioManager.Instance.PlaySound("Lightning");
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
    protected void SpawnReward()
    {
        eventDestroyTrigger = spawnPointManager.SpawnWeapon(3, EventTargetDestination,true);
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
        RectTransform target;
        target = EventText.GetComponent<RectTransform>();
        if (target == null)
            yield break;
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
    /// <summary>
    /// Create and set the start position of the targetTransform
    /// Whilst the timer is less than duration lerp the transform to event position one
    /// Percentage ensures the lerp is over a consistent value
    /// The same process is repeated in the second while loop however startPosition is set to the current position
    /// The transform is lerped to the second position in the target positions array
    /// </summary>
    /// <param name="targetTransform"></param>
    /// <returns></returns>
    public IEnumerator KillSacrificeEventEnemy(Transform targetTransform)
    {
        var startPosition = targetTransform.position;

        float percentage;
        float timer = 0;
        float duration = 0.75f;
        while (timer < duration)
        {
            percentage = Mathf.Min(timer / duration, 1);
            timer += Time.deltaTime;
            targetTransform.position = Vector3.Lerp(startPosition, eventKillTransformPositions[0].position, percentage);
        }
        startPosition = targetTransform.position;
        timer = 0;
        while (timer < duration)
        {
            Debug.Log("Should lerp to position two");
            percentage = Mathf.Min(timer / duration, 1);
            timer += Time.deltaTime;
            targetTransform.position = Vector3.Lerp(startPosition, eventKillTransformPositions[1].position, percentage);
        }
        yield return null;
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
