using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct WaveLayout
{
    GameObject[] enemies;

    public GameObject[] getEnemyWaveList(int i)
    {
        switch (i)
        {
            case 1:
                enemies = new GameObject[20];
                for(int e = 0; e < 20; e++)
                {
                    enemies[e] = GameObject.Find("Horde (1)").transform.GetChild(e).gameObject;
                }
                break;
            case 2:
                enemies = new GameObject[30];
                for (int e = 0; e < 30; e++)
                {
                    enemies[e] = GameObject.Find("Horde (1)").transform.GetChild(e).gameObject;
                }
                break;
            case 3:
                enemies = new GameObject[40];
                for (int e = 0; e < 40; e++)
                {
                    enemies[e] = GameObject.Find("Horde (1)").transform.GetChild(e).gameObject;
                }
                break;
            case 4:
                enemies = new GameObject[50];
                for (int e = 0; e < 50; e++)
                {
                    enemies[e] = GameObject.Find("Horde (1)").transform.GetChild(e).gameObject;
                }
                break;
            default:
                enemies = new GameObject[60];
                for (int e = 0; e < 60; e++)
                {
                    enemies[e] = GameObject.Find("Horde (1)").transform.GetChild(e).gameObject;
                }
                break;
        }
        return enemies;
    }
}
