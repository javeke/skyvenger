using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class BeamScript : MonoBehaviour
    {

        //private static readonly int BeamLayer = 3;
        private static readonly int PlaneLayer = 6;
        //private static readonly int MissileLayer = 7;

        public Rigidbody2D BeamRigidBody;
        public Vector2 BottomCorner = new Vector2(5, -3);

        public float speed = 1.0f;
        // Start is called before the first frame update
        void Start()
        {
            BeamRigidBody.velocity = Vector2.right * speed;
        }

        // Update is called once per frame
        void Update()
        {
            if (transform.position.x >= BottomCorner.x)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer != PlaneLayer)
            {
                Destroy(gameObject, 0.5f);
            }
        }
    }
}