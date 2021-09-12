using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : ProjectileBase
{
    [SerializeField] private float _acceleration = .004f;

    //[SerializeField] Rigidbody rb2;

    private void Start()
    {
        //set the starting speed to negative so missle moves back a little then flies forward
        TravelSpeed = -0.2f;
    }

    protected override void Move()
    {
        TravelSpeed += _acceleration;
        base.Move();
    }
}
