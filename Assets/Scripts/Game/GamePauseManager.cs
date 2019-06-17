using UnityEngine;
using System.Collections;

public class GamePauseManager : MonoBehaviour {

    private Vector3 position;
    private Vector3 out_Position;
    private bool paused = false;

	void Start () {
        AudioListener.pause = false;
        Cursor.lockState = CursorLockMode.Confined;
        out_Position = transform.position;
        position = new Vector3(out_Position.x, Screen.height / 2, 0);
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!paused)
                pause();
            else resume();
        }

        if (paused) transform.position = Vector3.Lerp(transform.position, position, 0.2f);
        else transform.position = Vector3.Lerp(transform.position, out_Position, 0.2f);
	}

    public void pause() {
        AudioListener.pause = true;
        paused = true;
        Time.timeScale = 0.0f;
    }

    public void resume() {
        AudioListener.pause = false;
        paused = false;
        Time.timeScale = 1.0f;
    }

    public void exit() {
        Application.Quit();
    }
}
