using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
[RequireComponent(typeof(Animator))]
public class WaveAnimationManager : MonoBehaviour {

    private static Text text;
    private static Animator animator;
    private static int changeHash;

    public void Start() {
        text = GetComponent<Text>();
        animator = GetComponent<Animator>();
        changeHash = Animator.StringToHash("change");
    }

    public static void changeWave(int wave) {
        text.text = "Wave " + wave;
        animator.SetTrigger(changeHash);
    }
}
