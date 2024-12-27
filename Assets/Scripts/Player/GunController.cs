using TMPro;
using UnityEngine;

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

        reticle.sizeDelta = new Vector2(_spread * 50, _spread * 50);
    }

    private void Update()
    {
        if (Input.GetButton("Fire") && _automatic)
        {
            ShootGun();
        }
        else if (Input.GetButtonDown("Fire") && !_automatic)
        {
            ShootGun();
        }
    }

    [Header("Gun Statistics")]

    public int _damage;

    public int _currentAmmo;
    public int _maxAmmo;
    public float _fireRate;

    public int _shotsPerClick;
    public float _timeBetweenShots; // Used for burst fire
    public bool _automatic;

    public float _spread;

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

            int shotsLeft = _shotsPerClick;

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

    private void ResetShot()
    {
        _canShoot = true;
    }


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
