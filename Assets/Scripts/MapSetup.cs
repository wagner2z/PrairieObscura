using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WaveLayout;
using TMPro;
//using Random = System.Random;

public class MapSetup : MonoBehaviour
{
    WaveLayout w = new WaveLayout();
    float xBoundLeft;
    float xBoundRight;
    float yBoundUp;
    float yBoundDown;
    GameObject[] enemies;
    int enemyCount;
    int enemyTotal;
    int waveCount;
    int waveStartCountdown = 5;
    bool settingUp;
    TextMeshProUGUI waveMsg;

    // Start is called before the first frame update
    void Start()
    {
        waveCount = 1;
        waveMsg = GameObject.Find("New Wave Message").GetComponent<TextMeshProUGUI>();
        waveMsg.text = "";
        enemies = w.getEnemyWaveList(waveCount);
        //enemies = GameObject.Find("Horde1");
        xBoundLeft = -15f;
        xBoundRight = 20f;
        yBoundDown = -20f;
        yBoundUp = 15f;
        /*xBoundLeft = -45.5f;
        xBoundRight = 44f;
        yBoundDown = -50f;
        yBoundUp = 38f;*/
        enemyTotal = 0;
        enemyCount = -1;
        settingUp = false;
        placeEnemies(enemies);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCount == 0 && !settingUp)
        {
            StartCoroutine(roundSetUp());
            settingUp = true;
        }
    }

    void placeEnemies(GameObject[] enemies)
    {
        enemyTotal = 0;
        foreach (GameObject e in enemies)
        {
            e.transform.position = new Vector3(Random.Range(xBoundLeft, xBoundRight), Random.Range(yBoundDown, yBoundUp), 0);
            e.GetComponent<Enemy>().rigidBody.constraints = RigidbodyConstraints2D.None;
            e.GetComponent<Enemy>().setCurrentHP(e.GetComponent<Enemy>().getMaxHP());
            e.GetComponent<Enemy>().unmarkDead();
            enemyTotal++;
        }
        enemyCount = enemyTotal;
    }

    public IEnumerator roundSetUp()
    {
        waveMsg.text = "Wave Complete!";
        yield return new WaitForSeconds(2f);
        waveCount++;
        enemies = w.getEnemyWaveList(waveCount);
        int tempCount = waveStartCountdown;
        while (tempCount > 0)
        {
            waveMsg.text = "New Wave in " + tempCount;
            yield return new WaitForSeconds(1f);
            tempCount--;
        }
        waveMsg.text = "";
        placeEnemies(enemies);
        settingUp = false;
        yield return null;
    }

    public int getWaveCount()
    {
        return waveCount;
    }

    public int getTotalEnemies()
    {
        return enemyTotal;
    }

    public int getCurrentEnemies()
    {
        return enemyCount;
    }

    public void removeEnemy()
    {
        enemyCount--;
    }
}
