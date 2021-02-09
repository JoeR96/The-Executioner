using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingZombieDumper : MonoBehaviour
{
    public Transform PlayerTransform;
    public Transform DumperSpawnPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnZombie",2f,3.5f);
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget() {
        //the speed, in units per second, we want to move towards the target
        float speed = 5;
        //move towards the center of the world (or where ever you like)
        Vector3 targetPosition = PlayerTransform.position;

        Vector3 currentPosition = this.transform.position;
        //first, check to see if we're close enough to the target
        if(Vector3.Distance(currentPosition, targetPosition) > .1f) { 
            Vector3 directionOfTravel = targetPosition - currentPosition;
            //now normalize the direction, since we only want the direction information
            directionOfTravel.Normalize();
            //scale the movement on each axis by the directionOfTravel vector components

            transform.Translate(
                (directionOfTravel.x * speed * Time.deltaTime),
                (directionOfTravel.y * speed * Time.deltaTime),
                (directionOfTravel.z * speed * Time.deltaTime),
                Space.World);
        }
    }

    private void SpawnZombie()
    {
        GameManager.instance.ZombieSpawner.SpawnZombie(DumperSpawnPoint);
    }
}
