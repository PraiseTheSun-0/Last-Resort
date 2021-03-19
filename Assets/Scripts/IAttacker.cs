using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttacker
{
    enum attackType
    {
        PIERCING,
        MAGIC,
        NORMAL,
        SIEGE,
        CHAOS
    }
    double damage { get; set; }
    double attackSpeed { get; set; }

    void Attack(IDamageable target);
}
