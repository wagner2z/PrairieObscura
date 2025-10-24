using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using static Player;

public class CameraMove : MonoBehaviour
{
    GameObject player;
    //UIHandler ui;
    //const float xStart = -10.4f;
    //const float yStart = 2.55f;
    //const float diffX = 9.05f;
    const float renderDistance = 15f;
    //float negXBound;
    //float posXBound;
    //RawImage darkScreen;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        //ui = GameObject.Find("SceneHandler").GetComponent<UIHandler>();
        //darkScreen = GameObject.Find("Cut Scene").GetComponent<RawImage>();
        //darkScreen.enabled = true;
        //setStartingPosition();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
        /*if (!ui.isPaused())
        {
            if (p.getX() - diffX >= negXBound && p.getX() + diffX <= posXBound)
            {
                transform.position = new Vector3(p.getX(), p.getY() + yStart, -10f);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, p.getY() + yStart, -10f);
            }
        }*/
    }

    public void setStartingPosition()
    {
        //p = GameObject.Find("Player").GetComponent<Player>();
        //transform.position = new Vector3(xStart, yStart, -10f);
        //negXBound = GameObject.Find("NegativeXBound").transform.position.x + 0.25f;
       //posXBound = GameObject.Find("PositiveXBound").transform.position.x - 0.25f;
        //Debug.Log("Current scene bounds are: " + negXBound + ", " + posXBound);
    }

    public bool isWithinDistance(Vector3 pos)
    {
        if (Vector3.Distance(transform.position, pos) <= renderDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

