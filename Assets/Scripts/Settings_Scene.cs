using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Settings_Scene : MonoBehaviour
{
    GameObject options;
    public AudioSource sfx;
    float tempWait;
    bool startUp;
    const float waitTime = 1f;
    int optionPosition;
    const int maxOptions = 2;
    // Start is called before the first frame update
    void Start()
    {
        startUp = false;
        options = GameObject.Find("SettingOptions");
        tempWait = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform t in options.transform)
        {
            t.GetComponent<TextMeshProUGUI>().color = Color.white;
        }
        options.transform.GetChild(optionPosition).GetComponent<TextMeshProUGUI>().color = Color.yellow;
        if (!ControlAssignment.getMoveByWorldAxis())
        {
            options.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.yellow;
            options.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.white;
        }
        else
        {
            options.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white;
            options.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.yellow;
        }
        if (Input.GetKeyDown(ControlAssignment.switchOptionUp()))
        {
            if (optionPosition == 0)
            {
                optionPosition = maxOptions - 1;
            }
            else
            {
                optionPosition--;
            }
        }
        if (Input.GetKeyDown(ControlAssignment.switchOptionDown()))
        {
            if (optionPosition == maxOptions - 1)
            {
                optionPosition = 0;
            }
            else
            {
                optionPosition++;
            }
        }
        if (Input.GetKeyDown(ControlAssignment.switchOptionLeft()))
        {
            if(optionPosition == 0)
            {
                ControlAssignment.setMoveByWorldAxis(!ControlAssignment.getMoveByWorldAxis());
            }
        }
        if (Input.GetKeyDown(ControlAssignment.switchOptionRight()))
        {
            if (optionPosition == 0)
            {
                ControlAssignment.setMoveByWorldAxis(!ControlAssignment.getMoveByWorldAxis());
            }
        }
        if (!startUp)
        {
            if (Input.GetKey(ControlAssignment.start()))
            {
                sfx.Play();
                startUp = true;
                tempWait = waitTime;
                
            }
        }

        if (startUp)
        {
            if (tempWait > 0)
            {
                tempWait -= Time.deltaTime;
            }
            else
            {
                SceneManager.LoadScene("TitleScene");
            }
        }
    }
}

