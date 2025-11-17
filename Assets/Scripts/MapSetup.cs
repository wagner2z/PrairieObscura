using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WaveLayout;
using TMPro;
//using Random = System.Random;

public class MapSetup : MonoBehaviour
{
    WaveLayout w = new WaveLayout();
    Player p;
    float xBoundLeft;
    float xBoundRight;
    float yBoundUp;
    float yBoundDown;
    float xPosition;
    float yPosition;
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
        p = GameObject.Find("Player").GetComponent<Player>();
        waveCount = 1;
        waveMsg = GameObject.Find("New Wave Message").GetComponent<TextMeshProUGUI>();
        waveMsg.text = "";
        enemies = w.getEnemyWaveList(waveCount);
        //enemies = GameObject.Find("Horde1");
        xBoundLeft = -15f;
        xBoundRight = 20f;
        yBoundDown = -20f;
        yBoundUp = 15f;
        xPosition = 0;
        yPosition = 0;
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
            int tempR = Random.Range(0, 2);
            if(tempR == 0)
            {
                xPosition = Random.Range(xBoundLeft, p.transform.position.x - 1f);
            }
            else
            {
                xPosition = Random.Range(p.transform.position.x + 1f, xBoundRight);
            }
            tempR = Random.Range(0, 2);
            if (tempR == 0)
            {
                yPosition = Random.Range(yBoundDown, p.transform.position.y - 1f);
            }
            else
            {
                yPosition = Random.Range(p.transform.position.y + 1f, yBoundUp);
            }
            e.transform.position = new Vector3(xPosition, yPosition, 0);
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
