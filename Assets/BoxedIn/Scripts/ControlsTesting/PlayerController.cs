using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace BoxedIn.testing
{
    [RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Transform cam;
        private Rigidbody rb;
        public GameObject deathPanel;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            Time.timeScale = 1f;
        }
        private void Update()
        {
            var h = Input.GetAxis("Horizontal");
            var v = Input.GetAxis("Vertical");

            var direction = new Vector3(v, 0f, h);

            if(direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, -direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                var moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
               
                rb.angularVelocity = moveDir * (speed * Time.deltaTime);
            }
        }

        /// <summary>
        /// When called will pause the game and load the death screen
        /// </summary>
        public void PlayerCapture()
        {
            // handle death logic here
            deathPanel.SetActive(true);
            Debug.Log("CAPTURED");
            Time.timeScale = 0;
        }
    }
}