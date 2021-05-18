using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HeartEscortEvent : Event, ITakeDamage
{
    
    [SerializeField] private GameObject targetAltar;
    public float percent;
    private int LimbsSacrificed { get; set; }
    private float startDistance;
    private GameObject altar;
    private HealthSystem healthSystem;
    
    public override void Awake()
    {
        waveSpawnTotal = 2 * GameManager.instance.roundManager.CurrentRound + 1;
        eventZombieSpawner = new EventZombieSpawner(waveSpawnTotal,transform);
        base.Awake();
    }
    private void OnEnable()
    {
        healthSystem = new HealthSystem(250, 250);
        waveSpawnTotal = 3 * GameManager.instance.roundManager.CurrentRound;
        EventTargetKillCountMultiplier = 5;
        StartHeartEvent();
        
    }
    /// <summary>
    /// Start the heart event
    /// 
    /// </summary>
    public void StartHeartEvent()
    {
        SetEventDestination();
        StartCoroutine(eventZombieSpawner.SpawnZombiesTargetingEvent());
        altar = Instantiate(targetAltar,EventTargetDestination.position,quaternion.identity);
        EventManager.AddEventTransformObjectToList(altar);
        transform.position = EventManager.ReturnAvailableEventLocation().position;
        activeEventGameObject = gameObject;
        SetHeart();
        AddEventTransformsToMaster();
    }
    private void SetHeart()
    {
        var activeHeart = GetComponent<Heart>();
        Debug.Log(EventTargetDestination);
        activeHeart.SetTargetPosition(EventTargetDestination);
    }
    public void EventComplete(Heart heart)
    {
        EventTargetDestination = ReturnHeartTargetPosition(altar);
        heart.EventComplete = true;
        heart.transform.rotation = EventTargetDestination.rotation;
        
        heart.DisableNavmeshAgent();
        StartCoroutine(LerpHeartTransform(heart.gameObject, altar.transform.position));
    }
    public Transform ReturnHeartTargetPosition(GameObject altar)
    {
        var target = altar.GetComponent<AltarEventCompleteTrigger>().HeartTargetPosition;
        return target;
    }
    private IEnumerator LerpHeartTransform(GameObject heart,Vector3 targetPosition)
    {
        eventZombieSpawner.ClearEventZombiesTarget();
        Vector3 startPosition = heart.transform.position;
        float timer = 0f;
        float duration = 0.5f;

        while (timer < duration)
        {
            float percentage = Mathf.Min(timer / duration, 1);
            timer += Time.deltaTime;
            heart.transform.position = Vector3.Lerp(startPosition,targetPosition,percentage);
            heart.transform.rotation = Quaternion.Slerp(heart.transform.rotation, EventTargetDestination.rotation, percentage);
            heart.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, percentage);
            yield return null;
        }
        base.EventComplete();
    }
    public void TakeDamage(float damage, Vector3 direction)
    {
        healthSystem.TakeDamage(damage);
        if(healthSystem.BelowPercent(1))
            gameObject.SetActive(false);
    }
  
}


