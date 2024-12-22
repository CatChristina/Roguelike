using UnityEngine;

public class ProjectileStats : MonoBehaviour
{
    public int damage;
    public float velocity;
    private Rigidbody rb;

    private void Start()
    {
        GunController gun = GameObject.FindGameObjectWithTag("PlayerGun").GetComponent<GunController>();
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Camera.main.transform.forward * velocity * 10 + new Vector3(Random.Range(-gun._spread, gun._spread), Random.Range(-gun._spread, gun._spread), 1), ForceMode.VelocityChange);

        damage = gun._damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            return;
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyStats>().TakeDamage(damage);
        }

        damage /= 2;
        
        if (damage < 1)
        {
            Destroy(gameObject, 0.01f);
        }

        Debug.Log("Dealt " + damage);
    }
}
