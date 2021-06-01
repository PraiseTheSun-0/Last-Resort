using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, IDamageable
{
    public int cost;
    public float HP;
    public float armor;
    public int team;

    public void takeDamage(float dmg, e_attackType type)
    {
        this.HP -= dmg;
    }

}
