using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HeartEscortEvent : Event, ITakeDamage, ICollectLimb
{
    
    [SerializeField] private GameObject targetAltar;
    public float percent;
    private int LimbsSacrificed { get; set; }
    private float startDistance;
    private GameObject altar;
    
    private void OnEnable()
    {
        EventTargetKillCountMultiplier = 5;
        waveSpawnTotal = 8;
        StartHeartEvent();
        
    }
    /// <summary>
    /// Start the heart event
    /// 
    /// </summary>
    public void StartHeartEvent()
    {
        SetEventDestination();
        altar = Instantiate(targetAltar,EventTargetDestination.position,quaternion.identity);
        eventZombieSpawner = new EventZombieSpawner(waveSpawnTotal,transform);
        transform.position = EventManager.ReturnAvailableEventLocation().position;
        activeEventGameObject = gameObject;
        eventZombieSpawner.SpawnZombiesTargetingEvent();
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
        StartCoroutine(LerpHeartTransform(heart.gameObject, EventTargetDestination.position));
        SpawnReward();
    }
    public Transform ReturnHeartTargetPosition(GameObject altar)
    {
        var target = altar.GetComponent<AltarEventCompleteTrigger>().HeartTargetPosition;
        return target;
    }
    private IEnumerator LerpHeartTransform(GameObject heart,Vector3 targetPosition)
    {
        Transform startRotation = heart.transform;
        Vector3 startPosition = heart.transform.position;
        float timer = 0f;
        float duration = 0.5f;

        while (timer < duration)
        {
            float percentage = Mathf.Min(timer / duration, 1);
            timer += Time.deltaTime;
            heart.transform.position = Vector3.Lerp(startPosition,targetPosition,percentage);
             heart.transform.rotation = Quaternion.Slerp(heart.transform.rotation, EventTargetDestination.rotation, percentage);
            yield return null;
        }
    }
    public void TakeDamage(float damage, Vector3 direction)
    {
        
    }
    public void CollectLimb(GameObject limb)
    {
        StartCoroutine(LerpHeartTransform(limb, transform.position));
        LimbsSacrificed++;
    }
    public override void OnTriggerEnter(Component other)
    {
        //base.OnTriggerEnter(other);
        if (other.CompareTag("Limb"))
        {
            ReturnLimb(other.gameObject);
        }
            
    }
    private void ReturnLimb(GameObject other)
    {
       // CollectLimb(other.gameObject);
    }
}


public interface ICollectLimb
{
    void CollectLimb(GameObject limb);
}
