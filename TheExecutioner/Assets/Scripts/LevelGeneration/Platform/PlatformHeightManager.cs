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
    public int CurrentHeight { get; set; }
    public float Duration = 3f;
    [SerializeField] private List<int> platformHeights = new List<int>();
    /// <summary>
    /// Raise the platform to the height within the height list using the index passed in
    /// </summary>
    /// <param name="index"></param>
    public void SetPlatformHeight(int index )
    {
        CurrentHeight = index;
        Vector3 targetPosition = new Vector3(gameObject.transform.position.x, platformHeights[index], gameObject.transform.position.z);
        StartCoroutine(MovePlatform(targetPosition));
    }
    /// <summary>
    /// Coroutine to lerp the platform height to a specified position
    /// Set a start position
    /// increase time using delta time
    /// create a percentage to lerp at a consistent speed
    /// lerp the platforms transform from the start value to the end value
    /// startPosition and targetPosition are created so the lerp happens at a consistent speed
    /// </summary>
    /// <param name="go"></param>
    /// <param name="targetPosition"></param>
    /// <returns></returns>
    private IEnumerator MovePlatform(Vector3 targetPosition)
    {
        Vector3 startPosition = transform.position;
        float timer = 0f;

        while (timer < Duration)
        {
            timer += Time.deltaTime;
            float percentage = Mathf.Min(timer / Duration, 1);
            transform.position = Vector3.Lerp(startPosition,  targetPosition, percentage);
            yield return null;
        }
    }
    /// <summary>
    /// Increase the height of each platform element in the height list by 2
    /// </summary>
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