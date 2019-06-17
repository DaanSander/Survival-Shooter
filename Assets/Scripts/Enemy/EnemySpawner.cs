using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public Text waveText;
    public int maxEnemies = 40;
    public float spawnRate = 2.0f;
    public float wavePauseTime = 10.0f;
    public GameObject[] enemyPrefabs;
    public GameObject[] spawns;
    private int currentWave = 0;
    private bool paused = true;
    private List<GameObject> enemies;
    private List<GameObject> qeue;
    private float currentPauseTimer = 0.0f, currentTimer = 0.0f;

    void Start() {
        Debug.Log(Screen.currentResolution);

        enemies = new List<GameObject>(maxEnemies);
        qeue = new List<GameObject>(maxEnemies);
        for (int i = 0; i < enemyPrefabs.Length; i++)
            if (enemyPrefabs[i].GetComponent<EnemyController>() == null) throw new System.NullReferenceException("Prefab needs a EnemyController!");
    }

    void calculateNextWave() {
        currentWave++;
        WaveAnimationManager.changeWave(currentWave);
        List<GameObject> available = new List<GameObject>(enemyPrefabs.Length);
        for (int i = 0; i < enemyPrefabs.Length; i++) {
            EnemyController controller = enemyPrefabs[i].GetComponent<EnemyController>();
            if (controller.enemy.introduceOn <= currentWave)
                available.Add(enemyPrefabs[i]);
        }

        for (int i = 0; i < available.Count; i++) {
            EnemyController controller = available[i].GetComponent<EnemyController>();
            int count = Random.Range(1 + (int)Mathf.Round(currentWave / 15), (int)Mathf.Round(Mathf.Clamp(controller.enemy.maxPerwave + (int) Mathf.Round(currentWave / 15), 1, Mathf.Round(maxEnemies / available.Count) + 1)));
            for (int a = 0; a < count; a++)
                qeue.Add(available[i]);
        }

    }

    void Update() {
        currentTimer += Time.deltaTime;
        UpdateList();
        if (paused == true) {
            waveText.text = "Next wave starts in " + System.Math.Round((wavePauseTime - currentPauseTimer), 1);
            waveText.color = Color.Lerp(Color.white, Color.red, currentPauseTimer / wavePauseTime);
        }
        else
            waveText.text = "";

        if (enemies.Count == 0 && qeue.Count == 0) {
            paused = true;
        }

        if (paused && currentPauseTimer < wavePauseTime) currentPauseTimer += Time.deltaTime;
        else if (paused && currentPauseTimer > wavePauseTime) {
            paused = false;
            currentPauseTimer = 0.0f;
            calculateNextWave();
        }

        if (currentTimer / spawnRate >= 1 && !paused && qeue.Count > 0) {
            currentTimer = 0.0f;
            spawnEnemies();
        }
    }

    void spawnEnemies() {
        for (int i = 0; i < spawns.Length; i++) {
            if (qeue.Count == 0) break;
            int index = Random.Range(0, qeue.Count);
            GameObject gameObject = (GameObject)Instantiate(qeue[index], spawns[i].transform.position, Quaternion.Euler(new Vector3()));
            enemies.Add(gameObject);
            gameObject.GetComponent<EnemyController>().calculateStats(currentWave);
            qeue.RemoveAt(index);
        }
    }

    void UpdateList() {
        for (int i = 0; i < enemies.Count; i++) {
            if (enemies[i] == null)
                enemies.RemoveAt(i);
        }
    }
}
