using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class RotatingPlatform : MonoBehaviour, ICheckDistanceFromPlayer
{
    private Transform _playerTransform;

    private Quaternion _originalRotation;
    // Start is called before the first frame update
    void Start()
    {
        _originalRotation = transform.rotation;
        _playerTransform = GameObject.FindWithTag("Player").transform;
    }
    
    

    [SerializeField] private GameObject _distanceCheckPoint;
    [SerializeField] private float _areaSize;
    
    public bool PlayerIsInTheArea()
    {
        if (Vector3.Distance(_distanceCheckPoint.transform.position, _playerTransform.position) >= _areaSize)
        {
            return true;
        }

        return false;
    }

    public void ReturnToOriginalRotation()
    {
        transform.rotation = _originalRotation;
    }
}

public interface ICheckDistanceFromPlayer
{
    bool PlayerIsInTheArea();
}
