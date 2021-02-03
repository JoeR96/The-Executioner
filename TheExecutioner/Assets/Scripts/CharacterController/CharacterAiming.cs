using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class CharacterAiming : MonoBehaviour
{
    public float TurnSpeed = 15f;
    public Rig RigLayer;
    public float AimDuration = 0.15f;
    private Camera _camera;

    private RaycastWeapon _weapon;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _camera = Camera.main;
        _weapon = GetComponentInChildren<RaycastWeapon>();
    }

    private void LateUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            RigLayer.weight += Time.deltaTime / AimDuration;
        }
        else
        {
            RigLayer.weight -= Time.deltaTime / AimDuration;
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            _weapon.StartFiring();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _weapon.StopFiring();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float camRot = _camera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, 
            Quaternion.Euler(0, camRot, 0),
            TurnSpeed * Time.fixedDeltaTime);
    }
    
}
