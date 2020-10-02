using System;
using UnityEditor;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    [SerializeField] private PlayerMovement movement;

    private void FixedUpdate() {
       float horizontal = Input.GetAxisRaw("Horizontal");
       if (horizontal > float.Epsilon || horizontal < -float.Epsilon) {
           movement.Move(horizontal);
       } else {
           movement.Stop();
       }

       if (Input.GetKeyDown(KeyCode.Space)) {
           movement.Jump();
           
       } else if (Input.GetKey(KeyCode.Space)){
           movement.KeepJumping();
           
       } else if (Input.GetKeyUp(KeyCode.Space)){
           movement.StopJumping();
       }
    }
}