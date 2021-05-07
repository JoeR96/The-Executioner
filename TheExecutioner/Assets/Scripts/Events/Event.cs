using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;

public class Event : MonoBehaviour, IStartEvent, IReturnEvent, IDisplayEventText
{
    [field: SerializeField] protected GameObject eventGameObject { get; set; }
    [SerializeField] protected EventManager eventManager;
    [field: SerializeField] protected Transform eventTargetDestination{ get; private set; }
    protected GameObject activeEventGameObject;
    protected EventZombieSpawner eventZombieSpawner;
    public int EventTargetKillCountMultiplier { get; set; }
    public EventTargetKillCount EventTargetKillCountManager;
    protected int waveSpawnTotal;
    public int currentTargetKillCount;
    public GameObject EventText;
    private bool eventComplete;

    private void Awake()
    {
        eventManager = GameManager.instance.EventManager;
    }
    private void Start()
    {
        EventTargetKillCountManager = new EventTargetKillCount(EventTargetKillCountMultiplier,GameManager.instance.roundManager.CurrentRound);
        Debug.Log("Current Round =" + GameManager.instance.roundManager.CurrentRound);
    }

    private void Update()
    {
        
        EventTargetKillCountManager.CurrentKillCount = currentTargetKillCount;
        if (currentTargetKillCount == EventTargetKillCountManager.TargetKillCount && eventComplete == false)
        {
            eventComplete = true;
            CompleteEvent();
        }
    }

    private void CompleteEvent()
    {
        Destroy(activeEventGameObject);
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
}
