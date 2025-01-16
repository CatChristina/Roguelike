using TMPro;
using UnityEngine;

public class GunController : MonoBehaviour
{
    private TMP_Text ammoText;
    private RectTransform reticle;
    private AudioSource audioSource;
    private Animator animator;
    private Camera cam;

    private void Awake()
    {
        reticle = GameObject.FindGameObjectWithTag("Reticle").GetComponent<RectTransform>();
        ammoText = GameObject.FindGameObjectWithTag("AmmoCounter").GetComponent<TMP_Text>();
        _currentAmmo = _maxAmmo;
        ammoText.text = new string('I', _currentAmmo);
        cam = Camera.main;

        _canShoot = true;

        // Adds an audio source if one is not present
        if (gameObject.GetComponent<AudioSource>() == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Gets the animator if one is present
        if (gameObject.GetComponent<Animator>() != null)
        {
            animator = gameObject.GetComponent<Animator>();
        }
    }

    private void Start()
    {
        UpdateUI();
    }

    // Input checks
    private void Update()
    {
        if (Input.GetButton("Fire") && automatic && _canShoot)
        {
            ShootGun();
        }
        else if (Input.GetButtonDown("Fire") && !automatic && _canShoot)
        {
            ShootGun();
        }
    }

    [Header("Gun Statistics")]

    public int _damage;

    public int _currentAmmo;
    public int _maxAmmo;
    public float _fireRate;

    public int _shotsPerClick; // Used for multi-projectile weapons
    public float timeBetweenShots; // Used for burst fire
    public bool automatic;


    [Header("Spread and Recoil")]
    public float _spread;

    public bool _canShoot { get; private set; }

    [Header("Shop Info")]
    public int cost; // Cost of the weapon at the store

    [Header("Game Objects")]
    public GameObject bullet;
    public GameObject shotPoint;

    public GameObject muzzleFlash;

    [Header("Sound Effects")]
    public AudioClip gunShotClip;
    public AudioClip outOfAmmoClip;

    // Fires the gun
    private void ShootGun()
    {

        if (_currentAmmo > 0 && Time.timeScale != 0)
        {
            _canShoot = false;
            _currentAmmo--;

            Invoke(nameof(ResetShot), _fireRate);

            int shotsLeft = _shotsPerClick;

            for (int i = 0; shotsLeft > i;)
            {
                Instantiate(bullet, shotPoint.transform.position, Quaternion.identity);
                shotsLeft--;
            }

            // Gunshot sound effect
            if (gunShotClip != null)
            {
                audioSource.PlayOneShot(gunShotClip);
            }

            // Muzzleflash effect
            if (muzzleFlash != null)
            {
                GameObject _muzFlash = Instantiate(muzzleFlash, shotPoint.transform);
                Destroy(_muzFlash, 0.02f);
            }

            // Weapon animation
            if (animator != null)
            {
                animator.SetTrigger("Shoot");
            }
        }
        else
        {
            if (outOfAmmoClip != null)
            {
                audioSource.PlayOneShot(outOfAmmoClip);
            }
        }

        UpdateUI();
    }

    // Readies the weapon to shoot
    private void ResetShot()
    {
        _canShoot = true;

        if (animator != null)
        {
            animator.ResetTrigger("Shoot");
        }
    }

    // Refills the weapons ammo by 1/3 of the max ammo
    public void RefillAmmo()
    {
        _currentAmmo += _maxAmmo / 3;

        if (_currentAmmo > _maxAmmo)
        {
            _currentAmmo = _maxAmmo;
        }

        UpdateUI();
    }

    // Updates the reticle size based on the spread of the gun and the ammo counter based on the current ammo
    public void UpdateUI()
    {
        if (gameObject.activeInHierarchy)
        {
            reticle.sizeDelta = new Vector2(_spread * 40, _spread * 40);
            ammoText.text = new string('I', _currentAmmo);
        }
    }

    // Increases the damage of the gun
    public void IncreaseDamage(int damage)
    {
        _damage += damage;
    }

    // Increases the fire rate of the gun
    public void IncreaseFireRate(float fireRate)
    {
        _fireRate -= fireRate;

        if (timeBetweenShots != 0)
        {
            timeBetweenShots -= fireRate / 3;
            if (timeBetweenShots < 0.01f)
            {
                timeBetweenShots = 0.01f;
            }
        }
    }

    // Increases the max ammo of the gun
    public void IncreaseAmmo(int ammo)
    {
        _maxAmmo += ammo;
    }

    // Increases or decreases the spread of the gun by a multiplier (1.0 = base, 0.5 = half etc.)
    public void IncreaseSpread(float spreadMult)
    {
        _spread *= spreadMult;
    }

    // Increases the number of bullets per shot
    public void IncreaseBulletsPerShot(int bulletsPerShot)
    {
        _shotsPerClick += bulletsPerShot;
    }
}