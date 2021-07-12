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
    private float projectileSpeed = 10.0f;
    [SerializeField, Tooltip("How long between attacks")]
    private float attackCooldown = 2.0f;

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
        if (cooldownTimer >= attackCooldown)
        {
            GameObject newProjectile = Instantiate(projectile);
            newProjectile.transform.position = new Vector2(transform.position.x, transform.position.y) + offset;
            newProjectile.transform.rotation = Quaternion.LookRotation(fireDirection);
            newProjectile.transform.Rotate(newProjectile.transform.up * 90.0f);

            if (newProjectile.TryGetComponent<ProjectileController>(out ProjectileController controller))
            {
                controller.InitialiseProjectile(fireDirection, projectileSpeed);
            }

            cooldownTimer = 0.0f;
        }
    }
}
