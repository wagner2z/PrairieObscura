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
    public Sprite thirtyoneSprint;
    public Sprite thirtySprint;
    public Sprite twentynineSprint;
    public Sprite twentyeightSprint;
    public Sprite twentysevenSprint;
    public Sprite twentysixSprint;
    public Sprite twentyfiveSprint;
    public Sprite twentyfourSprint;
    public Sprite twentythreeSprint;
    public Sprite twentytwoSprint;
    public Sprite twentyoneSprint;
    public Sprite twentySprint;
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
        else if (sprintPercent < 1 && sprintPercent >= 0.96875)
        {
            gameObject.GetComponent<Image>().sprite = thirtyoneSprint;
        }
        else if (sprintPercent < 0.96875 && sprintPercent >= 0.9375)
        {
            gameObject.GetComponent<Image>().sprite = thirtySprint;
        }
        else if (sprintPercent < 0.9375 && sprintPercent >= 0.90625)
        {
            gameObject.GetComponent<Image>().sprite = twentynineSprint;
        }
        else if (sprintPercent < 0.90625 && sprintPercent >= 0.875)
        {
            gameObject.GetComponent<Image>().sprite = twentyeightSprint;
        }
        else if (sprintPercent < 0.875 && sprintPercent >= 0.84375)
        {
            gameObject.GetComponent<Image>().sprite = twentysevenSprint;
        }
        else if (sprintPercent < 0.84375 && sprintPercent >= 0.8125)
        {
            gameObject.GetComponent<Image>().sprite = twentysixSprint;
        }
        else if (sprintPercent < 0.8125 && sprintPercent >= 0.78125)
        {
            gameObject.GetComponent<Image>().sprite = twentyfiveSprint;
        }
        else if (sprintPercent < 0.78125 && sprintPercent >= 0.75)
        {
            gameObject.GetComponent<Image>().sprite = twentyfourSprint;
        }
        else if (sprintPercent < 0.75 && sprintPercent >= 0.71875)
        {
            gameObject.GetComponent<Image>().sprite = twentythreeSprint;
        }
        else if (sprintPercent < 0.71875 && sprintPercent >= 0.6875)
        {
            gameObject.GetComponent<Image>().sprite = twentytwoSprint;
        }
        else if (sprintPercent < 0.6875 && sprintPercent >= 0.65625)
        {
            gameObject.GetComponent<Image>().sprite = twentyoneSprint;
        }
        else if (sprintPercent < 0.65625 && sprintPercent >= 0.625)
        {
            gameObject.GetComponent<Image>().sprite = twentySprint;
        }
        else if (sprintPercent < 0.625 && sprintPercent >= 0.59375)
        {
            gameObject.GetComponent<Image>().sprite = nineteenSprint;
        }
        else if (sprintPercent < 0.59375 && sprintPercent >= 0.5625)
        {
            gameObject.GetComponent<Image>().sprite = eighteenSprint;
        }
        else if (sprintPercent < 0.5625 && sprintPercent >= 0.53125)
        {
            gameObject.GetComponent<Image>().sprite = seventeenSprint;
        }
        else if (sprintPercent < 0.53125 && sprintPercent >= 0.5)
        {
            gameObject.GetComponent<Image>().sprite = sixteenSprint;
        }
        else if (sprintPercent < 0.5 && sprintPercent >= 0.46875)
        {
            gameObject.GetComponent<Image>().sprite = fifteenSprint;
        }
        else if (sprintPercent < 0.46875 && sprintPercent >= 0.4375)
        {
            gameObject.GetComponent<Image>().sprite = fourteenSprint;
        }
        else if (sprintPercent < 0.4375 && sprintPercent >= 0.40625)
        {
            gameObject.GetComponent<Image>().sprite = thirteenSprint;
        }
        else if (sprintPercent < 0.40625 && sprintPercent >= 0.375)
        {
            gameObject.GetComponent<Image>().sprite = twelveSprint;
        }
        else if (sprintPercent < 0.375 && sprintPercent >= 0.34375)
        {
            gameObject.GetComponent<Image>().sprite = elevenSprint;
        }
        else if (sprintPercent < 0.34375 && sprintPercent >= 0.3125)
        {
            gameObject.GetComponent<Image>().sprite = tenSprint;
        }
        else if (sprintPercent < 0.3125 && sprintPercent >= 0.28125)
        {
            gameObject.GetComponent<Image>().sprite = nineSprint;
        }
        else if (sprintPercent < 0.28125 && sprintPercent >= 0.25)
        {
            gameObject.GetComponent<Image>().sprite = eightSprint;
        }
        else if (sprintPercent < 0.25 && sprintPercent >= 0.21875)
        {
            gameObject.GetComponent<Image>().sprite = sevenSprint;
        }
        else if (sprintPercent < 0.21875 && sprintPercent >= 0.1875)
        {
            gameObject.GetComponent<Image>().sprite = sixSprint;
        }
        else if (sprintPercent < 0.1875 && sprintPercent >= 0.15625)
        {
            gameObject.GetComponent<Image>().sprite = fiveSprint;
        }
        else if (sprintPercent < 0.15625 && sprintPercent >= 0.125)
        {
            gameObject.GetComponent<Image>().sprite = fourSprint;
        }
        else if (sprintPercent < 0.125 && sprintPercent >= 0.09375)
        {
            gameObject.GetComponent<Image>().sprite = threeSprint;
        }
        else if (sprintPercent < 0.09375 && sprintPercent >= 0.0625)
        {
            gameObject.GetComponent<Image>().sprite = twoSprint;
        }
        else if (sprintPercent < 0.0625 && sprintPercent >= 0.03125)
        {
            gameObject.GetComponent<Image>().sprite = oneSprint;
        }
        else if (sprintPercent < 0.03125 && sprintPercent > 0)
        {
            gameObject.GetComponent<Image>().sprite = oneSprint;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = zeroSprint;
        }
    }
}