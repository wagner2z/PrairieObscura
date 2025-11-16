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
    TextMeshProUGUI deathMessageUI;
    ControlAssignment control = new ControlAssignment();
    bool isAlreadyDead;

    // Start is called before the first frame update
    void Start()
    {
        p = GameObject.Find("Player").GetComponent<Player>();
        deathMessageUI = GameObject.Find("DeathMessage").GetComponent<TextMeshProUGUI>();
        deathMessageUI.enabled = false;
        isAlreadyDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (p.getHP() <= 0 && !isAlreadyDead)
        {
            isAlreadyDead = true;
            deathMessageUI.enabled = true;
        }
        if (isAlreadyDead && Input.GetKeyDown(control.start()))
        {
            isAlreadyDead = false;
            deathMessageUI.enabled = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

