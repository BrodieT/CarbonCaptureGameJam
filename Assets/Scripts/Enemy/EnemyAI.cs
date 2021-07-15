using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthManager))]
public class EnemyAI : MonoBehaviour
{
    IEnumerator LerpEnemyPosition()
    {
        if (maxTop && maxBottom)
        {
            Transform goalTransform = maxTop;

            while (!isDead)
            {
                enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, goalTransform.position, Time.deltaTime * speed);

                if (Vector2.Distance(enemy.transform.position, goalTransform.position) < 0.1f)
                {
                    if (goalTransform == maxTop)
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
    [Range(0, 10)]
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

    [SerializeField]
    GameObject enemyDrop = default;


    private HealthManager healthManager = default;

    // Start is called before the first frame update
    void Start()
    {

        healthManager = GetComponent<HealthManager>();
        healthManager.onDie.AddListener(() => KillEnemy());


        StartCoroutine(LerpEnemyPosition());
        StartCoroutine(EnemyAttacks());
    }

    public void KillEnemy()
    {
        isDead = true;
        StopAllCoroutines();

        GameObject drop = Instantiate(enemyDrop);
        drop.transform.position = transform.position;

        Destroy(gameObject);
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
                attackModule.SprayAttack();
                break;
            case Attacks.BURST:
                attackModule.BurstAttack();
                break; 
        }
    }


}
