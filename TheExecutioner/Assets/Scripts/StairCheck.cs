using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class StairCheck : MonoBehaviour
{
    [SerializeField] private GameObject stair;
    [SerializeField] private GameObject raycastHolder;

    public int X;
    public int Y;
    private int[] rotations = new int[4];
    // Start is called before the first frame update


    public void SetInt(int x, int z)
    {
        X = x;
        Y = z;
    }
    private void SetRandomRotation()
    {
        Debug.Log("SET");
        var random = Random.Range(0, rotations.Length);
        var yRotation = rotations[random];
        transform.localRotation = Quaternion.Euler(0, yRotation, 0);

    }
    private void CheckDistance()
    {
        Ray raycast; 
        RaycastHit rayHit;
        Vector3 origin = raycastHolder.transform.position;
        Vector3 direction = -raycastHolder.transform.up;

        if (Physics.Raycast(origin, direction, out rayHit, 45f))
        {
            Debug.Log(origin);
            Debug.Log(rayHit.collider.transform.position);
            transform.position = rayHit.point;
            Debug.Log(transform.position);
            Debug.Log(rayHit.collider.transform.position);
        }
    }

}
