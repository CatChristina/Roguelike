using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");

        InvokeRepeating(nameof(MoveToPlayer), 0, Random.Range(0.2f, 1.5f));
    }

    // Sets the enemies destination to the players position with a random delay
    private void MoveToPlayer()
    {
        agent.destination = player.transform.position;
    }
}
