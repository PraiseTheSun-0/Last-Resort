using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttacker
{
    void Attack(IDamageable target, e_attackType type);
}
