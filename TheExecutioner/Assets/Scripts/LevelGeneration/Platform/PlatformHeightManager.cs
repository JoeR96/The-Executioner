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
        RaisePlatform(gameObject,platformHeights[height],1f);
        CurrentHeight = height;
    }

    private void RaisePlatform(GameObject go,float targetHeight,float duration)
    {
        Vector3 targetPosition;
        targetPosition = new Vector3(go.transform.position.x, targetHeight, go.transform.position.z);
        StartCoroutine(MovePlatform(go,targetPosition, 3f));
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

    public void RaisePlatformTowerHeight()
    {
        for (int i = 0; i < platformHeights.Count; i++)
        {
            if (i != 14) 
            {
                platformHeights[i] += 2;
            }
        }

        var spawnPos = GameManager.instance.playerSpawnPoint.transform.position;
        var targetVector = new Vector3(spawnPos.x, spawnPos.y += 2, spawnPos.z);
        GameManager.instance.playerSpawnPoint.transform.position = targetVector;
    }
}