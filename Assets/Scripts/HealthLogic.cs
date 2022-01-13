using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthLogic : MonoBehaviour
{

    public delegate void OnDamageDelegate(GameObject source, int health);
    public event OnDamageDelegate OnDamage;

    public int m_health=100;



    public void TakeDamage(int damage)
    {
        m_health -= damage;
        if (OnDamage != null)
        {
            OnDamage(gameObject, m_health);
        }

    }
}
