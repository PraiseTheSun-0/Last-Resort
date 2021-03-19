using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour, IDamageable, IAttacker
{
    public double HP { get; set; }
    public double armor { get; set; }
    public double damage { get; set; }
    public double attackSpeed { get; set; }
    public bool attackReady;
    public double mana { get; set; }

    public void takeDamage(double dmg)
    {
        this.HP -= dmg;
    }

    public void Attack(IDamageable target)
    {
        target.takeDamage(damage);
    }
}
