using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour
{
    bool full;
    // Start is called before the first frame update
    void Start()
    {
        full = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setFull()
    {
        full = true;
    }

    public bool isFull()
    {
        return full;
    }
}
