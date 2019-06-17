using UnityEngine;
using System.Collections.Generic;

public class SupplyDropManager : MonoBehaviour {

    public int height;
    public GameObject flarePrefab;
    public GameObject supplyDropPrefab;
    public GameObject[] supplyDropSpawns;

    private static List<GameObject> requestedDrops = new List<GameObject>(1);

    void Update() {
        reloadRequests();
    }

    public void requestDrop(Equipment equipment) {
        if (isRequested(equipment)) {
            Debug.Log("Already Requested");
            return;
        }
        Vector3 supplyLocation = supplyDropSpawns[Random.Range(0, supplyDropSpawns.Length)].transform.position;

        GameObject flare = (GameObject)Instantiate(flarePrefab, new Vector3(supplyLocation.x, 40, supplyLocation.z), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        GameObject supplyDrop = (GameObject)Instantiate(supplyDropPrefab, new Vector3(supplyLocation.x, height, supplyLocation.z)
                                                        , supplyDropPrefab.transform.localRotation);

        supplyDrop.GetComponent<SupplyDrop>().flare = flare;
        supplyDrop.GetComponent<SupplyDrop>().loot = equipment;

        requestedDrops.Add(supplyDrop);
    }

    void reloadRequests() {
        for (int i = 0; i < requestedDrops.Count; i++)
            if (requestedDrops[i] == null) requestedDrops.RemoveAt(i);
    }

    public static void removeRequest(GameObject gameObject) {
        requestedDrops.Remove(gameObject);
    }

    public static bool isRequested(Equipment equipment) {
        for (int i = 0; i < requestedDrops.Count; i++)
            if (requestedDrops[i].GetComponent<SupplyDrop>().loot.name.Equals(equipment.name)) return true;
        return false;
    }
}
