using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;
using UnityEngine.SceneManagement;
using static ControlAssignment;
using TMPro;

public class DeathHandler : MonoBehaviour
{
    Player p;
    TextMeshProUGUI messageUI;
    bool isAlreadyDead;

    // Start is called before the first frame update
    void Start()
    {
        isAlreadyDead = false;
        p = GameObject.Find("Player").GetComponent<Player>();
        messageUI = GameObject.Find("Message").GetComponent<TextMeshProUGUI>();
        messageUI.enabled = false;
        isAlreadyDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (p.getHP() <= 0 && !isAlreadyDead)
        {
            isAlreadyDead = true;
            messageUI.text = "You Are Dead\nPress Enter to Restart";
            messageUI.color = Color.red;
            messageUI.enabled = true;
        }
        if (isAlreadyDead && Input.GetKeyDown(ControlAssignment.start()))
        {
            isAlreadyDead = false;
            messageUI.enabled = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

