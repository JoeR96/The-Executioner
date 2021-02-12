using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{

    private RotationManager _rotationManager;

    // Start is called before the first frame update
    void Start()
    {
        _rotationManager = GetComponent<RotationManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            StartCoroutine(RotatePlatform());
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            ResetPlatform();
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            LowerPlatform();
        }
    }

    

    [SerializeField] private Transform[] _rotatingPlatforms;
    [SerializeField] private Quaternion[] _rotatingPlatformRotations;
    private float _rotationDuration = 0.1f;
    
    private IEnumerator RotatePlatform()
    {
        foreach (var platform in _rotatingPlatforms)
        {
            var t = platform.GetComponent<ICheckDistanceFromPlayer>();
            if (!t.PlayerIsInTheArea())
            {


            }
            else
            {
                StartCoroutine(StartMovement(platform));

            }

            yield return null;
        }

    }

    private IEnumerator StartMovement(Transform platform)
    {
        StartCoroutine(LerpTransformPosition(platform, new Vector3(platform.position.x,platform.position.y - 2.5f, platform.position.z), _rotationDuration));
        yield return new WaitForSeconds(0.1f);
        _rotationManager.RotateGameObject(platform);
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(LerpTransformPosition(platform, new Vector3(platform.position.x,platform.position.y + 2.5f, platform.position.z), _rotationDuration));
    }
    private void ResetPlatform()
    {
        foreach (var platform in _rotatingPlatforms)
        {
            StartCoroutine(ResetPlatforms(platform));
        }
    }

    private IEnumerator ResetPlatforms(Transform platform)
    {
        
        yield return new WaitForSeconds(0.1f);
        platform.GetComponent<RotatingPlatform>().ReturnToOriginalRotation();
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(LerpTransformPosition(platform, new Vector3(platform.position.x,platform.position.y + 2.5f, platform.position.z), _rotationDuration));
    }
    
    private void LowerPlatform()
    {
        foreach (var platform in _rotatingPlatforms)
        {
            StartCoroutine(LowerPlatforms(platform));
        }
    }
    private IEnumerator LowerPlatforms(Transform platform)
    {
        StartCoroutine(LerpTransformPosition(platform, new Vector3(platform.position.x,platform.position.y - 2.5f, platform.position.z), _rotationDuration));
        yield return null;
    }


    public IEnumerator LerpTransformPosition(Transform transformToLerp, Vector3 target, float duration)
    {
        Transform startPosition = transformToLerp;
        float timer = 0f;
        float _duration = duration;
        while (timer < _duration)
        {
            Debug.Log(timer);
            timer += Time.deltaTime;
            float percentage = Mathf.Min(timer / _duration, 1);
            Debug.Log(transformToLerp);
            transformToLerp.position = Vector3.Lerp(startPosition.position, target, percentage);
            yield return null;
        }
    }
}


