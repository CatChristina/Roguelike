using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject weapon1;
    public GameObject weapon2;

    // Switches between weapons if the player has two weapons
    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2)) && weapon2 != null)
        {
            SwitchWeapon();
        }
    }

    private void SwitchWeapon()
    {
        // Checks to see if the weapons are firing and if not swaps weapon
        if ((weapon1.GetComponent<GunController>()._canShoot && weapon1.activeInHierarchy) || (weapon2.GetComponent<GunController>()._canShoot && weapon2.activeInHierarchy))
        {
            weapon1.SetActive(!weapon1.activeSelf);
            UpdateWeaponUserInterface(weapon1);
            weapon2.SetActive(!weapon2.activeSelf);
            UpdateWeaponUserInterface(weapon2);
        }
    }

    private void UpdateWeaponUserInterface(GameObject weapon)
    {
        weapon.GetComponent<GunController>().UpdateUI();
    }
}
