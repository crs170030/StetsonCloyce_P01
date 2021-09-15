using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour, IDamageable<float>
{
    [SerializeField] float maxHealth = 10f;
    [SerializeField] GameObject _deathEffect = null;
    [SerializeField] AudioClip _deathSound = null;
    public float currentHealth;

    void Start()
    {
        restoreHealth();
    }

    public void restoreHealth()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(float damageTaken)
    {
        //ouch chihuahua

        currentHealth -= damageTaken;
        Debug.Log(this.name + " was hit! Health is now " + currentHealth);
        if(currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        //time to die
        Debug.Log("You've... killed me... The Great " + this.name);

        if (_deathEffect != null)
        {
            //make hit effect
            GameObject deathGO = Instantiate(_deathEffect, transform.position, transform.rotation);
            Destroy(deathGO, 3f); //eventually destroy
        }
        if (_deathSound != null)
        {
            //play sounds
            AudioHelper.PlayClip2D(_deathSound, 1f);
        }


        Destroy(gameObject, .2f);
    }
}
