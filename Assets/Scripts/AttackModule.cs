using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackModule : MonoBehaviour
{
    [SerializeField, Tooltip("The projectile being fired")]
    private GameObject projectile = default;
    [SerializeField, Tooltip("The spawn location for the projectiles")]
    private Vector2 offset = new Vector2(1, 0);
    [SerializeField, Tooltip("The direction the projectile will be fired in")]
    private Vector2 fireDirection = Vector2.right;
    [SerializeField, Tooltip("How fast the projectile will go")]
    private float projectileSpeed = 200.0f;
    [SerializeField, Tooltip("How long between attacks")]
    private float attackCooldown = 2.0f;

    [SerializeField]
    [Range(1, 5)]
    private int burstNum = 3;
    [SerializeField]
    [Range(0.1f, 1)]
    float burstCooldown = 0.1f;
    int currentBurst = 0; 

    private float cooldownTimer = 0.0f;

    private void Awake()
    {
        cooldownTimer = attackCooldown;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
    }


    public virtual void Attack()
    {
            GameObject newProjectile = Instantiate(projectile);
            newProjectile.transform.position = new Vector2(transform.position.x, transform.position.y) + offset;
            newProjectile.transform.rotation = Quaternion.LookRotation(fireDirection);
            newProjectile.transform.Rotate(newProjectile.transform.up * 90.0f);

            if (newProjectile.TryGetComponent<ProjectileController>(out ProjectileController controller))
            {
                controller.InitialiseProjectile(fireDirection, projectileSpeed);
            }

    }

    public void BurstAttack()
    {
        GameObject newProjectile = Instantiate(projectile);
        newProjectile.transform.position = new Vector2(transform.position.x, transform.position.y) + offset;
        newProjectile.transform.rotation = Quaternion.LookRotation(fireDirection);
        newProjectile.transform.Rotate(newProjectile.transform.up * 90.0f);

        if (newProjectile.TryGetComponent<ProjectileController>(out ProjectileController controller))
        {
            controller.InitialiseProjectile(fireDirection, projectileSpeed);
        }

        currentBurst++;
        if(currentBurst < burstNum)
        {
            Invoke("BurstAttack", burstCooldown);
        }
        else
        {
            CancelInvoke("BurstAttack");
            currentBurst = 0;
        }

    }

    public void SprayAttack()
    {
        if (cooldownTimer >= attackCooldown)
        {
            float angleIncrement = sprayRange / maxSprayCount;
            Vector3 originPos = new Vector2(transform.position.x, transform.position.y) + offset;
            int max = (maxSprayCount / 2);
            if (max % 2 > 0)
            {
                max++;
            }

            int min = -maxSprayCount / 2;

            for (int i = min; i < max; i++)
            {
                Quaternion q = Quaternion.AngleAxis(angleIncrement * i, Vector3.forward);

                GameObject newProjectile = Instantiate(projectile);
                newProjectile.transform.position = originPos + q * Vector3.right * 3.0f;
                newProjectile.transform.rotation = q;
                //newProjectile.transform.Rotate(newProjectile.transform.up * 90.0f);
                if (newProjectile.TryGetComponent<ProjectileController>(out ProjectileController controller))
                {
                    Vector3 moveDir = (newProjectile.transform.position - originPos).normalized;
                    controller.InitialiseProjectile(moveDir, projectileSpeed);
                }
            }


            cooldownTimer = 0.0f;
        }
    }

    [SerializeField]
    public float sprayRange = 45.0f;
 
    [SerializeField, Min(1)]
    public int maxSprayCount = 3;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + new Vector3(offset.x, offset.y, transform.position.z), 0.25f);
    }
}
