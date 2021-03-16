using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRampManager : MonoBehaviour
{
    public bool PlatformRampActive;
    public int CurrentRotation;
    [SerializeField] private List<float> rampRotations = new List<float>();
    [SerializeField] public GameObject ramp;
    [SerializeField] public GameObject rampMat;
    private Vector3 rampStartPosition;

    private void Start()
    {
        rampStartPosition = ramp.transform.localPosition;
    }
    

    public void ActivateRamp(bool active)
    {
    
        rampMat.GetComponent<MeshRenderer>().enabled = active;
        rampMat.GetComponent<MeshCollider>().enabled = active;
        var colour = ramp.GetComponentInParent<PlatformColourManager>();
        var t = colour.materials[colour.CurrentColour];
        var y = rampMat.GetComponent<MeshRenderer>();
        y.material = t;
    }

    public void SetRampRotation( int stairState)
    {
        ramp.transform.localRotation = Quaternion.Euler(ramp.
            transform.localRotation.eulerAngles.x, rampRotations[stairState],
            ramp.transform.localRotation.eulerAngles.z);
        CurrentRotation = stairState;
    }

    public bool ReturnRampValue()
    {
        PlatformRampActive = !PlatformRampActive;
        return PlatformRampActive;
    }
    
    private void RaisePlatform(bool active)
    {
        StartCoroutine(MovePlatform(active));
    }

    private IEnumerator MovePlatform(bool active)
    {
        Vector3 targetPosition;
        Vector3 startPosition = ramp.transform.position;
        float timer = 0f;
        float duration = 1f;
        float target;

        Debug.Log(ramp.transform.position.y);
        if (active)
            targetPosition = rampStartPosition;
        else
            targetPosition = new Vector3(rampStartPosition.x, rampStartPosition.y - 2,
                rampStartPosition.z);
        
         
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float percentage = Mathf.Min(timer / duration, 1);
            
            ramp.transform.position = Vector3.Lerp(startPosition,  targetPosition, percentage);
            yield return null;
        }

        ramp.transform.position = targetPosition;
        // ramp.transform.position = new Vector3(ramp.transform.position.x, target, ramp.transform.position.z);
    }
}