using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEditor.Animations;
public class ActiveWeapon : MonoBehaviour
{
    public UnityEngine.Animations.Rigging.Rig HandRig;
    public Transform CrossHairTarget;
    private RaycastWeapon _weapon;
    public Transform WeaponLeftGrip;
    public Transform WeaponRightGrip;
    public Transform WeaponParent;

    private Animator _animator;

    private AnimatorOverrideController _animatorOverrideController;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animatorOverrideController = _animator.runtimeAnimatorController as AnimatorOverrideController;
        RaycastWeapon existingWeapon = GetComponentInChildren<RaycastWeapon>();
        _weapon = GetComponent<RaycastWeapon>();
        if (existingWeapon)
        {
            EquipWeapon(existingWeapon);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_weapon)
        {
            
            if (Input.GetKey(KeyCode.Mouse0))
            {
                _weapon.StartFiring();
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                _weapon.StopFiring();
            }
        }

        else
        {
            HandRig.weight = 0f;
            _animator.SetLayerWeight(1, 0.0f);
        }
    }

    public void EquipWeapon(RaycastWeapon newWeapon)
    {
        if (_weapon)
        {
            Destroy(_weapon.gameObject);
        }
        _weapon = newWeapon;
        _weapon.RaycastDestination = CrossHairTarget;
        _weapon.transform.parent = WeaponParent;
        _weapon.transform.localPosition = Vector3.zero;
        _weapon.transform.localRotation = quaternion.identity;
        HandRig.weight = 1f;
        _animator.SetLayerWeight(1,1.0f);
        Invoke(nameof(SetDelayAnimation),0.001f);
        
    }

    private void SetDelayAnimation()
    {
        _animatorOverrideController["weapon_anim_empty"] = _weapon.WeaponAnimation;
    }
    [ContextMenu("Save weapon pose")]
    private void SaveWeaponPose()
    {
        GameObjectRecorder recorder = new GameObjectRecorder(gameObject);
        recorder.BindComponentsOfType<Transform>(WeaponParent.gameObject,false);
        recorder.BindComponentsOfType<Transform>(WeaponLeftGrip.gameObject,false);
        recorder.BindComponentsOfType<Transform>(WeaponRightGrip.gameObject,false);
        recorder.TakeSnapshot(0.0f);
        recorder.SaveToClip(_weapon.WeaponAnimation);
    }
}
