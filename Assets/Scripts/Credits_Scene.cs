using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Credits_Scene : MonoBehaviour
{
    ControlAssignment control = new ControlAssignment();
    public AudioSource music;
    public AudioSource sfx;
    float tempWait;
    bool startUp;
    bool titleFinished;
    const float waitTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        startUp = false;
        titleFinished = false;
        tempWait = 0f;
        StartCoroutine(creditAction());
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
                SceneManager.LoadScene("TitleScene");
            }
        }

        if (titleFinished)
        {
            SceneManager.LoadScene("TitleScene");
        }
    }

    IEnumerator creditAction()
    {
        //coroutineRun = true;
        music.Play();
        yield return new WaitForSeconds(2);
        gameObject.GetComponent<TextMeshProUGUI>().text = "Creative Director: Austin David";
        for (float i = 0f; i <= 1f;)
        {
            i = i + 0.1f;
            gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, i);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2);
        for (float i = 1f; i > 0;)
        {
            i = i - 0.1f;
            gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, i);
            yield return new WaitForSeconds(0.1f);
        }
        gameObject.GetComponent<TextMeshProUGUI>().text = "Technical Director: Zach Wagner";
        for (float i = 0f; i <= 1f;)
        {
            i = i + 0.1f;
            gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, i);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2);
        for (float i = 1f; i > 0;)
        {
            i = i - 0.1f;
            gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, i);
            yield return new WaitForSeconds(0.1f);
        }
        gameObject.GetComponent<TextMeshProUGUI>().text = "Principal Photographer: Cylen David";
        for (float i = 0f; i <= 1f;)
        {
            i = i + 0.1f;
            gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, i);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2);
        for (float i = 1f; i > 0;)
        {
            i = i - 0.1f;
            gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, i);
            yield return new WaitForSeconds(0.1f);
        }
        gameObject.GetComponent<TextMeshProUGUI>().text = "Playtesters: Cylen David, Brooke Sali";
        for (float i = 0f; i <= 1f;)
        {
            i = i + 0.1f;
            gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, i);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2);
        for (float i = 1f; i > 0;)
        {
            i = i - 0.1f;
            gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, i);
            yield return new WaitForSeconds(0.1f);
        }
        gameObject.GetComponent<TextMeshProUGUI>().text = "Character Model: Austin David";
        for (float i = 0f; i <= 1f;)
        {
            i = i + 0.1f;
            gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, i);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2);
        for (float i = 1f; i > 0;)
        {
            i = i - 0.1f;
            gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, i);
            yield return new WaitForSeconds(0.1f);
        }
        gameObject.GetComponent<TextMeshProUGUI>().text = "Sound Assets: Pizza Doggy, Nebula Audio";
        for (float i = 0f; i <= 1f;)
        {
            i = i + 0.1f;
            gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, i);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2);
        for (float i = 1f; i > 0;)
        {
            i = i - 0.1f;
            gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, i);
            yield return new WaitForSeconds(0.1f);
        }
        gameObject.GetComponent<TextMeshProUGUI>().text = "Sprite Assets: Jarp.Pix";
        for (float i = 0f; i <= 1f;)
        {
            i = i + 0.1f;
            gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, i);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2);
        for (float i = 1f; i > 0;)
        {
            i = i - 0.1f;
            gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, i);
            yield return new WaitForSeconds(0.1f);
        }
        gameObject.GetComponent<TextMeshProUGUI>().text = "Special Thanks: Josh, Sam, Lyle, Lisa, Ryder";
        for (float i = 0f; i <= 1f;)
        {
            i = i + 0.1f;
            gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, i);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2);
        for (float i = 1f; i > 0;)
        {
            i = i - 0.1f;
            gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, i);
            yield return new WaitForSeconds(0.1f);
        }
        titleFinished = true;
        yield return null;
    }
}

