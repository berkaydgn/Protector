using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    GameObject _target;
    NavMeshAgent _agent;
    public int _health;
    public float _enemyDamage;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        //_gameManager = GetComponent<GameManager>();
    }

    public void GoalSetting(GameObject target)
    {
        _target = target;
    }

    void Update()
    {
        _agent.SetDestination(_target.transform.position);    
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Target"))
        {
            GameObject _gameManager = GameObject.FindWithTag("GameManager");
            _gameManager.GetComponent<GameManager>().HealthBar(_enemyDamage);
            Dead();
        }
    }
}
