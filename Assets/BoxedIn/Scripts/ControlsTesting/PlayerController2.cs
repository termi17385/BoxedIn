using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

using UnityEngine;
using UnityEngine.EventSystems;

using Vector3 = UnityEngine.Vector3;

public class PlayerController2 : MonoBehaviour
{
      [SerializeField] private float speed;
      private float vSpeed = 0;

      private float gravity = 9.81f;
      private bool isGrounded = false;
      private CharacterController cController;
      
      private void Start() => cController = GetComponent<CharacterController>();
      private void Update()
      {
            BasicMovement();
      }

      private void BasicMovement()
      {
            var h = Input.GetAxisRaw("Horizontal") * speed;
            var v = Input.GetAxisRaw("Vertical") * speed;

            var  pTransform = transform;
            var moveForward = pTransform.forward * v;
            var sideWays = pTransform.right * h;

            if(isGrounded) vSpeed = 0;
            else vSpeed -= gravity * Time.deltaTime;
            var moveDirection = (moveForward + sideWays).normalized;

            moveDirection.y = vSpeed;
            cController.Move(moveDirection * Time.deltaTime);
      }

      private void OnCollisionEnter(Collision other)
      {
            if(other.collider.CompareTag("Ground"))
                  isGrounded = true;
      }

      private void OnCollisionExit(Collision other)
      {
            if(other.collider.CompareTag("Ground"))
                  isGrounded = false;
      }
}