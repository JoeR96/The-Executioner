using System.Collections;
using System.Collections.Generic;
using UnityEngine;


   public class PlatformManager : MonoBehaviour
{
    
    public void RaisePlatform(GameObject wall)
    {
        StartCoroutine(LerpTransformPosition(wall.transform, new Vector3(wall.transform.position.x, Mathf.Round(wall.transform.position.y + 5f), wall.transform.position.z), 1f));
            
    }
    public void RaisePlatformToLevelTwo(GameObject wall)
    {
        StartCoroutine(LerpTransformPosition(wall.transform, new Vector3(wall.transform.position.x, Mathf.Round(wall.transform.position.y + 10f), wall.transform.position.z), 1f));
            
    }
    public void LowerPlatform(GameObject wall)
    {
        StartCoroutine(LerpTransformPosition(wall.transform, new Vector3(wall.transform.position.x,0f, wall.transform.position.z), 1f));
            
    }
    public void LowerMultiplePlatformSection(List<List<Node>> PlatformArray)
    {
        foreach (var go in PlatformArray)
        {
            foreach (var node in go)
            {
                LowerPlatform(node.platform);
            }
            
        }
    }
    private void RaisePlatformSection(GameObject[,] wall)
    {
        foreach (var go in wall)
        {
            if (go)
            {
                StartCoroutine(LerpTransformPosition(go.transform, new Vector3(go.transform.position.x,
                    go.transform.position.y + 5f
                    , go.transform.position.z), 1f));
            }
        }
    }

    public void ResetPlatformSections(List<List<GameObject[,]>> LevelPlatforms)
    {
        foreach (var platformGroups in LevelPlatforms)
        {
            foreach (var nodes in platformGroups)
            {
                LowerPlatformSection(nodes);
            }
        }
    }

    public void RaisePath(List<Node> path)
    {
        foreach (var node in path)
        {
            RaisePlatform(node.platform);
        }
    }
    
    public void LowerPath(List<Node> path)
    {
        foreach (var node in path)
        {
            LowerPlatform(node.platform);
        }
    }
    
    public void LowerPlatformSection(GameObject[,] platforms)
    {
        foreach (var platform in platforms)
        {
            StartCoroutine(LerpTransformPosition(platform.transform, new Vector3(platform.transform.position.x,
                0f, platform.transform.position.z), 1f));
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



