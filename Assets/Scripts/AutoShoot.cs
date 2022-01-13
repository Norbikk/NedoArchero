using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShoot : MonoBehaviour
{

    public string m_EnemyTag;

    public GameObject m_currentTarget;

    [SerializeField]
    GameObject m_bullet;

    [SerializeField]
    float m_bulletSpeed = 35f; //Должно быть в оружии, которого нет.

    BulletLogic m_bulletLogic;

    public BulletLogic BulletLogic
    {
        get
        {
            if (m_bulletLogic == null)
            {
                m_bulletLogic = m_bullet.GetComponent<BulletLogic>(); 
            }
            return m_bulletLogic;
        }
    }

    void Update()
    {
        ShootEnemy();
    }   

 

    float m_timeFromLastShoot = 0;
    public float m_shootInterval = 1.0f;

    void ShootEnemy()
    {
        if (m_currentTarget != null)
        {
            Vector3 lookAt = m_currentTarget.transform.position;
            transform.LookAt(lookAt);
            if (m_bullet)
            {
                m_timeFromLastShoot += Time.deltaTime;
                if (m_timeFromLastShoot >= m_shootInterval)
                {
                    BulletLogic.ShootBullet(m_bulletSpeed);
                    m_timeFromLastShoot = 0;

                }
            }

        }
    }
}
