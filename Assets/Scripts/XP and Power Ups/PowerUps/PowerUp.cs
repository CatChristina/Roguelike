using UnityEngine;


public class PowerUp : MonoBehaviour
{
    public PowerUpBase[] powerUpBase;
    public PowerUpBase selectedPowerUp;
    [SerializeField] private PowerUps powerUpType;
    [SerializeField] private float value;
    private PlayerStats entity;
    private PlayerMovement playerMove;

    private void Awake()
    {
        entity = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();

        int chance = Random.Range(0, 99);
        Debug.Log("Chance = " + chance);

        if (chance <= 14 && chance >= 3)
        {
            selectedPowerUp = powerUpBase[1];
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        else if (chance >= 70)
        {
            selectedPowerUp = powerUpBase[3];
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            selectedPowerUp = powerUpBase[0];
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
        }

        powerUpType = selectedPowerUp.powerUps;
        value = selectedPowerUp.valueIncrease;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }

        switch (powerUpType)
        {
            case PowerUps.XP:
                entity.ModifyXP(value);
                break;

            case PowerUps.Health:
                entity.ModifyHealth(value);
                break;

            case PowerUps.Speed:
                entity.ModifySpeed(value);
                break;
            case PowerUps.Jump:
                entity.ModifyJumpCount(Mathf.RoundToInt(value));
                break;
            case PowerUps.Ammo:
                GameObject.FindGameObjectWithTag("PlayerGun").GetComponent<GunController>().RefillAmmo();
                break;
        }

        Destroy(gameObject);
    }
}
