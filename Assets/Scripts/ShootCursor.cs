using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCursor : MonoBehaviour
{
    public Animator anim;
    AnimatorStateInfo stateInfo;
    float lockOnTime;
    Player p;
    GameObject enemySelected;

    // Start is called before the first frame update
    void Start()
    {
        lockOnTime = 0f;
        p = GameObject.Find("Player").GetComponent<Player>();
        enemySelected = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Renderer>().enabled)
        {
            stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            lockOnTime = stateInfo.normalizedTime;
        }
        else
        {
            anim.Play("default", 0, 0);
            lockOnTime = 0f;
        }
    }

    public GameObject getEnemyTarget()
    {
        return enemySelected;
    }

    public float getLockOnTime()
    {
        return lockOnTime;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy" && GetComponent<Renderer>().enabled)
        {
            anim.Play("active_cursor", 0, 0);
            enemySelected = collider.gameObject;
        }

    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Enemy" && GetComponent<Renderer>().enabled)
        {
            if(stateInfo.IsName("default"))
            {
                anim.Play("active_cursor", 0, 0);
            }
            enemySelected = collider.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            anim.Play("remove_cursor", 0, lockOnTime);
            enemySelected = null;
        }

    }

    
}
