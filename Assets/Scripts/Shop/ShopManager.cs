using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour {

    public GameObject buttonPrefab;
    public GameObject shopPanel;
    public ShopObject[] content;
    private List<ShopObject> boughtItems;
    private SupplyDropManager supplyManager;

    void Start() {
        shopPanel.SetActive(false);
        boughtItems = new List<ShopObject>(content.Length);
        supplyManager = GameObject.Find("GameManager").GetComponent<SupplyDropManager>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.B)) {
            if (shopPanel.activeSelf) {
                Time.timeScale = 1.0f;
                shopPanel.SetActive(false);
            }
            else {
                shopPanel.SetActive(true);
                reloadShop();
                Time.timeScale = 0.35f;
            }
        }
    }

    bool containsChild(string name) {
        for (int i = 0; i < shopPanel.transform.childCount; i++)
            if (shopPanel.transform.GetChild(i).name.Equals(name)) return true;
        return false;
    }

    void reloadShop() {
        for (int i = 0; i < content.Length; i++) {
            GameObject buttonObject;

            if (containsChild(content[i].name))
                buttonObject = shopPanel.transform.Find(content[i].name).gameObject;
            else
                buttonObject = Instantiate(buttonPrefab);


            if (content[i].equipment.name.Equals(EquipmentManager.equippedWeapon.name) || SupplyDropManager.isRequested(content[i].equipment))
                buttonObject.SetActive(false);
            else
                buttonObject.SetActive(true);


            buttonObject.name = content[i].name;
            buttonObject.transform.SetParent(shopPanel.transform);

            buttonObject.GetComponent<Button>().onClick.AddListener(delegate { purchaseItem(buttonObject); });
            buttonObject.transform.Find("Name").GetComponent<Text>().text = content[i].name;
            buttonObject.transform.Find("Price").GetComponent<Text>().text = (boughtItem(buttonObject.name)) ? "Bought" : "" + content[i].price;
            buttonObject.transform.Find("Description").GetComponent<Text>().text = content[i].description;

            //gameObject.SetActive(true);
        }
    }

    bool boughtItem(string name) {
        return boughtItems.Contains(getShopItemByName(name));
    }

    ShopObject getShopItemByName(string name) {
        for (int i = 0; i < content.Length; i++)
            if (content[i].name.Equals(name)) return content[i];
        return null;
    }

    public void purchaseItem(GameObject button) {
        ShopObject shopObject = getShopItemByName(button.name);
        if (ScoreManager.score < shopObject.price) return;
        ScoreManager.score -= shopObject.price;

        boughtItems.Add(getShopItemByName(button.name));
        supplyManager.requestDrop(shopObject.equipment);
        reloadShop();
    }
}
