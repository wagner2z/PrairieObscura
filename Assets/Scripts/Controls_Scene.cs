using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controls_Scene : MonoBehaviour
{
    ControlAssignment control = new ControlAssignment();
    public AudioSource sfx;
    float tempWait;
    bool startUp;
    const float waitTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        startUp = false;
        tempWait = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        if (!startUp)
        {
            if (Input.GetKey(control.start()))
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
                SceneManager.LoadScene("FarmyardScene");
            }
        }
    }
}

