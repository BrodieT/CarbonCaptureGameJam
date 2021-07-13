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

}
