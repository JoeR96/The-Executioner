using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlatformHeight
{
    Flat,
    LevelOne,
    LevelTwo,
    LevelThree,
    LevelFour,
    LevelFive,
    LevelSix,
    UndergroundOne,
    UndergroundTwo,
    RaisedOuterWall,
    LoweredOuterWall
}
public class PlatformHeightManager : MonoBehaviour
{
    private PlatformManager platformManager;
    public int CurrentHeight;
    [SerializeField] private List<int> platformHeights = new List<int>();

    public PlatformHeightManager(PlatformManager platformStateManager)
    {
        platformManager = platformStateManager;
    }

    public void SetPlatformHeight(int height )
    {
        RaisePlatform(gameObject,platformHeights[height],2.5f);
        CurrentHeight = height;
    }

    private void RaisePlatform(GameObject go,float targetHeight,float duration)
    {
        Vector3 targetPosition;
        targetPosition = new Vector3(go.transform.position.x, targetHeight, go.transform.position.z);
        StartCoroutine(MovePlatform(go,targetPosition, 2.5f));
    }

    private IEnumerator MovePlatform( GameObject go,Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = go.transform.position;
        float timer = 0f;
        float _duration = duration;
         
        while (timer < _duration)
        {
            timer += Time.deltaTime;
            float percentage = Mathf.Min(timer / _duration, 1);
            go.transform.position = Vector3.Lerp(startPosition,  targetPosition, percentage);
            yield return null;
        }
        
    }
}