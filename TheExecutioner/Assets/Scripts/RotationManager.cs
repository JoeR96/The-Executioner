using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationManager : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private Quaternion[] _rotations;



    public void RotateGameObject(Transform target)
    {
        var index = Random.Range(0, _rotations.Length);
        var rotation = _rotations[index];
        
        var toRotate = target.localRotation;
        Quaternion targetRotation = toRotate * rotation ;
        target.rotation = targetRotation;

        
    }
    
   
    public IEnumerator RotateGameObject(Quaternion target,Transform trans,float rotateTime)
    {
        var m_rotateTime = rotateTime;
        float timer = 0;
        var start = trans.transform.rotation;
        while (timer < m_rotateTime)
        {
            timer += Time.deltaTime;
            float percentage = Mathf.Min(timer / m_rotateTime, 1);

            trans.transform.rotation = Quaternion.Slerp(start, target, percentage);
            yield return null;
        }
    }

    
}
