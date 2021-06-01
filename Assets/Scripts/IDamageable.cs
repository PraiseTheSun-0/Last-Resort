using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable 
{
    void takeDamage(float dmg, e_attackType type);
    //float damageMultiply(e_attackType);
}
