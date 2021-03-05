using System.Collections;
using System.Collections.Generic;
using UnityEngine;


   public class PlatformManager : MonoBehaviour
   {
       public List<Node> OuterWalls = new List<Node>();





    public void LowerMultiplePlatformSection(List<List<Node>> PlatformArray)
    {
        foreach (var go in PlatformArray)
        {
            foreach (var node in go)
            {
                node.platform.GetComponent<PlatformState>().SetPlatformHeight((int)PlatformHeight.Flat);
            }
            
        }
    }



    

    public void LowerPlatformSection(Node[,] platforms)
    {
        foreach (var platform in platforms)
        {
            StartCoroutine(LerpTransformPosition(platform.platform.transform, new Vector3(platform.platform.transform.position.x,
                0f, platform.platform.transform.position.z), 1f));
        }
    }

    public void BalanceSectionPlatforms (GameObject[,] platforms,Vector3 originTransform)
    {
        Vector3 targetPosition;
        
        float v = originTransform.y;
        float x =Mathf.Round(v);
        originTransform.y = x;
        foreach (var platform in platforms)
        {
            targetPosition = platform.transform.position;
            targetPosition.y = x;
            platform.transform.position = targetPosition;
        }
    }

    private IEnumerator LerpTransformPosition(Transform transform, Vector3 target, float duration)
    {
        Transform startPosition = transform;
        float timer = 0f;
        float _duration = duration;
        while (timer < _duration)
        {
            timer += Time.deltaTime;
            float percentage = Mathf.Min(timer / _duration, 1);
            transform.position = Vector3.Lerp(startPosition.position, target, percentage);
            yield return null;
        }

        transform.position = target;
    }
}



