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
    public Sprite thirtyoneHP;
    public Sprite thirtyHP;
    public Sprite twentynineHP;
    public Sprite twentyeightHP;
    public Sprite twentysevenHP;
    public Sprite twentysixHP;
    public Sprite twentyfiveHP;
    public Sprite twentyfourHP;
    public Sprite twentythreeHP;
    public Sprite twentytwoHP;
    public Sprite twentyoneHP;
    public Sprite twentyHP;
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
        else if (hpPercent < 1 && hpPercent >= 0.96875)
        {
            gameObject.GetComponent<Image>().sprite = thirtyoneHP;
        }
        else if (hpPercent < 0.96875 && hpPercent >= 0.9375)
        {
            gameObject.GetComponent<Image>().sprite = thirtyHP;
        }
        else if (hpPercent < 0.9375 && hpPercent >= 0.90625)
        {
            gameObject.GetComponent<Image>().sprite = twentynineHP;
        }
        else if (hpPercent < 0.90625 && hpPercent >= 0.875)
        {
            gameObject.GetComponent<Image>().sprite = twentyeightHP;
        }
        else if (hpPercent < 0.875 && hpPercent >= 0.84375)
        {
            gameObject.GetComponent<Image>().sprite = twentysevenHP;
        }
        else if (hpPercent < 0.84375 && hpPercent >= 0.8125)
        {
            gameObject.GetComponent<Image>().sprite = twentysixHP;
        }
        else if (hpPercent < 0.8125 && hpPercent >= 0.78125)
        {
            gameObject.GetComponent<Image>().sprite = twentyfiveHP;
        }
        else if (hpPercent < 0.78125 && hpPercent >= 0.75)
        {
            gameObject.GetComponent<Image>().sprite = twentyfourHP;
        }
        else if (hpPercent < 0.75 && hpPercent >= 0.71875)
        {
            gameObject.GetComponent<Image>().sprite = twentythreeHP;
        }
        else if (hpPercent < 0.71875 && hpPercent >= 0.6875)
        {
            gameObject.GetComponent<Image>().sprite = twentytwoHP;
        }
        else if (hpPercent < 0.6875 && hpPercent >= 0.65625)
        {
            gameObject.GetComponent<Image>().sprite = twentyoneHP;
        }
        else if (hpPercent < 0.65625 && hpPercent >= 0.625)
        {
            gameObject.GetComponent<Image>().sprite = twentyHP;
        }
        else if (hpPercent < 0.625 && hpPercent >= 0.59375)
        {
            gameObject.GetComponent<Image>().sprite = nineteenHP;
        }
        else if (hpPercent < 0.59375 && hpPercent >= 0.5625)
        {
            gameObject.GetComponent<Image>().sprite = eighteenHP;
        }
        else if (hpPercent < 0.5625 && hpPercent >= 0.53125)
        {
            gameObject.GetComponent<Image>().sprite = seventeenHP;
        }
        else if (hpPercent < 0.53125 && hpPercent >= 0.5)
        {
            gameObject.GetComponent<Image>().sprite = sixteenHP;
        }
        else if (hpPercent < 0.5 && hpPercent >= 0.46875)
        {
            gameObject.GetComponent<Image>().sprite = fifteenHP;
        }
        else if (hpPercent < 0.46875 && hpPercent >= 0.4375)
        {
            gameObject.GetComponent<Image>().sprite = fourteenHP;
        }
        else if (hpPercent < 0.4375 && hpPercent >= 0.40625)
        {
            gameObject.GetComponent<Image>().sprite = thirteenHP;
        }
        else if (hpPercent < 0.40625 && hpPercent >= 0.375)
        {
            gameObject.GetComponent<Image>().sprite = twelveHP;
        }
        else if (hpPercent < 0.375 && hpPercent >= 0.34375)
        {
            gameObject.GetComponent<Image>().sprite = elevenHP;
        }
        else if (hpPercent < 0.34375 && hpPercent >= 0.3125)
        {
            gameObject.GetComponent<Image>().sprite = tenHP;
        }
        else if (hpPercent < 0.3125 && hpPercent >= 0.28125)
        {
            gameObject.GetComponent<Image>().sprite = nineHP;
        }
        else if (hpPercent < 0.28125 && hpPercent >= 0.25)
        {
            gameObject.GetComponent<Image>().sprite = eightHP;
        }
        else if (hpPercent < 0.25 && hpPercent >= 0.21875)
        {
            gameObject.GetComponent<Image>().sprite = sevenHP;
        }
        else if (hpPercent < 0.21875 && hpPercent >= 0.1875)
        {
            gameObject.GetComponent<Image>().sprite = sixHP;
        }
        else if (hpPercent < 0.1875 && hpPercent >= 0.15625)
        {
            gameObject.GetComponent<Image>().sprite = fiveHP;
        }
        else if (hpPercent < 0.15625 && hpPercent >= 0.125)
        {
            gameObject.GetComponent<Image>().sprite = fourHP;
        }
        else if (hpPercent < 0.125 && hpPercent >= 0.09375)
        {
            gameObject.GetComponent<Image>().sprite = threeHP;
        }
        else if (hpPercent < 0.09375 && hpPercent >= 0.0625)
        {
            gameObject.GetComponent<Image>().sprite = twoHP;
        }
        else if (hpPercent < 0.0625 && hpPercent >= 0.03125)
        {
            gameObject.GetComponent<Image>().sprite = oneHP;
        }
        else if (hpPercent < 0.03125 && hpPercent > 0)
        {
            gameObject.GetComponent<Image>().sprite = oneHP;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = zeroHP;
        }
    }
}