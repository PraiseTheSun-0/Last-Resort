using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable 
{
    enum armorType
    {
        LIGHT,
        NORMAL,
        HEAVY,
        UNARMORED,
        FORTIFIED,
        HAVEL
    }
    double HP { get; set; }
    double armor { get; set; }
    void takeDamage(double dmg);
}
