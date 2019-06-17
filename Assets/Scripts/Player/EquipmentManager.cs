using UnityEngine;
using System.Collections;

public class EquipmentManager : MonoBehaviour {

    public Weapon defaultWeapon;
    public static Weapon equippedWeapon { get; private set; }
    private static GameObject gunHolder;

    void Start() {
        gunHolder = GameObject.Find("Gunholder");
        equipWeapon(defaultWeapon);
    }

    public static void equipWeapon(Weapon weapon) {
        bool found = false;
        for (int i = 0; i < gunHolder.transform.childCount; i++) {
            if (gunHolder.transform.GetChild(i).gameObject.name.Equals(weapon.gunPrefab.name)) {
                gunHolder.transform.GetChild(i).gameObject.SetActive(true);
                found = true;
            }
            else {
                gunHolder.transform.GetChild(i).GetChild(0).gameObject.GetComponent<WeaponShooting>().CancelInvoke("shoot");
                gunHolder.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        if (!found) {
            GameObject gun = (GameObject)Instantiate(weapon.gunPrefab);

            gun.transform.SetParent(gunHolder.transform, false);
        }

        equippedWeapon = weapon;
    }
}
