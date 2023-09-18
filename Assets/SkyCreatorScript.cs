using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyCreatorScript : MonoBehaviour
{

    private float timer = 0;
    private float cloudSpawnDelay = 10f;

    public GameObject SkySprite;

    void Start()
    {
        CreateSky();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer <= cloudSpawnDelay)
        {
            timer += Time.deltaTime;
        }
        else
        {
            CreateSky();
            timer = 0;
        }
    }

    void CreateSky()
    {
        Instantiate(SkySprite, transform.position, transform.rotation);
    }
}
