using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GunController : MonoBehaviour
{
    TMP_Text ammoText;

    void Start()
    {
        reticle = GameObject.FindGameObjectWithTag("Reticle").GetComponent<RectTransform>();
        ammoText = GameObject.FindGameObjectWithTag("AmmoCounter").GetComponent<TMP_Text>();
        _currentAmmo = _maxAmmo;
        ammoText.text = new string('I', _currentAmmo);
    }

    private RectTransform reticle;

    private void FixedUpdate()
    {
        reticle.sizeDelta = new Vector2(_spread * 2, _spread * 2);
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
    public float _timeBetweenShots;
    public bool _automatic;

    public float _spread;

    private bool _canShoot;

    public GameObject _bullet;
    public GameObject _shotPoint;

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
                Instantiate(_bullet, _shotPoint.transform.position, Quaternion.Euler(Random.Range(-_spread / 2, _spread / 2), Random.Range(-_spread / 2, _spread / 2),0));
                shotsLeft--;
            }
        }
        else
        {
            //Play Out of ammo sound
        }

        ammoText.text = new string('I', _currentAmmo);
    }

    private void ResetShot()
    {
        _canShoot = true;
    }


    public void RefillAmmo()
    {
        _currentAmmo += _maxAmmo / 2;
        ammoText.text = new string('I', _currentAmmo);
    }
}
