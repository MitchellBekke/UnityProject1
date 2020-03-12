using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    public float damageRange = 3;
    public int damage = 20;
    private float attackTime = 1;
    private float attackTimer;
    public float startSpeed;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        startSpeed = agent.speed;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.position);
        if (Vector3.Distance(transform.position, player.position) < damageRange)
        {
            if(attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
            else
            {
                player.GetComponent<PlayerHealth>().ApplyDamage(damage);
                agent.speed = 0;
                attackTimer = attackTime;
            }
           
        }
        else
        {
            agent.SetDestination(player.position);
            agent.speed = startSpeed;
        }
    }
}
