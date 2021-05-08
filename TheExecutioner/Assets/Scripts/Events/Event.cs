using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;

public class Event : MonoBehaviour, IStartEvent, IReturnEvent, IDisplayEventText
{
    protected EventManager eventManager; protected Transform eventTargetDestination{ get; private set; }
    public EventTargetKillCount EventTargetKillCountManager { get; private set; }
    public int EventTargetKillCountMultiplier { get; set; }
    public GameObject EventText;
    protected GameObject activeEventGameObject;
    protected EventZombieSpawner eventZombieSpawner;
    protected int waveSpawnTotal;
    private bool eventComplete;
    private SpawnPointManager SpawnPointManager;

    private void Awake()
    {
        //SpawnPointManager = GameManager.instance.GetComponent<SpawnPointManager>();
        eventManager = GameManager.instance.EventManager;
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
    }
    /// <summary>
    /// Play the destroy text animation to destroy the text
    /// Wait 1 second
    /// destroy the event game object
    /// Spawnn a reward
    /// </summary>
    /// <returns></returns>
    private IEnumerator CompleteEvent()
    {
        DestroyText();
        yield return new WaitForSeconds(1f);
        Destroy(activeEventGameObject);
        SpawnReward();
    }
    /// <summary>
    /// Spawn a reward at the the target location
    /// </summary>
    private void SpawnReward()
    {
        SpawnPointManager.SpawnWeapon(eventTargetDestination);
    }

    public int progress { get; set; }
    public virtual void StartEvent( )
    {
        SetEventDestination();
        activeEventGameObject = gameObject;
        transform.position = eventTargetDestination.position;
        AddEventTransformsToMaster();
        AddEventToList();
        eventZombieSpawner = new EventZombieSpawner(waveSpawnTotal,transform);
        eventZombieSpawner.SpawnZombiesTargetingEvent();
    }

    private void AddEventToList()
    {
        eventManager.AddEvent(this);
    }

    public void SetEventDestination()
    {
        eventTargetDestination = eventManager.ReturnAvailableEventLocation();
    }

    public void AddEventTransformsToMaster()
    {
        eventManager.AddEventDestinationToList(eventTargetDestination);
        eventManager.AddEventTransformObjectToList(activeEventGameObject);
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
