using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour, IDamageable
{
    public int cost;
    public int crystalCost;
    public float HP;
    private float maxHP;
    public float armor;
    public int team;

    public Slider hpbar_slider;
    public Text hpbar_values;
    private void Start()
    {
        maxHP = HP;
    }

    public void takeDamage(float dmg, e_attackType type)
    {
        this.HP -= dmg;
        if (hpbar_slider != null)
            hpbar_slider.value = HP / maxHP;

        if(hpbar_values != null) {
            hpbar_values.text = HP + "/" + maxHP;
        }

        if(HP <= 0)
        {
            Die();
        }

    }

    private void Die()
    {
        Destroy(gameObject);
    }

}
