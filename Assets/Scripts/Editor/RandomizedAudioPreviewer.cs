using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(RandomizedAudio), true)]
public class RandomizedAudioPreviewer : Editor {

    [SerializeField]
    private AudioSource source;

    public void OnEnable() {
        source = EditorUtility.CreateGameObjectWithHideFlags("RandomizedAudio previewer", HideFlags.HideAndDontSave, typeof(AudioSource)).GetComponent<AudioSource>();
        if (AudioListener.pause) AudioListener.pause = false;
    }

    public void OnDisable() {
        DestroyImmediate(source);
    }
    
    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        
        EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);

        if(GUILayout.Button("Preview")) {
            ((RandomizedAudio)target).play(source);
        }
    }
}
