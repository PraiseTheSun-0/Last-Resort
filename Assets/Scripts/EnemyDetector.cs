using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{

    public List<GameObject> friendlies;
    public List<GameObject> enemies;

    private void Start()
    {
        friendlies = new List<GameObject>();
        enemies = new List<GameObject>();
    }

    private void Update()
    {
        foreach (GameObject go in friendlies)
        {
            if (go == null) friendlies.Remove(go);
        }
        foreach (GameObject go in enemies)
        {
            if (go == null) enemies.Remove(go);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<IDamageable>() != null)
        {
            if (other.gameObject.GetComponent<Unit>())
            {
                if (other.gameObject.GetComponent<Unit>().team == GetComponentInParent<Unit>().team)
                {
                    friendlies.Add(other.gameObject);
                }
                else
                {
                    enemies.Add(other.gameObject);
                }
            }else if (other.gameObject.GetComponent<Building>())
            {
                if(other.gameObject.GetComponent<Building>().team != GetComponentInParent<Unit>().team) enemies.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<IDamageable>() != null)
        {
            friendlies.Remove(other.gameObject);
            enemies.Remove(other.gameObject);
        }
    }

}
