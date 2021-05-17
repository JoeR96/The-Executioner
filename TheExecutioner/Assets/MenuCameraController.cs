using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraController : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField]
    private Transform lookAtTarget;
    [SerializeField]
    private List<Transform> cameraPositions = new List<Transform>();
    [SerializeField]
    private Transform lastPosition;
    [SerializeField]
    private Transform currentMount;
    private int mountToShow;
    private float speed = 0.0125f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SetNextPosition", 0, 9);
    }


    private void Update()
    {
        transform.LookAt(lookAtTarget);
        transform.position = Vector3.Lerp(transform.position, currentMount.transform.position, speed);
        transform.rotation = Quaternion.Slerp(transform.rotation, currentMount.transform.rotation, 0.0125f);
    }
    private void SetNextPosition()
    {
        particleSystem.Play();
        mountToShow = Random.Range(0, cameraPositions.Count);
        currentMount = cameraPositions[mountToShow];
    }
}
