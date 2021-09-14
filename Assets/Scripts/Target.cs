using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : HealthBase
{
    //destroy self when it takes damage
    public override void TakeDamage(float damageTaken)
    {
        Kill();
    }
}
