using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileBase : MonoBehaviour
{
    [SerializeField] float _travelSpeed = 2f;
    [SerializeField] float _damage = 1f;
    [SerializeField] GameObject _impactEffect = null;
    [SerializeField] AudioClip _fireSound = null;
    [SerializeField] AudioClip _impactSound = null;

    protected float Damage
    {
        get
        {
            return _damage;
        }
        set
        {
            _damage = value;
        }
    }

    protected float TravelSpeed {
        get
        {
            return _travelSpeed;
        }
        set
        {
            _travelSpeed = value;
        }
    } //todo: validate max/min speed

    [SerializeField] Rigidbody _rb = null;

    void Awake()
    {
        _rb.GetComponent<Rigidbody>();
        _rb.useGravity = false;

        AudioHelper.PlayClip2D(_fireSound, 1f);
    }

    void FixedUpdate()
    {
        Move();
    }

    protected virtual void Move()
    {
        //get the new position
        Vector3 moveOffset = transform.forward * _travelSpeed;
        //move the rigidbody
        _rb.MovePosition(_rb.position + moveOffset);
        //_rb.AddForce(moveOffset);
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Projectile has hit " + other);
        ImpactEffect(other);
    }

    protected virtual void ImpactEffect(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player == null) //if hit anything other than player, explode
        {
            //call damage function to whatever it hit
            HealthBase health = other.gameObject.GetComponent<HealthBase>();
            if (health != null)
                health.TakeDamage(Damage);
            //destroy this projectile
            Explode();
        }
    }

    protected virtual void Explode()
    {
        //make hit effect
        GameObject impactGO = Instantiate(_impactEffect, transform.position, transform.rotation);
        Destroy(impactGO, 3f); //eventually destroy
                               //play sounds
        AudioHelper.PlayClip2D(_impactSound, .5f);

        Destroy(gameObject);//delete itself
    }
}
