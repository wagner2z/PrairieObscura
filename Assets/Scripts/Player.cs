using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    ControlAssignment control = new ControlAssignment();
    const float maxWalkSpeed = 5f;
    const float maxRunSpeed = 8f;
    float moveSpeed;
    public Rigidbody2D rigidBody;
    //float xVel;
    //float yVel;
    const int maxHitPoints = 10;
    int currentHP;
    const int maxStamina = 10;
    int currentStamina;
    const float recoverTime = 1f;
    const float staminaDiffTime = 0.25f;
    float tempRecoverTime;
    float tempDiffTime;
    float isHitTime;
    const float beenHitTime = 0.5f;
    Vector3 faceDirection;
    Vector3 moveDirection;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHitPoints;
        currentStamina = maxStamina;
        isHitTime = 0f;
        tempRecoverTime = recoverTime;
        tempDiffTime = staminaDiffTime;
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector3(0, 0, 0);
        moveSpeed = 0;
        anim.SetBool("Walking", false);

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        faceDirection = mouseWorldPos - new Vector3(rigidBody.position.x, rigidBody.position.y, 0);
        float angle = Mathf.Atan2(faceDirection.y, faceDirection.x) * Mathf.Rad2Deg;
        angle -= 90; // Example adjustment
        transform.rotation = Quaternion.Euler(0, 0, angle);

        anim.SetBool("Walking", false);
        moveSpeed = 0;

        if (Input.GetKey(control.characterMoveBack()))
        {
            // Calculate the backward direction based on the character's current forward vector
            // Multiplying by -1 reverses the direction
            if (Input.GetKey(control.characterDash()) && currentStamina > 0)
            {
                moveSpeed = maxRunSpeed;
                useStamina();
            }
            else
            {
                moveSpeed = maxWalkSpeed;
            }
            moveDirection = -transform.up;
            anim.SetBool("Walking", true);

            // Move the character in the backward direction
            // Time.deltaTime ensures frame-rate independent movement
            //transform.Translate(backwardDirection * moveSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(control.characterMoveForward()))
        {
            // Calculate the backward direction based on the character's current forward vector
            // Multiplying by -1 reverses the direction
            if (Input.GetKey(control.characterDash()) && currentStamina > 0)
            {
                moveSpeed = maxRunSpeed;
                useStamina();
            }
            else
            {
                moveSpeed = maxWalkSpeed;
            }
            moveDirection = transform.up;
            anim.SetBool("Walking", true);

            // Move the character in the backward direction
            // Time.deltaTime ensures frame-rate independent movement
            //transform.Translate(forwardDirection * moveSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(control.characterMoveLeft()))
        {
            // Calculate the backward direction based on the character's current forward vector
            // Multiplying by -1 reverses the direction
            if (Input.GetKey(control.characterDash()) && currentStamina > 0)
            {
                moveSpeed = maxRunSpeed;
                useStamina();
            }
            else
            {
                moveSpeed = maxWalkSpeed;
            }
            moveDirection = -transform.right;
            anim.SetBool("Walking", true);

            // Move the character in the backward direction
            // Time.deltaTime ensures frame-rate independent movement
            //transform.Translate(leftDirection * moveSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(control.characterMoveRight()))
        {
            // Calculate the backward direction based on the character's current forward vector
            // Multiplying by -1 reverses the direction
            if (Input.GetKey(control.characterDash()) && currentStamina > 0)
            {
                moveSpeed = maxRunSpeed;
                useStamina();
            }
            else
            {
                moveSpeed = maxWalkSpeed;
            }
            moveDirection = transform.right;
            anim.SetBool("Walking", true);

            // Move the character in the backward direction
            // Time.deltaTime ensures frame-rate independent movement
            //transform.Translate(rightDirection * moveSpeed * Time.deltaTime, Space.World);
        }

        if(!Input.GetKey(control.characterDash()))
        {
            recoverStamina();
            
        }

        if (isHitTime > 0)
        {
            isHitTime -= Time.deltaTime;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }

    void FixedUpdate()
    {
        rigidBody.velocity = new Vector3(0, 0, 0);
        rigidBody.angularVelocity = 0f;
        rigidBody.velocity = moveDirection * moveSpeed;
        //if (!ui.isPaused())
        //{

        //}
    }

    public int getHP()
    {
        return currentHP;
    }

    public int getStamina()
    {
        return currentStamina;
    }

    public void addHP(int h)
    {
        if (currentHP + h >= maxHitPoints)
        {
            currentHP = maxHitPoints;
        }
        else
        {
            currentHP += h;
        }
    }

    public void removeHP(int h)
    {
        if (currentHP - h <= 0)
        {
            currentHP = 0;
        }
        else
        {
            currentHP -= h;
        }
    }

    public void addStamina(int s)
    {
        if (currentStamina + s >= maxStamina)
        {
            currentStamina = maxStamina;
        }
        else
        {
            currentStamina += s;
        }
    }

    public void removeStamina(int s)
    {
        if (currentStamina - s <= 0)
        {
            currentStamina = 0;
        }
        else
        {
            currentStamina -= s;
        }
    }

    public void useStamina()
    {
        tempRecoverTime = recoverTime;
        if (currentStamina > 0)
        {

                if (tempDiffTime > 0)
                {
                    tempDiffTime -= Time.deltaTime;
                }
                else
                {
                    removeStamina(1);
                    tempDiffTime = staminaDiffTime;
                }
        }
        else
        {
            tempDiffTime = staminaDiffTime;
        }
    }

    public void recoverStamina()
    {
        if (currentStamina < maxStamina)
        {
            if (tempRecoverTime > 0)
            {
                tempRecoverTime -= Time.deltaTime;

            }
            else
            {
                if (tempDiffTime > 0)
                {
                    tempDiffTime -= Time.deltaTime;
                }
                else
                {
                    addStamina(1);
                    tempDiffTime = (staminaDiffTime * 2);
                }
            }
        }
        else
        {
            tempRecoverTime = recoverTime;
            tempDiffTime = (staminaDiffTime * 2);
        }
    }

    public int getMaxHP()
    {
        return maxHitPoints;
    }

    public int getMaxStamina()
    {
        return maxStamina;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.collider.gameObject.tag == "Enemy") && isHitTime <= 0f)
        {
            removeHP(collision.collider.gameObject.GetComponent<Enemy>().getDamageDealt());

            GetComponent<Renderer>().material.color = Color.red;
            //GameObject dmg = transform.GetChild(3).gameObject;
            //dmg.transform.position = collision.contacts[0].point;
            //dmg.GetComponent<ParticleSystem>().Play();
            isHitTime = beenHitTime;
            Debug.Log("Current HP is " + currentHP);
        }
        if ((collision.collider.gameObject.tag == "Wall"))
        {
            moveSpeed = 0;
        }
    }


}
