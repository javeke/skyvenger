using Manager;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Enemy
{
    public class SpawnMissile : MonoBehaviour
    {
        private float timer;


        /// <summary>
        /// The accuracy level of the missile launcher based on the level
        /// </summary>
        public float accuracy;

        /// <summary>
        /// The number of seconds to wait between each missile launch
        /// </summary>
        public float spawnDelay;


        public GameObject MissileAndExplosion;

        public GameObject Target;

        public GameStateManager GameStateManager;
        public string GameManagerTag = "GameManager";

        // Start is called before the first frame update
        void Start()
        {
            timer = 0;


            GameStateManager = GameObject.FindGameObjectWithTag(GameManagerTag).GetComponent<GameStateManager>();
            accuracy = AccuracyLevelFunction(GameStateManager.GameLevel);
            spawnDelay = SpawnDelayLevelFunction(GameStateManager.GameLevel, 0.05f);

            if (GameStateManager.State == GameStateManager.GameState.Running)
            {
                CreateMissile(accuracy);
            }

        }

        // Update is called once per frame
        void Update()
        {
            accuracy = AccuracyLevelFunction(GameStateManager.GameLevel);
            spawnDelay = SpawnDelayLevelFunction(GameStateManager.GameLevel, 0.05f);

            if (GameStateManager.State == GameStateManager.GameState.Running)
            {
                if (timer < spawnDelay)
                {
                    timer += Time.deltaTime;
                }
                else
                {
                    CreateMissile(accuracy);
                    timer = 0;
                }
            }
        }

        /// <summary>
        /// Creates a wallet instance in a random location
        /// </summary>
        void CreateMissile(float accuracy)
        {
            CreateWallet(new Vector3(transform.position.x, Random.Range(Target.transform.position.y - accuracy, Target.transform.position.y + accuracy), transform.position.z));
        }

        void CreateWallet(Vector3 position)
        {
            Instantiate(MissileAndExplosion, position, transform.rotation);
        }

        /// <summary>
        /// The level is used to define how well the missile should be aimed at the plane
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private float AccuracyLevelFunction(int level)
        {
            if (level <= 0)
            {
                return 1.0f;
            }

            return (1.0f / level) + 0.01f;
        }

        private float SpawnDelayLevelFunction(int level, float minOffset)
        {
            if (level <= 0)
            {
                return minOffset;
            }

            return (1.0f / level) + minOffset;
        }
    }
}