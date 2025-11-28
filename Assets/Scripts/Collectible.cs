using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    Player p;
    Color flashingColor;
    public string type;
    // Start is called before the first frame update
    void Start()
    {
        p = GameObject.Find("Player").GetComponent<Player>();
        flashingColor = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {
        flashingColor = Color.Lerp(Color.white, Color.yellow, Mathf.PingPong(Time.time, 1));
        GetComponent<SpriteRenderer>().color = flashingColor;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        /*if (collider.gameObject.tag == "Player")
        {
            GetComponent<Renderer>().enabled = false;
            p.setCarryableObject();
            Debug.Log("Item picked up.");
            //gameObject.transform.position = new Vector3(offScreenX, offScreenY, 0);
        }*/
    }
}
