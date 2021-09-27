using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyBase
{
    /*
     * Boss Pattern: Have 4 spots it move to.
     * Transform at certain speed to a random spot
     * At a random time, lung at player
     * Lunge Attack: Boss pick point where player is, stops for a moment, then moves to that spot. Resume normal pattern
     * Innovation: Land mine fall in at random spot on map
     * 
    */

    [SerializeField] GameObject _location1 = null;
    [SerializeField] GameObject _location2 = null;
    [SerializeField] GameObject _location3 = null;
    [SerializeField] GameObject _location4 = null;
    [SerializeField] GameObject _normalArt = null;
    [SerializeField] GameObject _angryArt = null;
    [SerializeField] Renderer _body = null;
    [SerializeField] Transform _target = null;
    [SerializeField] GameObject _mine = null;
    [SerializeField] GameObject _lazer = null;

    [SerializeField] float glompDamage = 1f;
    [SerializeField] float normalMoveSpeed = .3f;
    [SerializeField] float angryMoveSpeed = 1f;
    [SerializeField] float fireRate = 5f;
    [SerializeField] float phaseChangeRate = 6f;
    [SerializeField] float waitRate = 2f;

    int phase = 0; //phase: 0 - move between points, 1 - lunge attack, 2 - spawn minion
    int location;
    float projLifetime = 5f;
    bool reachedDestination;
    bool targetAquired, bombReady;
    float nextTimeToFire;
    float nextTimeToChangeAttack;
    float nextWakeTime;
    Vector3 selectedPoint = new Vector3(0,0,0);
    Vector3 targetPosition = new Vector3(0,0,0);
    Material bodyMaterial;

    // Start is called before the first frame update
    void Start()
    {
        reachedDestination = true;
        targetAquired = false;
        bombReady = false;
        nextTimeToFire = 2f;
        nextTimeToChangeAttack = 2f;
        nextWakeTime = 2f;
        _normalArt.SetActive(true);
        _angryArt.SetActive(false);

        bodyMaterial = _body.material;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //countdown timer to change phase
        if (Time.time >= nextTimeToChangeAttack)
        {
            //wait another phaseChange seconds + between 1 or 5 seconds
            nextTimeToChangeAttack = Time.time + phaseChangeRate + Random.Range(1,5);
            //change phase to random
            phase = (int)Random.Range(1, 3);
            
            //Debug.Log("It is a phase, mom. New Phase: " + phase);
        }

        //phase: 0 - move between points, 1 - lunge attack, 2 - spawn minion, 3 - cutscene
        switch (phase)
       {
            case 0:
               if (reachedDestination) //check if we need a new point to move to
                   SelectNewMovement();
               MoveToPoint(selectedPoint, normalMoveSpeed); //move 
                                                                 //check if its time to fire gun
                                                            //if reloadTime is == 0, call shoot script to fire gun
               if (Time.time >= nextTimeToFire)
               {
                   nextTimeToFire = Time.time + fireRate;
                   //Debug.Log("Distance between enemy and player == " + distance);
                   FireWeapon();
               }
               break;

            case 1:
                LungeAtPlayer();
                nextTimeToFire = Time.time + fireRate; //reset time to fire
                break;
            case 2: SpawnMine();
                break;
            case 3: //do nothing :>
                break;
           default:
                phase = 0;
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
    
    void MoveToPoint(Vector3 spot, float speed)
    {
        //call when it is time to move.
        transform.position = Vector3.MoveTowards(transform.position, spot, speed);
        if(Vector3.Distance(transform.position, spot) <= 4)//if we are close enough, then get a new point to find
        {
            reachedDestination = true;
        }
    }

    void FireWeapon()
    {
        //Debug.Log("Boss goes pew!");
        
        GameObject lazerGO = Instantiate(_lazer, transform.position, Quaternion.identity);
        //rotate lazer towards player
        if(_target != null)
            lazerGO.transform.LookAt(_target);
        lazerGO.SetActive(true);

        Destroy(lazerGO, projLifetime);
    }

    void LungeAtPlayer()
    {
        //wait a sec before continuing
        if (Time.time >= nextWakeTime)
        {
            if (!targetAquired)
            { //should run the first cycle of this script
                //Debug.Log("OwO whats, this? *glomps you cutely*");
                //get player's position
                if(_target != null)
                    targetPosition = _target.position;
                reachedDestination = false;
                targetAquired = true;

                //on next cycle, make the boss do nothing for a bit
                nextWakeTime = Time.time + waitRate;
            }

            if (reachedDestination) //return to normal steps
            {
                //Debug.Log("Glomp succ-cessful! Returning to normal.");
                reachedDestination = false;
                targetAquired = false;
                phase = 0;
                //return();//too much of a headache to not move one more frame after reaching target
            }

            //lunge at player
            MoveToPoint(targetPosition, angryMoveSpeed);
        }
    }

    void SpawnMine()
    {
        if (!bombReady)//if this is the first time, then start the timer
        {
            bombReady = true;
            nextWakeTime = Time.time + waitRate * .5f; //wait half as long to deploy bombs
        }

        transform.Rotate(0, 15f, 0, Space.Self);//spin boss around

        if (Time.time >= nextWakeTime)
        {
            //Debug.Log("Bombs? It's yours my friend.");

            //spawn a land mine object
            //make a copy anywhere from x: -22 to 22, y: 20, z: -27 to 10
            Vector3 minePosition = new Vector3(Random.Range(-22, 22), 20, Random.Range(-27, 10));

            GameObject mineGO = Instantiate(_mine, minePosition, Quaternion.identity);
            mineGO.SetActive(true);
            //return to normal phase
            bombReady = false;
            transform.rotation = Quaternion.Euler(0,0,0);//rotate back to front
            phase = 0;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)//we have hit player
        {
            Debug.Log("Boss has hit " + other);
            //call damage function to player
            HealthBase health = player.gameObject.GetComponent<HealthBase>();
            if (health != null)
                health.TakeDamage(glompDamage);
            //make the boss not lunge anymore (if player runs into it, then boss runs away)
            reachedDestination = true;
        }
    }

    public void AngryTime()
    {
        //call when the boss health is low
        //double the speed and half the wait times!
        normalMoveSpeed *= 2f;  //.3f
        angryMoveSpeed *= 2f;    //1f
        fireRate /= 2f;          //5f
        phaseChangeRate /= 2f;   //6f
        waitRate /= 2f;          //2f

        //return to normal attack pattern
        phase = 0;
    }

    public void TurnAround()
    {
        phase = 3;
        //set x rotation to -100 to face down
        transform.Rotate(-160f, 0, 0, Space.Self);
    }

    public void ChangeForm()
    {
        //hopefully rotate back
        transform.Rotate(160f, 0, 0, Space.Self);
        _normalArt.SetActive(false);
        _angryArt.SetActive(true);
    }

    IEnumerator ChangeColor() //DOESN'T WORK!!! AAAAAAHHH
    {
        //_body.material.SetColor("_Color", Color.yellow);
        bodyMaterial.color = Color.yellow;
        
        yield return new WaitForSeconds(1f);

        //_body.material.SetColor("_Color", Color.red);
        bodyMaterial.color = Color.red;
    }
}
