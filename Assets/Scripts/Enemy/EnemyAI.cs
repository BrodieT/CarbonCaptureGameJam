using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    IEnumerator LerpEnemyPosition()
    {
        Transform goalTransform = default;

        goalTransform = maxTop;
        float dist = 0;

        while(!isDead)
        {
            dist = Vector2.Distance(enemy.transform.position, goalTransform.position);

            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, goalTransform.position, Time.deltaTime * speed);

            if (dist < 0.1f)
            {
                if(goalTransform == maxTop)
                {
                    goalTransform = maxBottom;
                }
                else
                {
                    goalTransform = maxTop;
                }
            }

            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator EnemyAttacks()
    {
        float currentTime = 0;

        while(!isDead)
        {
            currentTime += Time.deltaTime;

            if(currentTime >= attackTime)
            {
                currentTime = 0;
                Attack();
                Debug.Log("ATTACK");
            }

            yield return new WaitForFixedUpdate();
        }
    }

    [SerializeField]
    Transform maxTop = default;

    [SerializeField]
    Transform maxBottom = default;

    [SerializeField]
    GameObject enemy = default;

    [SerializeField]
    [Range(1, 10)]
    float speed = 0;

    bool isDead = false;

    [SerializeField]
    [Range(1, 10)]
    float attackTime = 0;

    [SerializeField]
    [Range(1, 100)]
    int maxHealth = 0;

    int currentHealth = 0;

    [SerializeField]
    AttackModule attackModule = default;

    public enum Attacks { SINGLE, BURST, SPRAY }
    [SerializeField]
    List<Attacks> attackList = new List<Attacks>();
    int currentAttack = 0;

    [SerializeField]
    bool randomAttacks = default;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        StartCoroutine(LerpEnemyPosition());
        StartCoroutine(EnemyAttacks());
    }

    public void KillEnemy()
    {
        isDead = true;
        StopAllCoroutines();
    }

    public void HitEnemy(int damage)
    {
        currentHealth -= damage;

        if(currentHealth < 0)
        {
            KillEnemy();
        }
    }

    void Attack()
    {
        if (!randomAttacks)
        {
            currentAttack++;

            if (currentAttack > attackList.Count - 1)
            {
                currentAttack = 0;
            }
        }
        else
        {
            currentAttack = Random.Range(0, attackList.Count);
        }

        switch(attackList[currentAttack])
        {
            case Attacks.SINGLE: 
                attackModule.Attack();
                break;
            case Attacks.SPRAY:
                attackModule.Attack();
                break;
            case Attacks.BURST:
                attackModule.BurstAttack();
                break; 
        }
    }
}
