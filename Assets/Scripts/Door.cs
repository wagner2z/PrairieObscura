using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float xPlacement;
    public float yPlacement;
    public int pointsNeeded;
    public bool indoors;
    Color flashingColor;
    bool doorUnlocked;

    // Start is called before the first frame update
    void Start()
    {
        doorUnlocked = false;
        flashingColor = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        if (doorUnlocked)
        {
            flashingColor = Color.Lerp(Color.white, Color.green, Mathf.PingPong(Time.time, 1));
        }
        else
        {
            flashingColor = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, 1));
        }
        GetComponent<SpriteRenderer>().color = flashingColor;
    }
}
