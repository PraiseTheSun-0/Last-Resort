using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum e_attackType
{
    PIERCING,
    MAGIC,
    NORMAL,
    SIEGE,
    CHAOS
}
public enum e_armorType
{
    LIGHT,
    NORMAL,
    HEAVY,
    UNARMORED,
    FORTIFIED,
    HAVEL
}

public class Unit : MonoBehaviour, IDamageable, IAttacker
{

    public float HP;
    public float armor;
    public float damage;
    public float attackSpeed;
    public float mana;
    public float moveSpeed;
    public int team;
    public e_armorType armorType;
    public e_attackType attackType;
    private bool attackReady = true;

    private UnitController uc;
    private SpriteRenderer sr;

    private void Start()
    {
        uc = GameObject.Find("UnitController").GetComponent<UnitController>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if(team == 1)
        {
            sr.flipX = true;
        }
        FlowFieldMove();
    }

    public void FlowFieldMove()
    {
        Rigidbody2D unitRB = GetComponent<Rigidbody2D>();
        unitRB.AddForce(uc.whereToMove(transform.position, team) * moveSpeed * Time.deltaTime);
    }

    public void takeDamage(float dmg)
    {
        this.HP -= dmg;
    }

    public void Attack(IDamageable target)
    {
        target.takeDamage(damage);
    }
}
