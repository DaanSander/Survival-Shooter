using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(EnemyController))]
public class EnemyHealthbar : MonoBehaviour {

    public Color almostDead = new Color(0.0f, 0.0f, 0.0f, 1.0f), healthy = new Color(0.0f, 0.0f, 0.0f, 1.0f);
    public float fadeOutAfter = 5.0f;
    public Slider healthBarPrefab;
    [HideInInspector]
    public Slider healthBar;
    private Image fillImage;
    private GameObject canvas;
    private Renderer renderer;
    private EnemyController enemyController;

    void Start() {
        canvas = GameObject.Find("HUDCanvas");
        if (canvas == null) throw new System.NullReferenceException("No canvas in the scene!");


        GameObject healthBar = Instantiate(healthBarPrefab.gameObject);
        healthBar.transform.SetParent(canvas.transform);

        Image[] images = healthBar.GetComponentsInChildren<Image>();
        for (int i = 0; i < images.Length; i++) {
            if (images[i].name.Equals("Fill")) {
                fillImage = images[i];
                break;
            }
        }

        this.healthBar = healthBar.GetComponent<Slider>();
        enemyController = GetComponent<EnemyController>();
        renderer = GetComponentInChildren<Renderer>();

        this.healthBar.maxValue = enemyController.enemy.maxHealth;
        this.healthBar.minValue = 0.0f;
        this.healthBar.gameObject.SetActive(false);
    }

    public bool destroyed() {
        return healthBar == null;
    }

    void Update() {
        if (healthBar != null) {
            if (enemyController.noAttackedTime >= fadeOutAfter || enemyController.health == enemyController.enemy.maxHealth)
                healthBar.gameObject.SetActive(false);
            else if (enemyController.noAttackedTime < fadeOutAfter || enemyController.health != enemyController.enemy.maxHealth) healthBar.gameObject.SetActive(true);

            fillImage.color = Color.Lerp(almostDead, healthy, enemyController.health / enemyController.enemy.maxHealth);


            healthBar.transform.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, renderer.bounds.max.y - renderer.bounds.max.y / 8, transform.position.z));
            healthBar.value = enemyController.health;
        }
    }
}
