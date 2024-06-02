using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float rangeAwayFromPlayer;
    [SerializeField] float timeBetweenShots;
    [SerializeField] GameObject projectile;
    bool _hasAttacked;

    [SerializeField] enum enemyState { chasing, attacking }
    [SerializeField] enemyState state;

    [SerializeField] int health;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        handleState();
    }
    void handleState() {
        float dist = Vector3.Distance(transform.position, target.transform.position);
        if (dist > rangeAwayFromPlayer) {
            ChasePlayer();
        }else AttackPlayer();
    }
    void ChasePlayer() => agent.SetDestination(target.transform.position);
    void AttackPlayer() { 
        agent.SetDestination(transform.position);
        transform.LookAt(target.transform.position);
        if (!_hasAttacked) {
            Rigidbody bullet = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            bullet.velocity = target.transform.position - transform.position;
            _hasAttacked = true;
            Invoke(nameof(resetAttack), timeBetweenShots);
        }
    }
    void resetAttack() => _hasAttacked = false;
    public void Damage(int dmg) {
        health -= dmg;
        if(health <= 0) {
            SpawnEnemies spawn = GameObject.Find("Spawner").GetComponent<SpawnEnemies>();
            spawn.EnemyKilled();
            Destroy(this.gameObject);
        }
    }
}
