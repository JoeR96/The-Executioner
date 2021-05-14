using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlatformBridgeHeight
{
    LowBridge,
    Middlebridge,
    HighBridge,
    Disabled
}

public class PlatformBridgeManager : MonoBehaviour
{
    [SerializeField] private List<float> bridgeHeights = new List<float>();
    [SerializeField] protected GameObject bridge;
    
    public bool PlatformBridgeActive { get; set; }

    [SerializeField] protected internal int CurrentBridgeHeight;
    public Vector3 bridgeStartPosition;
    public float duration = 3f;

    private void Start()
    {
        if (GameManager.instance.BuildMode)
            duration = GameManager.instance.BuildSpeed;
    }
    /// <summary>
    /// Sets the bridge at the height passed as a parameter
    /// </summary>
    /// <param name="height"></param>
    public void SetBridgeHeight(int height)
    {
        SetBridgeStartPosition();
        SetBridgePosition(bridgeHeights[height]);
        CurrentBridgeHeight = height;
    }
    /// <summary>
    /// Set the initial position of the bridge relative to the parent
    /// </summary>
    private void SetBridgeStartPosition()
    {
        bridgeStartPosition = new Vector3(2, 17.9f, -2);
        bridge.transform.localPosition = bridgeStartPosition;
    }
    /// <summary>
    /// Set the bridge position dependant on the input height
    /// lerp the bridge over duration passed in
    /// </summary>
    /// <param name="targetHeight"></param>
    /// <param name="duration"></param>
    private void SetBridgePosition(float targetHeight)
    {
        Vector3 targetPosition;
        targetPosition = new Vector3(2, bridgeStartPosition.y  + targetHeight,
           -2);
        StartCoroutine(LerpPosition(targetPosition));
    }
    /// <summary>
    /// Toggle the bridge active
    /// Return a boolean dependant on the bridge active status
    /// </summary>
    /// <returns></returns>
    public virtual bool ReturnBridgeValue()
    {
        return PlatformBridgeActive;
    }
    /// <summary>
    /// Toggle the bridge boolean
    /// </summary>
    public virtual void ToggleBridge()
    {
        PlatformBridgeActive = !PlatformBridgeActive;
    }
    /// <summary>
    /// Lerp the bridge position to the input position over the input duration
    /// </summary>
    /// <param name="targetPosition"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    private IEnumerator LerpPosition(Vector3 targetPosition)
    {
        Vector3 startPosition = bridge.transform.localPosition;
        float timer = 0f;
        float _duration = duration;

        while (timer < _duration)
        {
            timer += Time.deltaTime;
            float percentage = Mathf.Min(timer / _duration, 1);
            bridge.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, percentage);
            yield return null;
        }
    }
    /// <summary>
    /// Toggle the mesh and collider of the bridge depending on the active status
    /// </summary>
    /// <param name="active"></param>
    public virtual void ActivateBridge(bool active)
    {
        bridge.GetComponent<MeshRenderer>().enabled = active;
        bridge.GetComponent<BoxCollider>().enabled = active;
        PlatformBridgeActive = active;
    }
    /// <summary>
    /// Raise the platform by one index
    /// if it is at the last index return
    /// </summary>
    public void RaiseOneLevel()
    {
        CurrentBridgeHeight++;
        
        if (CurrentBridgeHeight == bridgeHeights.Count)
            CurrentBridgeHeight++;
        
        SetBridgeHeight(CurrentBridgeHeight);
        
    }
    /// <summary>
    /// if it is at the first index return
    /// Lower the platform index by 1
    /// </summary>
    public void DownOneLevel()
    {
        if (CurrentBridgeHeight == 0)
            CurrentBridgeHeight++;
        SetBridgeHeight(CurrentBridgeHeight - 1);
    }
}