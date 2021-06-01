using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, IDamageable
{
    public int cost;
    public int crystalCost;
    public float HP;
    public float maxHP;
    public float armor;
    public int team;

    public void takeDamage(float dmg, e_attackType type)
    {
        this.HP -= dmg;
    }

    private void Start()
    {
        maxHP = HP;
    }
}
