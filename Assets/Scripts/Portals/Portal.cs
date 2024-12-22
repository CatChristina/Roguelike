using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject destination;
    public Texture renderTexture;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.transform.position = destination.transform.position;
            other.gameObject.transform.rotation = destination.transform.rotation;
        }
    }
}
