using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : MonoBehaviour
{
    [SerializeField] float attackDamage = 3f;

    void OnCollisionEnter(Collision other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)//we have hit player
        {
            //Debug.Log("Landmine has hit " + other);
            //call damage function to player
            HealthBase health = player.gameObject.GetComponent<HealthBase>();
            if (health != null)
                health.TakeDamage(attackDamage);

            //destroy self
            GetComponent<Target>().Kill();
        }
    }
}
