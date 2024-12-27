using System.Collections.Generic;
using UnityEngine;

public class ProjectileStats : MonoBehaviour
{
    public int damage;
    public float velocity;
    private Rigidbody rb;
    private HashSet<Collider> hitColliders;

    private void Start()
    {
        GunController gun = GameObject.FindGameObjectWithTag("PlayerGun").GetComponent<GunController>();
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Camera.main.transform.forward * velocity * 10 + new Vector3(Random.Range(-gun.spread, gun.spread), Random.Range(-gun.spread, gun.spread), 1), ForceMode.VelocityChange);

        hitColliders = new HashSet<Collider>();
        damage = gun._damage;

        Destroy(gameObject, 3);
    }

    private Vector3 previousPosition;
    // Does the raycast for collision detection
    private void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 direction = transform.position - previousPosition;
        float distance = direction.magnitude;

        if (Physics.Raycast(previousPosition, direction, out hit, distance))
        {
            if (hit.collider != null)
            {
                DoCollisionCheck(hit.collider);
            }
        }

        previousPosition = transform.position;
    }

    // Checks for collision with enemies and deals damage, divides damage upon any collision and continues until damage is less than 1
    private void DoCollisionCheck(Collider collider)
    {
        if (collider == null || hitColliders.Contains(collider) || collider.CompareTag("Player"))
        {
            return;
        }

        hitColliders.Add(collider);

        if (collider.gameObject.CompareTag("Projectile"))
        {
            return;
        }

        if (collider.CompareTag("Enemy"))
        {
            EnemyStats enemyStats = collider.GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                enemyStats.TakeDamage(damage);
                Debug.Log("Dealt " + damage);
            }
            else
            {
                Debug.LogError("EnemyStats component not found on the collider.");
            }
        }

        damage /= 2;

        if (damage < 1)
        {
            Destroy(gameObject, 0.001f);
        }
    }
}
