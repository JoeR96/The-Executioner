using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocomotion : MonoBehaviour
{
    private Animator _animator;
    private CharacterController _characterController;
    private Vector2 _input;
    private Vector3 _rootMotion;

    public float JumpHeight;

    public float Gravity;
    public float StepDown;
    
    private Vector3 _velocity;
    private bool _isJumping;

    public float Aircontrol;

    public float GroundSpeed;
    public float Damping;
    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RecieveInput();
        SetAnimatorfloat();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
    private void RecieveInput()
    {
        _input.x = Input.GetAxis("Horizontal");
        _input.y = Input.GetAxis("Vertical");
    }
    private void SetAnimatorfloat()
    {
        _animator.SetFloat("inputX",_input.x);
        _animator.SetFloat("inputY",_input.y);
    }

    private void OnAnimatorMove()
    {
        _rootMotion += _animator.deltaPosition;
    }
    //Move character controller in fixed update so rigcontroller runs in update
    private void FixedUpdate()
    {
        if (_isJumping)
        {
            CharacterInAir();
        }
        else
        {
            CharacterGrounded();
        }
       



    }

    private void CharacterInAir()
    {
        _velocity.y -= Gravity * Time.fixedDeltaTime;
        Vector3 displacement = _velocity * Time.fixedDeltaTime;
        displacement += CalculateAirMovement();
        _characterController.Move(displacement);
        _isJumping = !_characterController.isGrounded;
        _rootMotion = Vector3.zero;
        _animator.SetBool("IsJumping",_isJumping);
    }
    private void CharacterGrounded()
    {
        {
            Vector3 stepForwardAmount = _rootMotion * GroundSpeed;
            Vector3 stepDownAmount = Vector3.down * StepDown;
            
            _characterController.Move(stepForwardAmount + stepDownAmount);
            _rootMotion = Vector3.zero;
            if (!_characterController.isGrounded)
            {
                SetInAir(0);
            }
        }
    }

    private void Jump()
    {
        if (!_isJumping)
        {
            float jumpVelocity = Mathf.Sqrt(2 * Gravity * JumpHeight);
            SetInAir(jumpVelocity);
           
        }
    }

    private void SetInAir(float jumpVelocity)
    {
        _isJumping = true;
        _velocity = _animator.velocity * Damping * GroundSpeed;
        _velocity.y = jumpVelocity;
        _animator.SetBool("IsJumping",true);
    }

    private Vector3 CalculateAirMovement()
    {
        return ((transform.forward * _input.y) + (transform.right * _input.x)) *(Aircontrol / 100);
    }
}
