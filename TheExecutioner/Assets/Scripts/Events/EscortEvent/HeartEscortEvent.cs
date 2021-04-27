using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HeartEscortEvent : Event, ITakeDamage, ICollectLimb
{
    
    [SerializeField] private GameObject targetAltar;

    public float percent;
    private int LimbsSacrificed { get; set; }
    private Transform targetPos;
    private float startDistance;
    
    private void OnEnable()
    {
        EventTargetKillCount = 25;
        waveSpawnTotal = 8;
        StartHeartEvent();
    }
    public void StartHeartEvent()
    {
        SetEventDestination();
        eventZombieSpawner = new EventZombieSpawner(waveSpawnTotal,transform);
        transform.position = eventManager.ReturnAvailableEventLocation().position;
        
        eventZombieSpawner.SpawnZombiesTargetingEvent();
        SetHeart();
        
        AddEventTransformsToMaster();
        
        
    }

    private void SetHeart()
    {
        var activeHeart = GetComponent<Heart>();
        Debug.Log(eventTargetDestination);
        activeHeart.SetTargetPosition(eventTargetDestination);
        
    }
    
    public void EventComplete(Heart heart)
    {
        if(heart.Destination != null)
            return;
        var altar = Instantiate(targetAltar, heart.Destination.position,quaternion.identity);
        targetPos = ReturnHeartTargetPosition(altar);

        heart.transform.rotation = targetPos.rotation;
        
        heart.DisableNavmeshAgent();
        StartCoroutine(LerpHeartTransform(heart.gameObject, targetPos.position));
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
            heart.transform.rotation = Quaternion.Slerp(heart.transform.rotation, targetPos.rotation, percentage);
            yield return null;
        }
    }


    public void TakeDamage(float damage, Vector3 direction)
    {
        throw new System.NotImplementedException();
    }

    public void CollectLimb(GameObject limb)
    {
        StartCoroutine(LerpHeartTransform(limb, transform.position));
        LimbsSacrificed++;
    }

    public override void OnTriggerEnter(Component other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag("Limb"))
        {
            ReturnLimb(other);
        }
            
    }

    private void ReturnLimb(Component other)
    {
        var limb = other.GetComponent<ICollectLimb>();
        limb.CollectLimb(other.gameObject);
    }
}


public interface ICollectLimb
{
    void CollectLimb(GameObject limb);
}
