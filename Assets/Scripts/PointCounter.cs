using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointCounter : MonoBehaviour
{
    int points;
    const int maxPoints = 99999;
    string pointString;
    int startPos;
    GameObject pointDisplay;
    Image currentPoint;
    public Sprite point0;
    public Sprite point1;
    public Sprite point2;
    public Sprite point3;
    public Sprite point4;
    public Sprite point5;
    public Sprite point6;
    public Sprite point7;
    public Sprite point8;
    public Sprite point9;

    // Start is called before the first frame update
    void Start()
    {
        //pointDisplay = GameObject.Find("PointCounter");
        points = 0;
        startPos = 0;
        pointString = points.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        pointString = points.ToString();
        switch (pointString.Length)
        {
           case 1:
                startPos = 0;
                break;
           case 2:
                startPos = 1;
                break;         
           case 3:
                startPos = 2;
                break;
           case 4:
                startPos = 3;
                break;
           case 5:
                startPos = 4;
                break;
           default:
                startPos = 0;
                break;
        }
        foreach(char digit in pointString)
        {
            currentPoint = transform.GetChild(startPos).gameObject.GetComponent<Image>();
            switch (digit)
            {
                case '0':
                    currentPoint.sprite = point0;
                    break;
                case '1':
                    currentPoint.sprite = point1;
                    break;
                case '2':
                    currentPoint.sprite = point2;
                    break;
                case '3':
                    currentPoint.sprite = point3;
                    break;
                case '4':
                    currentPoint.sprite = point4;
                    break;
                case '5':
                    currentPoint.sprite = point5;
                    break;
                case '6':
                    currentPoint.sprite = point6;
                    break;
                case '7':
                    currentPoint.sprite = point7;
                    break;
                case '8':
                    currentPoint.sprite = point8;
                    break;
                case '9':
                    currentPoint.sprite = point9;
                    break;
                default:
                    currentPoint.sprite = point0;
                    break;
            }
            startPos--;
        }
    }

    public int getPoints()
    {
        return points;
    }

    public void increasePoints(int p)
    {
        if (points + p > maxPoints)
        {
            points = maxPoints;
        }
        else
        {
            points += p;
        }
    }

    public void decreasePoints(int p)
    {
        if (points -  p <= 0)
        {
            points = 0;
        }
        else
        {
            points = points - p;
        }
       
    }


}
