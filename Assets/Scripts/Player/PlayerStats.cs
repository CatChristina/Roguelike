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

    [SerializeField] private float _xp = 0;
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth = 100;

    private void Awake()
    {
        playerMove = GetComponent<PlayerMovement>();
        xpSlider.value = _xp;
        _health = _maxHealth;
        healthSlider.maxValue = _maxHealth;
        healthSlider.value = _health;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(40);
        }
    }

    public void ModifyXP(float xpAmount)
    {
        _xp += xpAmount;

        if (_xp > xpSlider.maxValue)
        {
            _xp -= xpSlider.maxValue;

            xpSlider.maxValue += 20;

            ModifyHealth(10);
        }

        xpSlider.value = _xp;
    }

    public void ModifyHealth(float healthAmount)
    {
        _maxHealth += healthAmount;
        _health = _maxHealth;
        healthSlider.value = _health;

        healthText.text = _health.ToString();
    }
    public void ModifySpeed(float speedAmount)
    {
        playerMove._maxSpeed += speedAmount;
        playerMove._moveSpeed += speedAmount;
    }

    public void ModifyJumpCount(int value)
    {
        playerMove._maxJumps += value;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        healthSlider.value = _health;
        healthText.text = _health.ToString();

        if (_health <= 0)
        {

        }
    }
}
