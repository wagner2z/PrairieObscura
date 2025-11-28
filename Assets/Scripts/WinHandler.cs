using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;
using UnityEngine.SceneManagement;
using static ControlAssignment;
using TMPro;

public class WinHandler : MonoBehaviour
{
    CarFix c;
    TextMeshProUGUI messageUI;
    ControlAssignment control = new ControlAssignment();
    bool hasAlreadyWon;

    // Start is called before the first frame update
    void Start()
    {
        hasAlreadyWon = false;
        c = GameObject.Find("car").GetComponent<CarFix>();
        messageUI = GameObject.Find("Message").GetComponent<TextMeshProUGUI>();
        messageUI.enabled = false;
        //isAlreadyDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (c.checkWon() && !hasAlreadyWon)
        {
            hasAlreadyWon = true;
            messageUI.text = "Congrats, You Escaped!\nPress Enter to Restart";
            messageUI.color = Color.white;
            messageUI.enabled = true;
        }
        if (hasAlreadyWon && Input.GetKeyDown(control.start()))
        {
            hasAlreadyWon = false;
            messageUI.enabled = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public bool hasWon()
    {
        return hasAlreadyWon;
    }
}
