using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyBase
{
    /*
     * Boss Pattern: Have 4 spots it move to.
     * Transform at certain speed to a random spot
     * At a random time, lung at player
     * Lunge Attack: Boss pick point where player is, starts shaking, then moves to that spot. Resume normal pattern
     * Innovation: Make minions appear in random spots that chase the player
     * 
    */

    [SerializeField] GameObject _location1 = null;
    [SerializeField] GameObject _location2 = null;
    [SerializeField] GameObject _location3 = null;
    [SerializeField] GameObject _location4 = null;

    [SerializeField] float normalMoveSpeed = .3f;
    [SerializeField] float fireRate = 5f;
    int phase = 0; //phase: 0 - move between points, 1 - lunge attack, 2 - spawn minion
    int location;
    bool reachedDestination;
    float nextTimeToFire;
    Vector3 selectedPoint = new Vector3(0,0,0);

    // Start is called before the first frame update
    void Start()
    {
        reachedDestination = true;
        nextTimeToFire = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        //phase: 0 - move between points, 1 - lunge attack, 2 - spawn minion
        switch (phase) {
            case 0:
                if(reachedDestination) //check if we need a new point to move to
                    SelectNewMovement();
                MoveToPoint(selectedPoint); //move 
                                            //check if its time to fire gun
                //if reloadTime is == 0, call shoot script to fire gun
                if (Time.time >= nextTimeToFire)
                {
                    nextTimeToFire = Time.time + fireRate;
                    //Debug.Log("Distance between enemy and player == " + distance);
                    FireWeapon();
                }
                break;
        }
    }

    void SelectNewMovement()
    {
        reachedDestination = false;
        location = (int)Random.Range(1, 5);
        //TODO: Maybe make it to where it doesn't chose a point from 1 cycle before
        //Debug.Log("New Location is " + location);

        switch (location)
        {
            case 1:
                //MoveToPoint(_location1.transform.position);
                selectedPoint = _location1.transform.position;
                break;
            case 2:
                //MoveToPoint(_location2.transform.position);
                selectedPoint = _location2.transform.position;
                break;
            case 3:
                //MoveToPoint(_location3.transform.position);
                selectedPoint = _location3.transform.position;
                break;
            case 4:
                //MoveToPoint(_location4.transform.position);
                selectedPoint = _location4.transform.position;
                break;
            default: //MoveToPoint(_location1.transform.position);
                selectedPoint = _location1.transform.position;
                break;
        }
    }
    
    void MoveToPoint(Vector3 spot)
    {
        //call when it is time to move.
        transform.position = Vector3.MoveTowards(transform.position, spot, normalMoveSpeed);
        if(Vector3.Distance(transform.position, spot) < 5)//if we are close enough, then get a new point to find
        {
            reachedDestination = true;
        }
    }

    void FireWeapon()
    {
        Debug.Log("Boss goes pew!");
    }
}
