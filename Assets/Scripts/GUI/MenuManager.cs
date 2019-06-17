using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
using System;
using System.Collections.Generic;


public class MenuManager : MonoBehaviour {

    public Image fadeImage;
    private bool fade = false;
    private float timer = 0.0f;

    void Start() {
        Analytics.CustomEvent("systemInfo", new Dictionary<string, object> {
            {"os", SystemInfo.operatingSystem },
            {"cpu", SystemInfo.processorType + " Cores " + SystemInfo.processorCount},

            {"gpu", SystemInfo.graphicsDeviceName + " Memory " + SystemInfo.graphicsMemorySize + " Multi-Threading " + SystemInfo.graphicsMultiThreaded},
            {"ram", SystemInfo.systemMemorySize}
        });
        Analytics.CustomEvent("sessionStart", new Dictionary<string, object> {
            {"sesstionStartTime",  System.DateTime.Now.ToString()}
        });
    }

    void Update() {
        if (fade) {
            timer += Time.deltaTime;
            fadeImage.color = Color.Lerp(new Color(0.0f, 0.0f, 0.0f, 0.0f), new Color(0.0f, 0.0f, 0.0f, 1.0f), timer);
            if (fadeImage.color.a >= 1.0f) SceneManager.LoadScene("Main");
        }
    }

    public void startGame() {
        Analytics.CustomEvent("gameStart", new Dictionary<string, object> {
            {"time",  System.DateTime.Now.ToString()}
        });
        fade = true;
    }

    public void quit() {
        Application.Quit();
    }
}
