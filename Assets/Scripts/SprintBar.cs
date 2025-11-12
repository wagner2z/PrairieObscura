using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static Player;
using TMPro;

public class SprintBar : MonoBehaviour
{
    Player p;
    public Sprite fullSprint;
    public Sprite nineteenSprint;
    public Sprite eighteenSprint;
    public Sprite seventeenSprint;
    public Sprite sixteenSprint;
    public Sprite fifteenSprint;
    public Sprite fourteenSprint;
    public Sprite thirteenSprint;
    public Sprite twelveSprint;
    public Sprite elevenSprint;
    public Sprite tenSprint;
    public Sprite nineSprint;
    public Sprite eightSprint;
    public Sprite sevenSprint;
    public Sprite sixSprint;
    public Sprite fiveSprint;
    public Sprite fourSprint;
    public Sprite threeSprint;
    public Sprite twoSprint;
    public Sprite oneSprint;
    public Sprite zeroSprint;
    // Start is called before the first frame update
    void Start()
    {
        p = GameObject.Find("Player").GetComponent<Player>();
        

        gameObject.GetComponent<Image>().sprite = fullSprint;
    }

    // Update is called once per frame
    void Update()
    {
        float currentSprint = p.getStamina();
        float maxSprint = p.getMaxStamina();

        float sprintPercent = currentSprint / maxSprint;

        if (sprintPercent >= 1) 
        {
            gameObject.GetComponent<Image>().sprite = fullSprint;
        }
        else if (sprintPercent < 1 && sprintPercent >= 0.95)
        {
            gameObject.GetComponent<Image>().sprite = nineteenSprint;
        }
        else if (sprintPercent < 0.95 && sprintPercent >= 0.90)
        {
            gameObject.GetComponent<Image>().sprite = eighteenSprint;
        }
        else if (sprintPercent < 0.90 && sprintPercent >= 0.85)
        {
            gameObject.GetComponent<Image>().sprite = seventeenSprint;
        }
        else if (sprintPercent < 0.85 && sprintPercent >= 0.80)
        {
            gameObject.GetComponent<Image>().sprite = sixteenSprint;
        }
        else if (sprintPercent < 0.80 && sprintPercent >= 0.75)
        {
            gameObject.GetComponent<Image>().sprite = fifteenSprint;
        }
        else if (sprintPercent < 0.75 && sprintPercent >= 0.70)
        {
            gameObject.GetComponent<Image>().sprite = fourteenSprint;
        }
        else if (sprintPercent < 0.70 && sprintPercent >= 0.65)
        {
            gameObject.GetComponent<Image>().sprite = thirteenSprint;
        }
        else if (sprintPercent < 0.65 && sprintPercent >= 0.60)
        {
            gameObject.GetComponent<Image>().sprite = twelveSprint;
        }
        else if (sprintPercent < 0.60 && sprintPercent >= 0.55)
        {
            gameObject.GetComponent<Image>().sprite = elevenSprint;
        }
        else if (sprintPercent < 0.55 && sprintPercent >= 0.50)
        {
            gameObject.GetComponent<Image>().sprite = tenSprint;
        }
        else if (sprintPercent < 0.50 && sprintPercent >= 0.45)
        {
            gameObject.GetComponent<Image>().sprite = nineSprint;
        }
        else if (sprintPercent < 0.45 && sprintPercent >= 0.40)
        {
            gameObject.GetComponent<Image>().sprite = eightSprint;
        }
        else if (sprintPercent < 0.40 && sprintPercent >= 0.35)
        {
            gameObject.GetComponent<Image>().sprite = sevenSprint;
        }
        else if (sprintPercent < 0.35 && sprintPercent >= 0.30)
        {
            gameObject.GetComponent<Image>().sprite = sixSprint;
        }
        else if (sprintPercent < 0.30 && sprintPercent >= 0.25)
        {
            gameObject.GetComponent<Image>().sprite = fiveSprint;
        }
        else if (sprintPercent < 0.25 && sprintPercent >= 0.20)
        {
            gameObject.GetComponent<Image>().sprite = fourSprint;
        }
        else if (sprintPercent < 0.20 && sprintPercent >= 0.15)
        {
            gameObject.GetComponent<Image>().sprite = threeSprint;
        }
        else if (sprintPercent < 0.15 && sprintPercent >= 0.10)
        {
            gameObject.GetComponent<Image>().sprite = twoSprint;
        }
        else if (sprintPercent < 0.10 && sprintPercent >= 0.05)
        {
            gameObject.GetComponent<Image>().sprite = oneSprint;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = zeroSprint;
        }
    }
}