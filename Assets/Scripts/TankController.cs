using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    //[SerializeField] GameObject turretBody = null;
    [SerializeField] Rigidbody turretBody = null;
    [SerializeField] Camera mainCamera = null;
    [SerializeField] GameObject missile = null;
    [SerializeField] GameObject firePoint = null;
    [SerializeField] GameObject muzzleFlash = null;

    [SerializeField] float _turnSpeed = 2f;
    [SerializeField] float projLifetime = 20f;

    [SerializeField] float _maxSpeed = .25f;
    public float MaxSpeed
    {
        get => _maxSpeed;
        set => _maxSpeed = value;
    }

    Rigidbody _rb = null;
    Vector3 projectilePosition;
    Vector3 pointToLook;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MoveTank();
        TurnTank();
        TurnTurret();
    }

    private void Update()
    {
        //check if the player has a button pressed, and fire gun if so
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("FIRE ZE MISSLES!");
            FireGun();
        }
    }

    public void MoveTank()
    {
        // calculate the move amount
        float moveAmountThisFrame = Input.GetAxis("Vertical") * _maxSpeed;
        // create a vector from amount and direction
        Vector3 moveOffset = transform.forward * moveAmountThisFrame;
        // apply vector to the rigidbody
        _rb.MovePosition(_rb.position + moveOffset);
        //also move the turret body along
        turretBody.MovePosition(turretBody.position + moveOffset);
    }

    public void TurnTank()
    {
        // calculate the turn amount
        float turnAmountThisFrame = Input.GetAxis("Horizontal") * _turnSpeed;
        // create a Quaternion from amount and direction (x,y,z)
        Quaternion turnOffset = Quaternion.Euler(0, turnAmountThisFrame, 0);
        // apply quaternion to the rigidbody
        _rb.MoveRotation(_rb.rotation * turnOffset);
    }

    public void TurnTurret()
    {
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition); //make ray cast
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero); //make a plane to check cast against
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength)) //if mouse raycast hits plane...
        {
            pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.cyan); //debug to show trajectory

            turretBody.transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }   

        //manually check if turret is turned at a bad angle!
        if(turretBody.rotation.x > 0)
        {
            //if x angle is positive, the turret aims at the ground!
            //turretBody.transform.rotation = Quaternion.Euler(0, 0, 0); //ERROR: sets angle to a rotation
            //turretBody.transform.rotation = new Quaternion(0, turretBody.rotation.y, turretBody.rotation.z, 1); //ERROR: Turret jiggles from facing north to proper position
        }
    }

    public void FireGun()
    {
        //TODO: * Make the missile have a time to fire so you can't spam it
        //      * 

        projectilePosition = firePoint.transform.position;

        //create projectile clone at turret
        GameObject lazerGO = Instantiate(missile, projectilePosition, turretBody.transform.rotation);
        lazerGO.SetActive(true);

        //make muzzle flash
        GameObject muzzleFlashGO = Instantiate(muzzleFlash, projectilePosition, turretBody.transform.rotation);
        Destroy(muzzleFlashGO, 0.1f);//destroy muzzle flash fast

        //play firing noise
        //_enemySounds.PlayOneShot(shootLaser, .5f);

        //destroy projectile after x seconds
        Destroy(lazerGO, projLifetime);
    }
}
