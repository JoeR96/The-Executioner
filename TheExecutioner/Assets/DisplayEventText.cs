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
        if(activeEvent != null)
            SetCurrentKillCountText(activeEvent.currentTargetKillCount);
    }
    public void SetEvent(Event newEvent)
    {
        activeEvent = newEvent;
        ReturnEventKillCount();
    }
    
    public void SetCurrentKillCountText(int KillCount)
    {
        currentKillCountText.SetText(KillCount + " / " + targetKillCount );
    }

    private void ReturnEventKillCount()
    {
        targetKillCount = activeEvent.EventTargetKillCount;
    }
    
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
