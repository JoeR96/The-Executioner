using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HeartEscortEvent : Event, ITakeDamage
{
    
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private GameObject targetAltar;

    public int LimbsSacrificed { get; set; }
    private Transform targetPos;
    
    public override void StartEvent()
    {
        var target = eventManager.ReturnAvailableEventLocation().position;
        
        activeEventGameObject = Instantiate(eventGameObject, target, quaternion.identity);
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
        float duration = 1f;

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
}
