using UnityEngine;
using random = UnityEngine.Random;
using System.Collections;

[CreateAssetMenu(menuName = "Audio/RandomizedAudio")]
public class RandomizedAudio : ScriptableObject {

    public AudioClip[] audioClips;

    [MinMaxRange(0.1f, 2.0f)]
    public RangedFloat volume;
    public RangedFloat pitch;

    public void play(AudioSource source) {
        source.clip = audioClips[random.Range(0, audioClips.Length)];
        source.pitch = random.Range(pitch.minValue, pitch.maxValue);
        source.volume = random.Range(volume.minValue, volume.maxValue);
        source.Play();
    }
}
