using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class ControlPlane : MonoBehaviour
    {
        private readonly float speed = 3f;


        public Vector2 TopCorner = new Vector2(-5, 3);
        public Vector2 BottomCorner = new Vector2(5, -3);
        public Rigidbody2D PlaneRigidBody2d;

        // Start is called before the first frame update
        void Start()
        {
            Initialize();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Shoot Own Missile
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                PlaneRigidBody2d.velocity = Vector2.up * speed;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                PlaneRigidBody2d.velocity = Vector2.down * speed;
            }


            if (transform.position.y <= BottomCorner.y)
            {
                transform.position = new Vector3(transform.position.x, BottomCorner.y, transform.position.z);
            }


            if (transform.position.y >= TopCorner.y)
            {
                transform.position = new Vector3(transform.position.x, TopCorner.y, transform.position.z);
            }

            if (transform.position.x != 0)
            {
                transform.position = new Vector3(0, transform.position.y, transform.position.z);
            }

            transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        public void Initialize()
        {
            transform.position = new Vector3(0, 0, transform.position.z);
        }
    }
}