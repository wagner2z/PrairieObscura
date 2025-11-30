using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float xPlacement;
    public float yPlacement;
    public int pointsNeeded;
    PointCounter point;
    public bool indoors;
    Color flashingColor;
    bool doorUnlocked;

    // Start is called before the first frame update
    void Start()
    {
        point = GameObject.Find("Point Counter").GetComponent<PointCounter>();
        if (indoors)
        {
            doorUnlocked = false;
        }
        else
        {
            doorUnlocked = true;
        }
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

    public bool isDoorUnlocked()
    {
        return doorUnlocked;
    }

    public void unlockDoor()
    {
        doorUnlocked = true;
        point.decreasePoints(pointsNeeded);
    }

}
