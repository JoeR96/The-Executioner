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
        StartEvent();
    }
    public override void StartEvent()
    {
        transform.position = eventManager.ReturnAvailableEventLocation().position;
        activeEventGameObject = gameObject;
 
        AddEventTransformsToMaster();
        SetEventDestination();
        SetHeart();
    }

    private void SetHeart()
    {
        var activeHeart = activeEventGameObject.GetComponent<Heart>();
        activeHeart.SetTargetPosition(eventTargetDestination);
        
    }
    
    public void EventComplete(Heart heart)
    {
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
        StartCoroutine(LerpHeartTransform(limb, this.transform.position));
        LimbsSacrificed++;
    }

    }


public interface ICollectLimb
{
    void CollectLimb(GameObject limb);
}
