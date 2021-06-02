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

    private UnitController uc;
    private SpriteRenderer sr;
    private Slider hpbar_slider;

    public GameObject deadPrefab;

    public EnemyDetector enemy_detector;

    private float attack_timer = 0f;

    private void Start()
    {
        uc = GameObject.Find("UnitController").GetComponent<UnitController>();
        sr = GetComponentInChildren<SpriteRenderer>();
        hpbar_slider = GetComponentInChildren<Slider>();
        enemy_detector = GetComponentInChildren<EnemyDetector>();
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

        if (enemy_detector.enemies.Count > 0)
        {
            GameObject target = enemy_detector.enemies[0];
            float minDist = Vector3.Distance(transform.position, enemy_detector.enemies[0].transform.position);
            for(int i = 0; i < enemy_detector.enemies.Count; i++)
            {
                if (!enemy_detector.enemies[i]) break;
        
                float dist = Vector3.Distance(transform.position, enemy_detector.enemies[i].transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    target = enemy_detector.enemies[i];
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
        if (enemy_detector.friendlies.Count > 0)
        {
            Vector3 resultvector = new Vector3();
            for (int i = 0; i < enemy_detector.friendlies.Count; i++)
            {
                if (enemy_detector.friendlies[i])
                    if (Vector3.Distance(transform.position, enemy_detector.friendlies[i].transform.position) < attackRange*0.9)
                    {
                        resultvector += enemy_detector.friendlies[i].transform.position - transform.position;
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
