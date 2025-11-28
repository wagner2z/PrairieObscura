using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Studio_Logo_Cutscene : MonoBehaviour
{
    ControlAssignment control = new ControlAssignment();
    RawImage darkScreen;
    bool titleFinished;
    public AudioSource music;
    public AudioSource sfx;
    float tempWait;
    bool startUp;
    const float waitTime = 1f;
    //public AudioClip titleMusic;
    //public AudioClip startSound;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 0f);
        darkScreen = GameObject.Find("Cut Scene").GetComponent<RawImage>();
        darkScreen.enabled = true;
        titleFinished = false;
        startUp = false;
        //music.loop = true;
        //music.clip = titleMusic;
        //sfx.clip = startSound;
        tempWait = 0f;
        StartCoroutine(studioLogoAction());
    }

    // Update is called once per frame
    void Update()
    {
        if (titleFinished && !startUp)
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
            if(tempWait > 0)
            {
                tempWait -= Time.deltaTime;
            }
            else
            {
                SceneManager.LoadScene("ControlsScene");
            }
        }

       
        /*if (gameStart.isSkipped())
        {
            if (coroutineRun)
            {
                StopCoroutine(logoAction());
                coroutineRun = false;
            }
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }*/
    }

    IEnumerator studioLogoAction()
    {
        //coroutineRun = true;
        music.Play();
        yield return new WaitForSeconds(2);
        for (float i = 0f; i <= 1f;)
        {
            i = i + 0.1f;
            gameObject.GetComponent<RawImage>().color = new Color(1f, 1f, 1f, i);
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(2);
        for (float i = 1f; i > 0;)
        {
            i = i - 0.1f;
            gameObject.GetComponent<RawImage>().color = new Color(1f, 1f, 1f, i);
            yield return new WaitForSeconds(0.1f);
        }
        for (float i = 1f; i > 0;)
        {
            i = i - 0.1f;
            darkScreen.color = new Color(0f, 0f, 0f, i);
            yield return new WaitForSeconds(0.1f);
        }
        titleFinished = true;
        yield return null;
    }

}

