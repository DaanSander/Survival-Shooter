using UnityEngine;
using System.Collections;

public class PlayerMotor : MonoBehaviour {
    
    public float sprintSpeed = 1.2f;
    public LayerMask floorMask;
    public float speed = 1.0f;
    private Rigidbody rigidbody;

    void Start() {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        //Debug.Log(Input.GetAxis("Vertical") * speed * sprintSpeed);

        float extraSpeed = (Input.GetKey(KeyCode.LeftShift)) ? sprintSpeed : 1.0f;
        rigidbody.velocity = new Vector3(Input.GetAxis("Horizontal") * speed * extraSpeed, rigidbody.velocity.y, Input.GetAxis("Vertical") * speed * extraSpeed);


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, 100, floorMask)) {
            Vector3 relative = hit.point - transform.position;

            rigidbody.MoveRotation(Quaternion.LookRotation(new Vector3(relative.x, 0, relative.z)));
        }
    }
}
