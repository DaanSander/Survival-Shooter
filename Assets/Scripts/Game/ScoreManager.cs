using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    public Text scoreText;
    public static int score = 0;

    void Start() {
        score = 0;
    }

	void Update () {
        scoreText.text = "Score: " + score;
	}
}
