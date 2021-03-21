
using UnityEngine;

public class CrosshairTarget : MonoBehaviour
{
    private Camera _camera;

    private Ray ray;

    private RaycastHit hitinfo;
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        ray.origin = _camera.transform.position;
        ray.direction = _camera.transform.forward;
        Physics.Raycast(ray, out hitinfo);
        transform.position = hitinfo.point;
    }
}
