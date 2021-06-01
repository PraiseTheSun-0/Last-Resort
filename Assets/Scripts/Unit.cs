using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    private float maxHP;
    public float armor;
    public float damage;
    public float attackRange;
    public float attackSpeed;
    public float mana;
    public float moveSpeed;
    public int team;
    public e_armorType armorType;
    public e_attackType attackType;
    private bool attackReady = true;

    private UnitController uc;
    private SpriteRenderer sr;
    private Slider hpbar_slider;

    public GameObject deadPrefab;

    public CircleCollider2D enemy_detection_collider;

    private float attack_timer = 0f;

    private void Start()
    {
        uc = GameObject.Find("UnitController").GetComponent<UnitController>();
        sr = GetComponentInChildren<SpriteRenderer>();
        hpbar_slider = GetComponentInChildren<Slider>();
        attack_timer = attackSpeed;
        maxHP = HP;
    }

    private void Update()
    {
        if (attack_timer < attackSpeed*2) attack_timer += Time.deltaTime;

        if(maxHP != 0)
            hpbar_slider.value = HP / maxHP;

        if(team == 1)
        {
            sr.flipX = true;
        }

        MoveAwayFromTeammate();

        ContactFilter2D cf = new ContactFilter2D();
        cf.SetLayerMask(LayerMask.GetMask("Unit"+(team==0?"_team2":"_team1")));
        Collider2D[] colresults = new Collider2D[64];
        if (enemy_detection_collider.OverlapCollider(cf, colresults) > 0)
        {
            GameObject target = colresults[0].gameObject;
            float minDist = Vector3.Distance(transform.position, colresults[0].gameObject.transform.position);
            for(int i = 0; i < colresults.Length; i++)
            {
                if (!colresults[i]) break;

                float dist = Vector3.Distance(transform.position, colresults[i].gameObject.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    target = colresults[i].gameObject;
                }
            }
            if (Vector3.Distance(transform.position, target.transform.position) > attackRange)
            {
                MoveTo(target.gameObject.transform.position);
            }
            else
            {
                if(attack_timer >= attackSpeed)
                    Attack(target.gameObject.GetComponent<IDamageable>(), attackType);
            }
        }
        else
        {
            FlowFieldMove();
        }
    }

    public void FlowFieldMove()
    {
        Rigidbody2D unitRB = GetComponent<Rigidbody2D>();
        unitRB.AddForce(uc.whereToMove(transform.position, team) * moveSpeed * Time.deltaTime);
    }

    public void MoveTo(Vector3 target)
    {
        Rigidbody2D unitRB = GetComponent<Rigidbody2D>();
        Vector3 dir = (target - transform.position).normalized;
        unitRB.AddForce(dir * moveSpeed * Time.deltaTime);
    }

    public void MoveAway(Vector3 target)
    {
        Rigidbody2D unitRB = GetComponent<Rigidbody2D>();
        Vector3 dir = -(target);
        unitRB.AddForce(dir * moveSpeed * Time.deltaTime);
    }

    private void MoveAwayFromTeammate()
    {
        ContactFilter2D cf1 = new ContactFilter2D();
        cf1.SetLayerMask(LayerMask.GetMask("Unit" + (team == 0 ? "_team1" : "_team2")));
        Collider2D[] colresults1 = new Collider2D[64];
        if (enemy_detection_collider.OverlapCollider(cf1, colresults1) > 1)
        {
            Vector3 resultvector = new Vector3();
            for (int i = 1; i < colresults1.Length; i++)
            {
                if (colresults1[i])
                    if (Vector3.Distance(transform.position, colresults1[i].gameObject.transform.position) < attackRange)
                    {
                        resultvector += colresults1[i].gameObject.transform.position - transform.position;
                    }
            }
            MoveAway(resultvector.normalized);
        }
    }

    public void takeDamage(float dmg, e_attackType type)
    {
        this.HP -= dmg * (1.0f - Convert.ToSingle(Math.Log(armor)) * 0.2f); //TODO ARMOR LOGIC
        if(this.HP <= 0)
        {
            Instantiate(deadPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    //public float damageMultiply(e_attackType type)
    //{
    //    float multiplier;
    //    switch (type)
    //    {
    //        case
    //    }
    //    return multiplier;
    //}

    public void Attack(IDamageable target, e_attackType type)
    {
        target.takeDamage(damage, type);
        attack_timer = 0;
    }
}
