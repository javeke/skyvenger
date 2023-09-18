using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sky
{
    public class SkyScript : MonoBehaviour
    {
        public Rigidbody2D SkyRigidBody;

        private readonly float speedFactor = 0.5f;
        private readonly float minSpeed = 1f;

        public GameStateManager GameStateManager;
        public string GameManagerTag = "GameManager";

        private void Start()
        {
            GameStateManager = GameObject.FindGameObjectWithTag(GameManagerTag).GetComponent<GameStateManager>();
        }

        void Update()
        {
            SkyRigidBody.velocity = Vector2.left * SpeedLevelFunction(GameStateManager.GameLevel); 

            if(transform.position.x <= -20)
            {
                Destroy(gameObject);
            }
        }

        private float SpeedLevelFunction(int level)
        {
            return level * speedFactor + minSpeed;
        }
    }
}