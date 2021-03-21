
using UnityEngine;

public class CharacterAiming : MonoBehaviour
{
    public float TurnSpeed = 15f;
    public float AimDuration = 0.15f;
    private Camera _camera;
    public Transform cameraLookAt;
    public Cinemachine.AxisState xAxis;
    public Cinemachine.AxisState yAxis;
    private RaycastWeapon _weapon;

    private Animator animator;
    // Start is called before the first frame update
    private int isAimingParam = Animator.StringToHash("IsAiming");
    void Start()
    {
    
        animator = GetComponent<Animator>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _camera = Camera.main;
        _weapon = GetComponentInChildren<RaycastWeapon>();
    }

    private void Update()
    {
        bool isAiming = Input.GetMouseButton(1);
        animator.SetBool(isAimingParam,isAiming);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        xAxis.Update(Time.fixedDeltaTime);
        yAxis.Update(Time.fixedDeltaTime);
        cameraLookAt.eulerAngles = new Vector3(yAxis.Value, xAxis.Value,0);
        float camRot = _camera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, 
            Quaternion.Euler(0, camRot, 0),
            TurnSpeed * Time.fixedDeltaTime);
    }
    
}
