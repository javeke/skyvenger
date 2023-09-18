using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class BeamShooterScript : MonoBehaviour
    {

        /// <summary>
        /// The number of seconds to wait between each beam shot
        /// </summary>
        public const float beamThrottle = 0.5f;

        private float timer;
        private bool CanShootBeam;

        public GameObject Beam;
        public Quaternion BeamRotation;

        public GameStateManager GameStateManager;
        public string GameManagerTag = "GameManager";

        // Start is called before the first frame update
        void Start()
        {
            GameStateManager = GameObject.FindGameObjectWithTag(GameManagerTag).GetComponent<GameStateManager>();

            BeamRotation = Quaternion.FromToRotation(new Vector3(0, 1, 0), new Vector3(1, 0, 0));
            timer = beamThrottle;
            CanShootBeam = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                CanShootBeam = false;
            }

            if(timer <= 0)
            {
                CanShootBeam = true;
            }

            if (Input.GetKeyDown(KeyCode.Space) && GameStateManager.State == GameStateManager.GameState.Running && CanShootBeam)
            {
                CreateBeam(transform.position, BeamRotation);
                timer = beamThrottle;
            }
        }

        void CreateBeam(Vector3 position, Quaternion rotation)
        {
            Instantiate(Beam, position, rotation);
        }
    }
}