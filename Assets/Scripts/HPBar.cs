using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static Player;
using TMPro;

public class HPBar : MonoBehaviour
{
    Player p;
    public Sprite fullHP;
    public Sprite nineteenHP;
    public Sprite eighteenHP;
    public Sprite seventeenHP;
    public Sprite sixteenHP;
    public Sprite fifteenHP;
    public Sprite fourteenHP;
    public Sprite thirteenHP;
    public Sprite twelveHP;
    public Sprite elevenHP;
    public Sprite tenHP;
    public Sprite nineHP;
    public Sprite eightHP;
    public Sprite sevenHP;
    public Sprite sixHP;
    public Sprite fiveHP;
    public Sprite fourHP;
    public Sprite threeHP;
    public Sprite twoHP;
    public Sprite oneHP;
    public Sprite zeroHP;
    // Start is called before the first frame update
    void Start()
    {
        p = GameObject.Find("Player").GetComponent<Player>();
        

        gameObject.GetComponent<Image>().sprite = fullHP;
    }

    // Update is called once per frame
    void Update()
    {
        float currentHP = p.getHP();
        float maxHP = p.getMaxHP();
        
        float hpPercent = currentHP / maxHP;

        if (hpPercent >= 1) 
        {
            gameObject.GetComponent<Image>().sprite = fullHP;
        }
        else if (hpPercent < 1 && hpPercent >= 0.95)
        {
            gameObject.GetComponent<Image>().sprite = nineteenHP;
        }
        else if (hpPercent < 0.95 && hpPercent >= 0.90)
        {
            gameObject.GetComponent<Image>().sprite = eighteenHP;
        }
        else if (hpPercent < 0.90 && hpPercent >= 0.85)
        {
            gameObject.GetComponent<Image>().sprite = seventeenHP;
        }
        else if (hpPercent < 0.85 && hpPercent >= 0.80)
        {
            gameObject.GetComponent<Image>().sprite = sixteenHP;
        }
        else if (hpPercent < 0.80 && hpPercent >= 0.75)
        {
            gameObject.GetComponent<Image>().sprite = fifteenHP;
        }
        else if (hpPercent < 0.75 && hpPercent >= 0.70)
        {
            gameObject.GetComponent<Image>().sprite = fourteenHP;
        }
        else if (hpPercent < 0.70 && hpPercent >= 0.65)
        {
            gameObject.GetComponent<Image>().sprite = thirteenHP;
        }
        else if (hpPercent < 0.65 && hpPercent >= 0.60)
        {
            gameObject.GetComponent<Image>().sprite = twelveHP;
        }
        else if (hpPercent < 0.60 && hpPercent >= 0.55)
        {
            gameObject.GetComponent<Image>().sprite = elevenHP;
        }
        else if (hpPercent < 0.55 && hpPercent >= 0.50)
        {
            gameObject.GetComponent<Image>().sprite = tenHP;
        }
        else if (hpPercent < 0.50 && hpPercent >= 0.45)
        {
            gameObject.GetComponent<Image>().sprite = nineHP;
        }
        else if (hpPercent < 0.45 && hpPercent >= 0.40)
        {
            gameObject.GetComponent<Image>().sprite = eightHP;
        }
        else if (hpPercent < 0.40 && hpPercent >= 0.35)
        {
            gameObject.GetComponent<Image>().sprite = sevenHP;
        }
        else if (hpPercent < 0.35 && hpPercent >= 0.30)
        {
            gameObject.GetComponent<Image>().sprite = sixHP;
        }
        else if (hpPercent < 0.30 && hpPercent >= 0.25)
        {
            gameObject.GetComponent<Image>().sprite = fiveHP;
        }
        else if (hpPercent < 0.25 && hpPercent >= 0.20)
        {
            gameObject.GetComponent<Image>().sprite = fourHP;
        }
        else if (hpPercent < 0.20 && hpPercent >= 0.15)
        {
            gameObject.GetComponent<Image>().sprite = threeHP;
        }
        else if (hpPercent < 0.15 && hpPercent >= 0.10)
        {
            gameObject.GetComponent<Image>().sprite = twoHP;
        }
        else if (hpPercent < 0.10 && hpPercent >= 0.05)
        {
            gameObject.GetComponent<Image>().sprite = oneHP;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = zeroHP;
        }
    }
}