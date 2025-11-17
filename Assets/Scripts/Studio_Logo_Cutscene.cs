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
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 0f);
        darkScreen = GameObject.Find("Cut Scene").GetComponent<RawImage>();
        darkScreen.enabled = true;
        titleFinished = false;
        StartCoroutine(studioLogoAction());
    }

    // Update is called once per frame
    void Update()
    {
        if (titleFinished)
        {
            if (Input.GetKey(control.start()))
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

