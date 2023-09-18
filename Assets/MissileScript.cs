using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class MissileScript : MonoBehaviour
    {
        public MissileAndExplosionScript MissileAndExplosionScript;
        // Start is called before the first frame update
        void Start()
        {
            MissileAndExplosionScript = GetComponentInParent<MissileAndExplosionScript>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            MissileAndExplosionScript.OnMissileHit(collision);
        }
    }
}