using UnityEngine;


public class PowerUp : MonoBehaviour
{
    public PowerUpBase[] powerUpBase;
    public PowerUpBase selectedPowerUp;
    [SerializeField] private PowerUps powerUpType;
    [SerializeField] private float value;
    private PlayerStats playerStats;
    private PlayerMovement playerMove;

    // Sets the PowerUp type, color and value based on a random chance, gets the PlayerStats script to
    private void Awake()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        RandomPowerUp();

        objRenderer = gameObject.GetComponent<Renderer>();
        Invoke(nameof(DestroyPowerUp), 30);
    }

    // Randomly selects a PowerUp from the PowerUpBase array
    private void RandomPowerUp()
    {
        int chance = Random.Range(1, 100);
        Debug.Log("Chance = " + chance);

        if (chance <= 10)
        {
            selectedPowerUp = powerUpBase[1];
            gameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 0); // Red
        }
        else if (chance <= 40)
        {
            selectedPowerUp = powerUpBase[3];
            gameObject.GetComponent<Renderer>().material.color = new Color(0, 1, 0); // Green
        }
        else //if (chance <= 55)
        {
            selectedPowerUp = powerUpBase[0];
            gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 1); // Blue
        }

        powerUpType = selectedPowerUp.powerUps;
        value = selectedPowerUp.valueIncrease;
    }

    // Checks against the PowerUps enum to determine which function to call
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }

        switch (powerUpType)
        {
            case PowerUps.XP:
                playerStats.ModifyXP(value);
                break;

            case PowerUps.Health:
                playerStats.ModifyHealth(value);
                break;

            case PowerUps.Speed:
                playerStats.ModifySpeed(value);
                break;
            case PowerUps.Jump:
                playerStats.ModifyJumpCount(Mathf.RoundToInt(value));
                break;
            case PowerUps.Ammo:
                GameObject.FindGameObjectWithTag("PlayerGun").GetComponent<GunController>().RefillAmmo();
                break;
        }

        Destroy(gameObject);
    }

    private int Counter = 50;
    private Renderer objRenderer;
    // Destroys the PowerUp once Counter reaches 0
    private void DestroyPowerUp()
    {

        objRenderer.enabled = !objRenderer.enabled;

        Invoke(nameof(DestroyPowerUp), 0.01f * Counter);

        Counter--;

        if (Counter <= 0)
        {
            Destroy(gameObject);
        }
    }
}
