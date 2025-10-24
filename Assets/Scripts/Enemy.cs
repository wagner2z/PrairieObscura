using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UIHandler;

public class Enemy : MonoBehaviour
{
    Player p;
    CameraMove c;
    float moveSpeed = 1f;
    float maxHitPoints;
    float currentHP;
    bool facingRight;
    float isShotTime;
    int damageDealt;
    string enemyName;
    Vector3 movement;
    //UIHandler ui;
    const float offScreenX = -25f;
    const float offScreenY = -15f;
    const float beenShotTime = 0.5f;


    void Start()
    {
        p = GameObject.Find("Player").GetComponent<Player>();
        c = GameObject.Find("Main Camera").GetComponent<CameraMove>();
        enemyName = gameObject.name;
        isShotTime = 0;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        //ui = GameObject.Find("SceneHandler").GetComponent<UIHandler>();


        if (enemyName.Contains("Zombie"))
        {
            setBaseHP(1);
            setDamageDealt(0);
            //setBaseHP(gameObject.GetComponent<Zombie>().getMaxHP());
            //setDamageDealt(gameObject.GetComponent<Zombie>().getDamageDealt());
        }
        /*else if (enemyName.Contains("Murder Goose"))
        {
            setBaseHP(gameObject.GetComponent<RaptorGoose>().getMaxHP());
            setDamageDealt(gameObject.GetComponent<RaptorGoose>().getDamageDealt());
        }*/
    }

    void Update()
    {
        if (!c.isWithinDistance(transform.position))
        {
            GetComponent<Renderer>().enabled = false;
        }
        else
        {
            GetComponent<Renderer>().enabled = true;
            /*if (!ui.isPaused())
            {
                if (enemyName.Contains("Zombie"))
                {
                    //gameObject.GetComponent<SpoonGoon>().setAnimSpeed(1);
                }
                /*else if (enemyName.Contains("Murder Goose"))
                {
                    movement = gameObject.GetComponent<RaptorGoose>().move();
                    gameObject.GetComponent<RaptorGoose>().setAnimSpeed(1);
                }

                GetComponent<Renderer>().material.color = shotTime();


                if (isDead())
                {
                    gameObject.transform.position = new Vector3(offScreenX, offScreenY, 0);
                    // move offscreen and not move
                    //gameObject.SetActive(false);
                }
            }
            else
            {
                /*if (enemyName.Contains("Spoon Goon"))
                {
                    gameObject.GetComponent<SpoonGoon>().setAnimSpeed(0);
                }
                else if (enemyName.Contains("Murder Goose"))
                {
                    gameObject.GetComponent<RaptorGoose>().setAnimSpeed(0);
                }
            }*/
        }
        if (//!ui.isPaused() && 
            c.isWithinDistance(transform.position) && !isDead())
        {
            Vector3 playerWorldPos = p.transform.position;
            playerWorldPos.z = 0;
            Vector3 direction = playerWorldPos - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle -= 90; // Example adjustment
            transform.rotation = Quaternion.Euler(0, 0, angle);

            Vector3 forwardDirection = transform.up;
            // Move the character in the backward direction
            // Time.deltaTime ensures frame-rate independent movement
            transform.Translate(forwardDirection * moveSpeed * Time.deltaTime, Space.World);
        }

    }

    public Color takeDamage(float damage)
    {
        currentHP = currentHP - damage;
        isShotTime = beenShotTime;
        Debug.Log("HP of enemy shot is " + currentHP);
        return Color.red;
    }
    public bool isDead()
    {
        if (currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public Color shotTime()
    {
        if (isShotTime > 0)
        {
            isShotTime -= Time.deltaTime;
            return Color.red;
        }
        else
        {
            return Color.white;
        }
    }

    public void setBaseHP(float hp)
    {
        setMaxHP(hp);
        setCurrentHP(hp);
    }

    public float getMaxHP()
    {
        return maxHitPoints;
    }

    public void setMaxHP(float hp)
    {
        maxHitPoints = hp;
    }

    public float getCurrentHP()
    {
        return currentHP;
    }

    public void setCurrentHP(float hp)
    {
        currentHP = hp;
    }

    public void setDamageDealt(int d)
    {
        damageDealt = d;
    }

    public int getDamageDealt()
    {
        return damageDealt;
    }

    public bool isFacingRight()
    {
        return facingRight;
    }

    public void setFacingDirection(bool isFacingRight)
    {
        facingRight = isFacingRight;
    }

    public float getShotTime()
    {
        return isShotTime;
    }

    public float getX()
    {
        return transform.position.x;
    }

    public float getY()
    {
        return transform.position.y;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (collision.collider.gameObject.tag == "Shot")
        {
            GetComponent<Renderer>().material.color = takeDamage(collision.collider.gameObject.GetComponent<Shot>().getDamage());
        }
        if (collision.collider.gameObject.tag == "MeleeHit")
        {
            weaponSelect = GameObject.Find("WeaponSelect");
            GetComponent<Renderer>().material.color = takeDamage(weaponSelect.GetComponent<WeaponSelect>().getCurrentMeleeWeapon().GetComponent<Melee>().getDamage());
        }

        if (collision.collider.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }*/
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        /*if (collision.collider.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }*/
    }
}
