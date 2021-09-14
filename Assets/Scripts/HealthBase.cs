using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour, IDamageable<float>
{
    [SerializeField] float maxHealth = 10f;
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
        Destroy(gameObject, 1f);
    }
}
