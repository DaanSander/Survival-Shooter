using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class TorchLightFlickering : MonoBehaviour {

    public Light light;
    private float timer = 0.0f;

	void Update () {
        
        timer += Time.deltaTime;
        if (timer >= 0.11f) {
            timer = 0.0f;
            light.intensity = Random.Range(3.0f, 5.0f);
        }
    }
}
