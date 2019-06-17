using UnityEngine;
using System.Collections;

public class HandsFollower : MonoBehaviour {

    private Transform hand;
	
    void Start() {
            hand = GameObject.FindWithTag("Hand").transform;
    }

	void LateUpdate () {
        transform.position = hand.position;
	}
}
