using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{  
    [SerializeField]
    int m_bulletDamage;

    [SerializeField]
    GameObject m_bullet;

    [SerializeField]
    Transform m_bulletSpawnPoint;
        
    Rigidbody m_bulletRigidbody;

    public delegate bool OnShotDelegate(GameObject target, int damage);
    public event OnShotDelegate OnShot;

    private void Start()
    {
        m_bulletRigidbody = GetComponent<Rigidbody>();
        m_bullet.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (OnShot != null)
        {
            if ( OnShot(other.gameObject, m_bulletDamage))
            {
                m_bullet.SetActive(false);
            }
        }
    }

    public void ShootBullet( float m_bulletSpeed)
    {
        Vector3 m_bulletStartPointPosition = m_bulletSpawnPoint.transform.position;
        Quaternion m_bulletStartPointRotation = m_bulletSpawnPoint.rotation;

        m_bullet.transform.position = new Vector3(m_bulletStartPointPosition.x, m_bulletStartPointPosition.y, m_bulletStartPointPosition.z);
        m_bullet.transform.rotation = new Quaternion(m_bulletStartPointRotation.x, m_bulletStartPointRotation.y, m_bulletStartPointRotation.z, m_bulletStartPointRotation.w);

        m_bulletRigidbody.velocity = m_bullet.transform.up * m_bulletSpeed;
        m_bullet.SetActive(true);
    }

    public static void ShootNewBullet(GameObject m_bulletPrefab, Transform m_bulletSpawnPoint, float m_bulletSpeed )
    {
        GameObject bullet = Instantiate(m_bulletPrefab, m_bulletSpawnPoint.position, m_bulletSpawnPoint.rotation * m_bulletPrefab.transform.rotation);        
        var m_bulletRigidbody = bullet.GetComponent<Rigidbody>();
        m_bulletRigidbody.velocity = bullet.transform.up * m_bulletSpeed;
    }
}
