using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GunController : MonoBehaviour
{
    private TMP_Text ammoText;
    private RectTransform reticle;

    private void Start()
    {
        reticle = GameObject.FindGameObjectWithTag("Reticle").GetComponent<RectTransform>();
        ammoText = GameObject.FindGameObjectWithTag("AmmoCounter").GetComponent<TMP_Text>();
        _currentAmmo = _maxAmmo;
        ammoText.text = new string('I', _currentAmmo);

        _canShoot = true;

        reticle.sizeDelta = new Vector2(spread * 50, spread * 50);
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

    public int shotsPerClick;
    public float timeBetweenShots; // Used for burst fire
    public bool automatic;

    public float spread;

    private bool _canShoot;

    public GameObject _bullet;
    public GameObject _shotPoint;


    // Fires the gun
    private void ShootGun()
    {

        if (_currentAmmo > 0)
        {
            _canShoot = false;
            _currentAmmo--;

            Invoke(nameof(ResetShot), _fireRate);

            int shotsLeft = shotsPerClick;

            for (int i = 0; shotsLeft > i;)
            {
                Instantiate(_bullet, _shotPoint.transform.position, Quaternion.identity);
                shotsLeft--;
            }
        }
        else
        {
            // Play Out of ammo sound
        }

        ammoText.text = new string('I', _currentAmmo);
    }

    // Readies the weapon to shoot
    private void ResetShot()
    {
        _canShoot = true;
    }

    // Refills the weapons ammo by 1/3 of the max ammo
    public void RefillAmmo()
    {
        _currentAmmo += _maxAmmo / 3;

        if (_currentAmmo > _maxAmmo)
        {
            _currentAmmo = _maxAmmo;
        }

        ammoText.text = new string('I', _currentAmmo);
    }
}
