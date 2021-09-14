using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLazer : ProjectileBase
{
    protected override void ImpactEffect(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)//we have hit player
        {
            //call damage function to player
            HealthBase health = player.gameObject.GetComponent<HealthBase>();
            if (health != null)
                health.TakeDamage(Damage);
        }
        //regardless, destroy this projectile
        Explode();
    }
}
