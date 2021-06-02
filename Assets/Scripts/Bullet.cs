using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour{ 
    public float dmg = 1;
    public GameObject target;

    private void Start() {
        gameObject.AddComponent<ActionOnTimer>().SetTimer(4f, () => { Destroy(gameObject); });
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null) {
            damageable.takeDamage(dmg, e_attackType.NORMAL);
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Obstacle") {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == target)
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.takeDamage(dmg, e_attackType.NORMAL);
                Destroy(gameObject);
            }
        }
    }
}
