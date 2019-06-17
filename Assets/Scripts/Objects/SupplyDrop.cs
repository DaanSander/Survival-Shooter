using UnityEngine;
using System.Collections;

public class SupplyDrop : MonoBehaviour {

    public Animator animator;

    [HideInInspector]
    public Equipment loot;
    [HideInInspector]
    public GameObject flare;
    private int animationHash;

    void Start() { 
        animationHash = Animator.StringToHash("foldin");
    }
    
    void OnCollisionEnter(Collision collison) {
        Debug.Log(collison.collider.gameObject.layer == LayerMask.NameToLayer("Floor"));
        if (collison.collider.gameObject.layer == LayerMask.NameToLayer("Floor")) animator.SetTrigger(animationHash);
        if (!collison.collider.gameObject.name.Equals("Player")) return;

        EquipmentManager.equipWeapon((Weapon)loot);
        SupplyDropManager.removeRequest(collison.collider.gameObject);

        Destroy(gameObject);
        Destroy(flare);
    }
}
