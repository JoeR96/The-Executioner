using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    public float _mouseSensitivity = 5.0f;

    [SerializeField] 
    public Transform _playerTransform;

    private float _x;
    private float _y;

    private void Start()
    {
        //Lock the cursor from the screen
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        //Clamp the camera so we do not rotate out of desired field of view
        _x = Mathf.Clamp(_x, -75, 75);
        //Mouse input 
        _y += Input.GetAxis("Mouse X") * _mouseSensitivity;
        _x -= Input.GetAxis("Mouse Y") * _mouseSensitivity;
        
        //Camera rotation
        transform.localRotation = Quaternion.Euler(_x,0,0);
        _playerTransform.transform.localRotation = Quaternion.Euler(0, _y, 0);
        
        //Change cursor state
        if (Input.GetKeyDown(KeyCode.Escape) && Cursor.lockState == CursorLockMode.Locked)
            Cursor.lockState = CursorLockMode.None;
    }
}
