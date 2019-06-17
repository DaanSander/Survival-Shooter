using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(Animator))]
public class PlayerStateManager : MonoBehaviour {

    private Animator animator;
    private PlayerManager playerManager;
    private int sprintHash, forwardsHash, backwardsHash, leftHash, rightHash;

	void Start () {
        animator = GetComponent<Animator>();
        playerManager = GetComponent<PlayerManager>();
        sprintHash = Animator.StringToHash("sprinting");
        forwardsHash = Animator.StringToHash("forwards");
        backwardsHash = Animator.StringToHash("backwards");
        leftHash = Animator.StringToHash("left");
        rightHash = Animator.StringToHash("right");

    }

    void Update () {
        float width = Input.GetAxisRaw("Horizontal");
        float height = Input.GetAxisRaw("Vertical");

        Debug.Log(width + height);
       
        animator.SetBool(sprintHash, Input.GetKey(KeyCode.LeftShift));
        animator.SetBool(forwardsHash, height > 0);
        animator.SetBool(backwardsHash, height < 0);
        animator.SetBool(leftHash, width < 0);
        animator.SetBool(rightHash, width > 0);
    }
}
