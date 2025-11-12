using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Random = System.Random;

public class MapSetup : MonoBehaviour
{
    float xBoundLeft;
    float xBoundRight;
    float yBoundUp;
    float yBoundDown;
    GameObject enemies;

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.Find("Horde");
        xBoundLeft = -45.5f;
        xBoundRight = 44f;
        yBoundDown = -50f;
        yBoundUp = 38f;
        placeEnemies();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void placeEnemies()
    {
        foreach (Transform e in enemies.transform)
        {
            e.position = new Vector3(Random.Range(xBoundLeft, xBoundRight), Random.Range(yBoundDown, yBoundUp), 0);
        }
    }
}
