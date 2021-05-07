using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayEventText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentKillCountText;
    [SerializeField] private TextMeshProUGUI targetKillCountText;
    private int targetKillCount;
    private Event activeEvent;

    private void Start()
    {
        StartCoroutine(ScaleComponent());
    }
    private void Update()
    {

        SetCurrentKillCountText(activeEvent.currentTargetKillCount);
    }
    public void SetEvent(Event newEvent)
    {
        activeEvent = newEvent;
        ReturnEventKillCount();
    }
    /// <summary>
    /// Set the current killcount text
    /// </summary>
    /// <param name="KillCount"></param>
    public void SetCurrentKillCountText(int KillCount)
    {
        currentKillCountText.SetText(KillCount + " / " + targetKillCount );
    }
    /// <summary>
    /// Return the target event kill count from the active event
    /// </summary>
    private void ReturnEventKillCount()
    {
        targetKillCount = activeEvent.EventTargetKillCountMultiplier;
    }
    /// <summary>
    /// Animate the event UI to lerp in size from 0 - 1
    /// </summary>
    /// <returns></returns>
    private IEnumerator ScaleComponent( )
    {
        var target = GetComponent<RectTransform>();
        var startSize = transform.localScale;
        float timer = 0;
        float duration = 1;
        while (timer < duration)
        {
            float percentage = Mathf.Min(timer / duration, 1);
            timer += Time.deltaTime;
            target.localScale = Vector3.Lerp(startSize, Vector3.one,percentage);
            yield return null;
        }
    }
}
