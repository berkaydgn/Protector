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
    private GameObject _gameManager;
    Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _gameManager = GameObject.FindWithTag("GameManager");
        _agent = GetComponent<NavMeshAgent>();
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Target"))
        {
            _gameManager.GetComponent<GameManager>().HealthBar(_enemyDamage);
            Dead();
        }
    }

    void Dead()
    {
        _animator.SetTrigger("Dead");
        _gameManager.GetComponent<GameManager>().EnemiesCountUpdate();
        Destroy(gameObject, 5f);
    }
}
