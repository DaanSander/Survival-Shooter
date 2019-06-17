using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {

    public Color critical = new Color(0.0f, 0.0f, 0.0f, 1.0f), healthy = new Color(0.0f, 0.0f, 0.0f, 1.0f);
    public Slider healthBar;
    public float maxHealth = 50.0f, regenMin = 10.0f, regenPerSec = 0.5f;
    [SerializeField]
    private float health;
    private float noAttackedTime = 0.0f;
    private Image fillImage;
    private float beginTime = 0.0f;

    void Start() {
        beginTime = Time.time;
        health = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.minValue = 0.0f;

        Image[] images = healthBar.GetComponentsInChildren<Image>();
        for (int i = 0; i < images.Length; i++) {
            if (images[i].name.Equals("Fill")) {
                fillImage = images[i];
                break;
            }
        }
    }

    public void damage(float damage) {
        noAttackedTime = 0f;
        health -= damage;
    }

    void Update() {
        noAttackedTime += Time.deltaTime;
        healthBar.value = health;
        fillImage.color = Color.Lerp(critical, healthy, health / maxHealth);
        if (health <= 0) {
            Analytics.CustomEvent("playerDeath", new Dictionary<string, object> {
                {"surviveTime", Time.time - beginTime },
                {"endScore", ScoreManager.score},
                {"equippedWeapon", EquipmentManager.equippedWeapon.name}
            });

            SceneManager.LoadScene("Main");
        }
        if (health < maxHealth && noAttackedTime >= regenMin) {
            if (health + regenPerSec * Time.deltaTime > maxHealth) {
                health = maxHealth;
                return;
            }
            health += regenPerSec * Time.deltaTime;
        }
    }

    void OnApplicationQuit() {
        Analytics.CustomEvent("sessionEnd", new Dictionary<string, object> {
                {"surviveTime", Time.time },
                {"score", ScoreManager.score},
                {"equippedWeapon", EquipmentManager.equippedWeapon.name},
                {"sessionEndTime", System.DateTime.Now.ToString() }
        });
    }
}
