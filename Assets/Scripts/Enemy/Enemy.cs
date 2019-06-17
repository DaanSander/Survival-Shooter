using System.Collections;

[System.Serializable]
public class Enemy {

    public int points, introduceOn, maxPerwave;
    public float range, speed, damageOutput, maxHealth, regenAfter, regenPerSec;
    public bool regens = true;

    public Enemy(int introduceOn, int maxPerwave, float range, float speed, float damage, float maxHealth, float regenAfter, float regenPerSec, bool regens) {
        this.introduceOn = introduceOn;
        this.maxPerwave = maxPerwave;
        this.range = range;
        this.speed = speed;
        this.damageOutput = damage;
        this.maxHealth = maxHealth;
        this.regenAfter = regenAfter;
        this.regenPerSec = regenPerSec;
        this.regens = regens;
    }
}
