using UnityEngine;
using System.Collections;

public class EnemyAudioManager : MonoBehaviour {

    //Specific locations to play sound(NEEDED??)
    public AudioSource footstepsSource;

    [Space(5)]
    public RandomizedAudio footstepsAudio;

    [Space(10)]
    public AudioSource commonSource;

    [Space(5)]
    public RandomizedAudio ambientAudio;

    [Space(2)]
    public RandomizedAudio deathAudio;

    [Space(2)]
    public RandomizedAudio attackAudio;

    private float currentAmbientTimer = 0.0f;
    private float ambientTime = 0.0f;

    void Start() {
        calculateAmbientSound();
    }

    public void stopAudio() {
        footstepsSource.Stop();
        commonSource.Stop();
    }

    void Update() {
        currentAmbientTimer+= Time.deltaTime;
        if(currentAmbientTimer >= ambientTime) {
            currentAmbientTimer = 0.0f;
            ambientAudio.play(commonSource);
            calculateAmbientSound();
        }

    }
    
    void calculateAmbientSound() {
        ambientTime = Random.Range(5.0f, 20.0f);
    }

    public void playDeathAudio() {
        deathAudio.play(commonSource);
    }


    public void playAttackAudio() {
        attackAudio.play(commonSource);
    }

    public void playFootstepAudio() {
        footstepsAudio.play(footstepsSource);
    }
}
