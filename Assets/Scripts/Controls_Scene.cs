using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controls_Scene : MonoBehaviour
{
    ControlAssignment control = new ControlAssignment();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(control.start()))
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}

