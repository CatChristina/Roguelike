using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemStore : MonoBehaviour
{
    public int money;
    public float moneyMult;
    public TMP_Text costTMP;
    public TMP_Text description;
    public TMP_Text interactionTMP;

    private GameObject player;
    private GunController _currentWeapon;
    private WeaponController weaponController;

    private ItemData itemData;

    private void Start()
    {
        money = 0;
        storeUI = GameObject.FindGameObjectWithTag("StoreUI");
        weaponUI = GameObject.FindGameObjectWithTag("WeaponUI");
        itemUI = GameObject.FindGameObjectWithTag("ItemUI");
        itemUI.SetActive(false);
        weaponUI.SetActive(false);
        storeUI.SetActive(false);
        interactionTMP.text = "";
        player = GameObject.FindGameObjectWithTag("Player");
        weaponController = GameObject.FindGameObjectWithTag("GunContainer").GetComponent<WeaponController>();
    }
    
    private bool _isInTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionTMP.text = "Press F to open store";
            _isInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionTMP.text = "";
            _isInTrigger = false;
        }
    }

    // Close store UI when player presses escape
    private void Update()
    {
        // Closes the store UI when the player presses escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoBackOneMenu();
        }

        // Open store UI when player is in trigger zone and presses F
        if (Input.GetKeyDown(KeyCode.F) && _isInTrigger)
        {
            OpenStoreUserInterface();
        }
    }

    public void GoBackOneMenu()
    {
        switch (true)
        {
            // bool _ matches the condition of the switch statement and is discarded afterwards
            case bool _ when weaponUI.activeSelf:
                CloseWeaponUserInterface();
                break;
            case bool _ when itemUI.activeSelf:
                CloseItemUserInterface();
                break;
            default:
                CloseStoreUserInterface();
                break;
        }
    }

    public TMP_Text moneyHUD;
    public TMP_Text moneyStore;
    // Add money to player's account
    public void AddMoney(int amount)
    {
        money += Mathf.CeilToInt(amount * moneyMult);
        moneyHUD.text = "$" + money.ToString();
        moneyStore.text = "$" + money.ToString();
    }

    private bool PurchaseItem(int itemCost)
    {
        if (money >= itemCost)
        {
            money -= itemCost;
            return true;
        }
        else
        {
            description.text = "Not enough money!";
            return false;
        }
    }

    private GameObject storeUI;
    // Open store UI
    private void OpenStoreUserInterface()
    {
        Time.timeScale = 0;
        storeUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseStoreUserInterface()
    {
        Time.timeScale = 1;
        storeUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private GameObject weaponUI;
    // Open weapon UI
    public void OpenWeaponUserInterface()
    {
        weaponUI.SetActive(true);
    }

    public void CloseWeaponUserInterface()
    {
        weaponUI.SetActive(false);
    }

    private GameObject itemUI;
    // Open item UI
    public void OpenItemUserInterface()
    {
        itemUI.SetActive(true);
    }

    public void CloseItemUserInterface()
    {
        itemUI.SetActive(false);
    }

    public GameObject[] weapons;
    // Purchases a weapon and sets it as the player's second weapon if it's not already equipped
    public bool PurchaseWeapon(GameObject weapon)
    {
        if (weapon != weaponController.weapon1 && weapon != weaponController.weapon2)
        {
            if (weaponController.weapon2 == null || weaponController.weapon2.activeInHierarchy)
            {
                weaponController.weapon2 = weapons[1];
                return true;
            }
            else if (weaponController.weapon1.activeInHierarchy)
            {
                weaponController.weapon1 = weapons[1];
                return true;
            }
            PurchaseItem(weapons[1].GetComponent<GunController>().cost);
        }
        return false;
    }

    public int healthCost = 50;
    // Increases player health by a set amount
    public void PurchaseHealth()
    {
        if (PurchaseItem(itemData.cost))
        {
            player.GetComponent<PlayerStats>().ModifyHealth(20);
            itemData.cost += healthCost;
            UpdateStoreUI();
        }
    }

    public int ammoCost = 10;
    // Refills player ammo for the current weapon
    public void PurchaseAmmo()
    {
        GunController gunController = GameObject.FindGameObjectWithTag("PlayerGun").GetComponent<GunController>();

        if (gunController._currentAmmo == gunController._maxAmmo)
        {
            description.text = "Ammo is full!";
            return;
        }

        if (PurchaseItem(itemData.cost))
        {
            gunController.RefillAmmo();
            UpdateStoreUI();
        }
    }

    public int jumpCost = 1000;
    // Increases player jump count by one
    public void PurchaseJump()
    {
        if (PurchaseItem(itemData.cost))
        {
            player.GetComponent<PlayerStats>().ModifyJumpCount(1);
            itemData.cost += jumpCost;
            UpdateStoreUI();
        }
    }

    // Purchases the sniper rifle
    public void PurchaseSniper()
    {
        PurchaseWeapon(weapons[1]);
        UpdateStoreUI();
    }

    // Checks for the cursor entering a button and displays the item's description and cost
    public void OnHoverEnter(Button button)
    {
        itemData = button.GetComponent<ItemData>();

        description.text = itemData.description;
        UpdateStoreUI();
    }

    // Checks if the cursor has left the button and clears the description and cost
    public void OnHoverExit()
    {
        description.text = "";
        costTMP.text = "";
    }

    private void UpdateStoreUI()
    {
        moneyHUD.text = "$" + money.ToString();
        moneyStore.text = "$" + money.ToString();
        costTMP.text = "$" + itemData.cost.ToString();
    }
}
