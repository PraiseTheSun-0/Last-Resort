using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, IDamageable
{
    public int cost { get; set; }
    public double HP { get; set; }
    public double armor { get; set; }

    public void takeDamage(double dmg)
    {
        this.HP -= dmg;
    }
}
