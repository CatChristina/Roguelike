using UnityEngine;

public class ProjectileStats : MonoBehaviour
{
    public int damage;
    public float velocity;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Camera.main.transform.forward * velocity * 5, ForceMode.VelocityChange);

        Destroy(gameObject, 5);
    }
}
