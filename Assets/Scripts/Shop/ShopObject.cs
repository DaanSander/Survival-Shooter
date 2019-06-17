using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Shop/ShopObject")]
public class ShopObject : ScriptableObject {

    public int price;
    public new string name;
    public string description;
    public Equipment equipment;
}
