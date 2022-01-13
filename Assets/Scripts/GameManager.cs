using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    string m_nextLevelName;
    [SerializeField]
    GameObject m_buttonContinue;
    [SerializeField]
    GameObject m_buttonRestart;


    public GameObject m_player;

    AutoShoot m_playerAutoShoot;
    HealthLogic m_healthLogic;

    [SerializeField]
    Text m_healthText;

    List<GameObject> m_enemies;

    GameObject m_level;

    void Start()
    {
        m_playerAutoShoot = m_player.GetComponent<AutoShoot>();
        m_playerAutoShoot.BulletLogic.OnShot += GameManager_OnShot;
        m_healthLogic = m_player.GetComponent<HealthLogic>();
        m_healthLogic.OnDamage += HealthLogic_OnDamage;
        m_enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        foreach(GameObject m_enemy in m_enemies)
        {
            AutoShoot m_enemyAutoShoot = m_enemy.GetComponent<AutoShoot>();
            m_enemyAutoShoot.m_currentTarget = m_player;
            m_enemyAutoShoot.BulletLogic.OnShot += GameManager_OnShot;
            HealthLogic m_enemyHealthLogic = m_enemy.GetComponent<HealthLogic>();
            m_enemyHealthLogic.OnDamage += HealthLogic_OnDamage;
        }
        SetHealthText();
    }

    private bool GameManager_OnShot(GameObject target, int damage)
    {
        if (target.tag == "Enemy" || target.tag == "Player")
        {
            target.GetComponent<HealthLogic>().TakeDamage(damage);
            return true;
        }
        return false;
    }

    private void HealthLogic_OnDamage(GameObject source, int health)
    {
        if (source == m_player)
        {
            SetHealthText();
        }
        if (health <= 0)
        {
            if (source == m_player)
            {
                m_buttonRestart.SetActive(true);
            }
            else
            {
                if (m_enemies.Contains(source))
                {
                    m_enemies.Remove(source);
                }
            }
            Destroy(source);
        }
    }

    public GameObject SearchClosestEnemy()
    {        
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = m_player.transform.position;
        foreach (GameObject go in m_enemies)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    void Update()
    {
       if (m_enemies != null &&  m_enemies.Count == 0)
        {
            m_buttonContinue.SetActive(true);
        }

        if (m_player)
        {
            if (m_playerAutoShoot.m_currentTarget == null)
            {
                m_playerAutoShoot.m_currentTarget = SearchClosestEnemy();
            }
        }
    }

    void SetHealthText()
    {
        if (m_healthText)
        {
            m_healthText.text = "Health: " +m_healthLogic.m_health;
        }
    }

   public void ButtonContinue()
    {
        SceneManager.LoadScene(m_nextLevelName,LoadSceneMode.Single);
    }

   public void ButtonRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

}
