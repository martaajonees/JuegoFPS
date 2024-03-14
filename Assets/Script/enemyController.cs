using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyController : MonoBehaviour, IDamage
{
    public Transform target;
    public Transform weapon;

    public float shootDistance = 10f;
    public float shootInterval = 2f;
    float shootTime;
    float distanceToTarget;

    public int life = 5;
    NavMeshAgent agent;

    public float distanceToChase = 3f;
    public float chaseInterval = 2f;
    float chaseTime;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        shootTime = shootInterval;
        chaseTime = chaseInterval;
    }

    public bool DoDamage(int vld, bool isPlayer)
    {
        Debug.Log("Enemy recibe " + vld + " de daño " + isPlayer);
        if(isPlayer == true){
            life -= vld;
            if(life <= 0){
                Die();
            }
            return true;
        }
        return false;
    }

    void Die() {
        Destroy(gameObject);
    }

    void Update()
    {
        Vector3 posNoRot = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.LookAt(posNoRot);
        distanceToTarget = Vector3.Distance(transform.position, target.position);

        Chase();

        ShootControl();
    }

    void ShootControl()
    {
        shootTime -= Time.deltaTime;
        if(shootTime < 0)
        {
            if(distanceToTarget < shootDistance)
            {
                shootTime = shootInterval;
                GameObject bullet = ObjectPollingManager.instance.GetBullet(false);
                bullet.transform.position = weapon.position;
                bullet.transform.LookAt(target.position);
            }
            
        }
    }

    void Chase(){
        chaseTime -= Time.deltaTime;
        if(chaseTime < 0)
        {
            if(distanceToTarget > distanceToChase)
            {
                agent.SetDestination(target.position);
                agent.stoppingDistance = distanceToChase;
                chaseTime = chaseInterval;
            }
        }
    }
}
