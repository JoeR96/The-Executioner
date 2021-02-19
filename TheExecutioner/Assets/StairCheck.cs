using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class StairCheck : MonoBehaviour
{
    [SerializeField] private GameObject stair;
    [SerializeField] private GameObject raycastHolder;
    // Start is called before the first frame update
    void Start()
    {
        SetRandomRotation();
        //Invoke("CheckDistance",0.5f);
    }

    private void SetRandomRotation()
    {
        List<Quaternion> list = new List<Quaternion>();
        var quartenionOne = new Quaternion(0,90,0,0);
        var quartenionTwo = new Quaternion(0,180,0,0);
        var quartenionThree = new Quaternion(0,270,0,0);
        var quartenionFour = new Quaternion(0,0,0,0);
        list.Add(quartenionOne);
        list.Add(quartenionTwo);
        list.Add(quartenionFour);
        list.Add(quartenionThree);
        var random = Random.Range(0, list.Count);
        var yRotation = list[random];
        stair.transform.rotation = yRotation;
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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGA");
        if (other.CompareTag("Cube"))
        {
            Debug.Log("HERE");
            stair.transform.position = new Vector3(stair.transform.position.x, stair.transform.position.y + 5f,
                stair.transform.position.z);

        }
    }
    
}
