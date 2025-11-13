using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveDisplay : MonoBehaviour
{
    MapSetup map;
    TextMeshProUGUI waveDisplay;
    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.Find("SceneHandler").GetComponent<MapSetup>();
        waveDisplay = gameObject.GetComponent<TextMeshProUGUI>();
        waveDisplay.text = "Wave X (xx/xx)";
    }

    // Update is called once per frame
    void Update()
    {
        waveDisplay.text = "Wave " + map.getWaveCount() + " (" + 
            map.getCurrentEnemies() + "/" + map.getTotalEnemies() + ")";
    }
}
