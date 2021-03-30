using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HeartEscortEvent : Event
{
    
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private GameObject targetAltar;

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
        var targetPos = ReturnHeartTargetPosition(altar);
        
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
        Vector3 startPosition = heart.transform.position;
        float timer = 0f;
        float duration = 1f;

        while (timer < duration)
        {
            float percentage = Mathf.Min(timer / duration, 1);
            timer += Time.deltaTime;
            heart.transform.position = Vector3.Lerp(startPosition,targetPosition,percentage);
            yield return null;
        }
    }
    

}
