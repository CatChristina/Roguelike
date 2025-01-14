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

    private Renderer powerUpModelRef;
    // Randomly selects a PowerUp from the PowerUpBase array
    private void RandomPowerUp()
    {
        int chance = Random.Range(1, 100);

        if (chance <= 10)
        {
            selectedPowerUp = powerUpBase[1];
            gameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 0); // Red
        }
        else if (chance <= 20)
        {
            selectedPowerUp = powerUpBase[3];
            gameObject.GetComponent<Renderer>().material.color = new Color(0, 1, 0); // Green
        }
        else if (chance <= 25)
        {
            selectedPowerUp = powerUpBase[4];
            gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 0); // Yellow
        }
        else if (chance <= 75)
        {
            selectedPowerUp = powerUpBase[0];
            gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 1); // Blue
        }

        powerUpType = selectedPowerUp.powerUps;
        value = selectedPowerUp.valueIncrease;

        // Sets the PowerUp mesh to the selected PowerUp mesh
        if (selectedPowerUp.powerUpModel != null)
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            GameObject mesh = Instantiate(selectedPowerUp.powerUpModel, gameObject.transform);
            powerUpModelRef = mesh.GetComponent<Renderer>();
        }
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

    private int counter = 50;
    private Renderer objRenderer;
    // Destroys the PowerUp once Counter reaches 0
    private void DestroyPowerUp()
    {

        if (selectedPowerUp.powerUpModel != null)
        {
            powerUpModelRef.enabled = !powerUpModelRef.enabled;
        }
        else
        {
            objRenderer.enabled = !objRenderer.enabled;
        }


        Invoke(nameof(DestroyPowerUp), 0.01f * counter);

        counter--;

        if (counter <= 0)
        {
            Destroy(gameObject);
        }
    }
}
