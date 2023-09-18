using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class MissileAndExplosionScript : MonoBehaviour
    {
        private const int BeamLayer = 3;
        private const int PlaneLayer = 6;
        private const int MissileLayer = 7;


        public Vector2 TopCorner = new Vector2(-5, 3);
        public float deleteDelay = 1;

        public GameStateManager GameStateManager;
        public string GameManagerTag = "GameManager";

        public Rigidbody2D MissileAndExplosionRigitBody;
        public AudioSource MissileLaunchAudioSource;

        public AudioSource MissileExplosionAudioSource;

        public GameObject ExplosionChild;
        public GameObject MissileChild;

        private readonly float speedFactor = 1f;
        private readonly float minSpeed = 2.5f;
        // Start is called before the first frame update
        void Start()
        {
            GameStateManager = GameObject.FindGameObjectWithTag(GameManagerTag).GetComponent<GameStateManager>();

            MissileLaunchAudioSource.volume = 0.05f;
            MissileExplosionAudioSource.volume = 0.1f;

            MissileLaunchAudioSource.Play();
        }

        // Update is called once per frame
        void Update()
        {

            MissileAndExplosionRigitBody.velocity = Vector2.left * SpeedLevelFunction(GameStateManager.GameLevel);


            if (transform.position.x <= TopCorner.x)
            {
                Destroy(gameObject);
            }
        }

        public void OnMissileHit(Collider2D collision)
        {
            ExplosionChild.SetActive(true);
            MissileChild.SetActive(false);

            Explode();

            int points = 0;
            int hitEffect = 0;

            switch (collision.gameObject.layer)
            {
                case PlaneLayer:
                    points = -1;
                    hitEffect = -1;
                    break;
                case BeamLayer:
                    points = 1;
                    hitEffect = 0;
                    break;
                case MissileLayer:
                default:
                    break;
            }

            GameStateManager.UpdateScore(points, hitEffect);

            // Destroy prefab after 1s after hit
            Destroy(gameObject, 1);
        }

        private void Explode()
        {
            MissileExplosionAudioSource.Play();
        }

        private float SpeedLevelFunction(int level)
        {
            return level * speedFactor + minSpeed;
        }
    }
}