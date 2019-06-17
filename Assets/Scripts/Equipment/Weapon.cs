using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(menuName = "Equipment/Weapon")]
public class Weapon : Equipment {

    public RandomizedAudio shootAudio;
    public float damage, fireRate;
    public GameObject gunPrefab;

    [Space(10)]
    public bool scatterShot = false;
    public int pellets = 1;
    public float spread = 5.0f;
}