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
    [SerializeField] public GameObject bridge;
    public bool PlatformBridgeActive;
    public int CurrentBridgeHeight;

    private PlatformManager platformManager;

    public PlatformBridgeManager(PlatformManager platform)
    {
        platformManager = platform;
    }

    public void SetBridgeHeight(int height)
    {
        SetBridgePosition(bridgeHeights[height], 1f);
        CurrentBridgeHeight = height;
    }

    private void SetBridgePosition(float targetHeight, float duration)
    {
        Vector3 targetPosition;
        targetPosition = new Vector3(bridge.transform.position.x, bridge.transform.position.y + 18f + targetHeight,
            bridge.transform.position.z);
        StartCoroutine(LerpPosition(targetPosition, duration));
    }

    public bool ReturnBridgeValue()
    {
        PlatformBridgeActive = !PlatformBridgeActive;
        return PlatformBridgeActive;
    }


    private IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = transform.position;
        float timer = 0f;
        float _duration = duration;

        while (timer < _duration)
        {
            timer += Time.deltaTime;
            float percentage = Mathf.Min(timer / _duration, 1);
            transform.position = Vector3.Lerp(startPosition, targetPosition, percentage);
            yield return null;
        }
    }
    private PlatformStateManager _platformStateManager;



    public void ActivateBridge(bool active)
    {
        bridge.GetComponent<MeshRenderer>().enabled = active;
        bridge.GetComponent<BoxCollider>().enabled = active;
    }
}