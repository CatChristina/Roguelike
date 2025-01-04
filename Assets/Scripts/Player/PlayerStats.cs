using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    private PlayerMovement playerMove;

    public TMP_Text speedText;
    public TMP_Text healthText;

    public Slider healthSlider;
    public Slider xpSlider;

    private void Awake()
    {
        playerMove = GetComponent<PlayerMovement>();
        xpSlider.value = _xp;
        _health = _maxHealth;
        healthSlider.maxValue = _maxHealth;
        healthSlider.value = _health;
    }

    // Damages the player if they collide with an enemy
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(40);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
    }


    [SerializeField] private float _xp = 0;
    // Gives the player XP
    public void ModifyXP(float xpAmount)
    {
        _xp += xpAmount;

        if (_xp > xpSlider.maxValue)
        {
            _xp -= xpSlider.maxValue;

            xpSlider.maxValue += 20;
            healthSlider.maxValue = _maxHealth;

            ModifyHealth(10);
        }

        xpSlider.value = _xp;
    }

    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth = 100;
    // Increases the players max health and heals them
    public void ModifyHealth(float healthAmount)
    {
        _maxHealth += healthAmount;
        _health = _maxHealth;
        healthSlider.value = _health;

        healthText.text = _health.ToString();
    }

    // Increases the players speed (Currently Unused)
    public void ModifySpeed(float speedAmount)
    {
        playerMove._maxSpeed += speedAmount;
        playerMove._moveSpeed += speedAmount;
    }

    // Gives the player more jumps
    public void ModifyJumpCount(int value)
    {
        playerMove._maxJumps += value;
    }

    // Deals damage to the player
    public void TakeDamage(int damage)
    {
        _health -= damage;
        healthSlider.value = _health;
        healthText.text = _health.ToString();

        if (_health <= 0)
        {
            //Dead
        }
    }
}
