using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;

public class CharacterAiming : MonoBehaviour
{
    public float TurnSpeed = 15f;
    public Rig aimLayer;
    private Camera _camera;

    public float AimDuration = 0.3f;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _camera = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float camRot = _camera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, 
            Quaternion.Euler(0, camRot, 0),
            TurnSpeed * Time.fixedDeltaTime);
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Mouse1))
        {Aim();}
        else
        {
            aimLayer.weight -= Time.deltaTime / AimDuration;
        }
    }

    private void Aim()
    {
        aimLayer.weight += Time.deltaTime / AimDuration;
      
    }
}
