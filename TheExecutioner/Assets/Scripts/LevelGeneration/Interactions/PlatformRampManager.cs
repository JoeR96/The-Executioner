using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRampManager : MonoBehaviour
{
    public bool PlatformRampActive;
    public int CurrentRotation;
    [SerializeField] private List<float> rampRotations = new List<float>();
    [SerializeField] public GameObject ramp;
    
    

    public void ActivateRamp(bool active)
    {
        ramp.GetComponentInChildren<MeshRenderer>().enabled = active;
        ramp.GetComponentInChildren<MeshCollider>().enabled = active;
        //StartCoroutine(ChangeRampHeight(active));
    }

    private IEnumerator ChangeRampHeight(bool active)
    {
        
        if (active)
        {
            ramp.GetComponentInChildren<MeshRenderer>().enabled = active;
            ramp.GetComponentInChildren<MeshCollider>().enabled = active;
           // RaisePlatform();
        }
        else
        {
            //RaisePlatform();
            yield return new WaitForSeconds(1f);
            ramp.GetComponentInChildren<MeshRenderer>().enabled = active;
            ramp.GetComponentInChildren<MeshCollider>().enabled = active;
        }
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
    
    private void RaisePlatform()
    {
        StartCoroutine(MovePlatform());
    }

    private IEnumerator MovePlatform()
    {
        Vector3 startPosition = ramp.transform.position;
        
        float timer = 0f;
        float duration = 1f;
        float target;
        
        if (PlatformRampActive)
            target = 18f;
        else
            target = 16f;

        Vector3 targetPosition = new Vector3(ramp.transform.position.x
            , target, ramp.transform.position.z);
         
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