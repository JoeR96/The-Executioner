using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Rigidbody _playerRB;
    [SerializeField] private AudioSource _playerAudio;



    
    [SerializeField] private float _moveSpeed = 6f;
    private float _jumpForce = 4f;
    private float _walkSpeed = 6f;
    private float _sprintSpeed = 10f;
    private float _sprintSpeedUpTimer = 0.5f;
    
    public bool _isGrounded;
    private bool _playerIsMoving;
    private bool _isSprinting;

    private Quaternion _handIdlePosition;
    private Quaternion _handSprintPosition;

    private float coyoteTime;
    
    // Update is called once per frame
    private void Start()
    {
        _playerRB.detectCollisions = true;
    }
    private void Update()
    {
        Movement();

    }
    
    //Refactor this in to smaller parts
    private void Movement()
    {
        ToggleSprint();
        float x = Input.GetAxisRaw("Horizontal") * _moveSpeed;
        float y = Input.GetAxisRaw("Vertical") * _moveSpeed;
        
        //Player movement
        Vector3 movePos = transform.right * x + transform.forward * y;
        Vector3 newMovePos = new Vector3(movePos.x,_playerRB.velocity.y,movePos.z);
        _playerRB.velocity = newMovePos;
        
        //check if our player is grounded
         _isGrounded = Physics.CheckSphere(
             new Vector3(transform.position.x, transform.position.y - 1, transform.position.z),
             0.2f, _layerMask);
         
        
        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _playerRB.velocity = new Vector3(_playerRB.velocity.x, _jumpForce, _playerRB.velocity.z);
                
        }
        
        if (!_playerAudio.isPlaying && _playerIsMoving)
        {
            _playerAudio.volume = Random.Range(0.8f, 1f);
            _playerAudio.pitch = Random.Range(0.8f, 1.1f);
            _playerAudio.Play();
        }
        
    }

    private void ToggleSprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(LerpSpeed(_sprintSpeed,_handSprintPosition));
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            StartCoroutine(LerpSpeed(_walkSpeed,_handIdlePosition));
        }

    }

    protected IEnumerator LerpSpeed(float targetSpeed, Quaternion targetPosition)
    {

        float currentSpeed = _moveSpeed;
        float _timer = 0;
        while(_timer < _sprintSpeedUpTimer)
        {
            float percentage = Mathf.Min(_timer / _sprintSpeedUpTimer, 1);
            _timer += Time.deltaTime;
            _moveSpeed = Mathf.Lerp(currentSpeed, targetSpeed, percentage);
            yield return null;
        }
    }


    

}

