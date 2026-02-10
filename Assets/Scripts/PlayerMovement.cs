using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody rigidbody;

    public float MovementSpeed = 10f;
    public float RotationSpeed = 10f;

    private void Start() {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        Move();
        Rotate();
    }

    private void Move() {
        float x = CrossPlatformInputManager.GetAxis("HorizontalMovement");
        float y = CrossPlatformInputManager.GetAxis("VerticalMovement");

        //rigidbody.velocity = new Vector3(x, 0, y) * MovementSpeed;

        //var newpos = rigidbody.position + transform.forward * y;
        //rigidbody.MovePosition(newpos);

        //transform.Translate(x, y, 0, Space.World);
        if (x != 0 || y != 0) {
            Vector3 movement = new Vector3(x, 0, y);
            rigidbody.AddRelativeForce(movement * MovementSpeed, ForceMode.Impulse);

            Vector3 velocity = rigidbody.velocity;

            ////when the player turns, dont drag around
            //if (movement.x > 0 && velocity.x < 0 || movement.x < 0 && velocity.x > 0) {
            //    velocity.x *= -1;
            //}
            //if (movement.y > 0 && velocity.y < 0 || movement.y < 0 && velocity.y > 0) {
            //    velocity.y *= -1;
            //}

            ////regulate the max movement speed
            //Mathf.Clamp(velocity.y, -MovementSpeed, MovementSpeed);
            //Mathf.Clamp(velocity.x, -MovementSpeed, MovementSpeed);

            rigidbody.velocity = velocity;
        }
        else {
            rigidbody.velocity = Vector3.zero;
        }
    }

    private void Rotate() {

        float y = CrossPlatformInputManager.GetAxis("HorizontalRotation");
        float x = CrossPlatformInputManager.GetAxis("VerticalRotation");

        y = -y;
        x = -x;


        //Vector3 euler = transform.localEulerAngles;
        Vector3 euler = transform.eulerAngles;
        euler.x = (euler.x + x) % 360;
        euler.y = (euler.y - y) % 360;
        //transform.localEulerAngles = euler;
        transform.eulerAngles = euler;
        rigidbody.rotation = transform.rotation;
    }
}
