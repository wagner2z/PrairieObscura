using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    ControlAssignment control = new ControlAssignment();
    const float startX = 0f;
    const float startY = -2.9f;
    GunTypes[] availableGuns;
    GunTypes currentGun;
    GameObject canvas;
    int currentWeaponPos;
    bool isEquipped;
    const int maxWeaponPos = 2;
    public Sprite idleSprite;
    public Sprite shootSprite;
    const float maxWalkSpeed = 5f;
    const float maxRunSpeed = 8f;
    float moveSpeed;
    public Rigidbody2D rigidBody;
    GameObject shoot_cursor;
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
    float tempReloadTime;
    bool isReloading;
    bool isInside;
    //int testGunDamage = 5;
    Vector3 faceDirection;
    Vector3 moveDirection;
    public Animator anim;
    public RuntimeAnimatorController baseAnim;
    public RuntimeAnimatorController handgun1;
    public RuntimeAnimatorController rifle1;
    public RuntimeAnimatorController shotgun1;

    AudioSource playerSounds;
    public AudioClip shootSound;
    public AudioClip reloadSound;
    public AudioClip completeReloadSound;
    public AudioClip equipSound;

    // Start is called before the first frame update
    void Start()
    {
        //Physics.IgnoreLayerCollision(0, 5);
        transform.position = new Vector3(startX, startY, -0.72f);
        currentHP = maxHitPoints;
        isEquipped = false;
        currentWeaponPos = -1;
        canvas = GameObject.Find("Canvas");
        //Transform uiParent = canvas.Find("SelectedGun").transform;
        availableGuns = new GunTypes [maxWeaponPos + 1];
        availableGuns[0] = new GunTypes("revolver", 1, 5, 6, 1, 3f, 1, true, canvas.transform.Find("SelectedGun/RevolverUI (1)"), handgun1);
        availableGuns[1] = new GunTypes("bolt rifle", 1, 8, 1, 1, 0.67f, 2, true, canvas.transform.Find("SelectedGun/BoltRifleUI (1)"), rifle1);
        availableGuns[2] = new GunTypes("double barrel shotgun", 1, 12, 2, 2, 0.82f, 3, true, canvas.transform.Find("SelectedGun/DoubleBarrelUI (1)"), shotgun1);
        currentStamina = maxStamina;
        isHitTime = 0f;
        tempRecoverTime = recoverTime;
        tempDiffTime = staminaDiffTime;
        tempReloadTime = 0f;
        isReloading = false;
        isInside = false;
        //this.sRenderer = gameObject.GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector3(0, 0, 0);
        moveSpeed = 0;
        shoot_cursor = GameObject.Find("Shoot_Cursor");
        anim.SetBool("Walking", false);
        anim.SetBool("Shooting", false);
        anim.SetBool("Reloading", false);

        playerSounds = GetComponent<AudioSource>();
        //this.sRenderer.sprite = shootSprite;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead())
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0;
            faceDirection = mouseWorldPos - new Vector3(rigidBody.position.x, rigidBody.position.y, 0);
            float angle = Mathf.Atan2(faceDirection.y, faceDirection.x) * Mathf.Rad2Deg;
            angle -= 90; // Example adjustment
            transform.rotation = Quaternion.Euler(0, 0, angle);
            anim.SetBool("Walking", false);
            anim.SetBool("Shooting", false);
            moveSpeed = 0;
            if (isEquipped)
            {
                anim.runtimeAnimatorController = currentGun.getAnimator();
            }
            else
            {
                anim.runtimeAnimatorController = baseAnim;
            }

            if (Input.GetKey(control.playerFirePosition()) && isEquipped && !isReloading)
            {
                Cursor.visible = false;
                shoot_cursor.GetComponent<Renderer>().enabled = true;
                shoot_cursor.transform.position = mouseWorldPos;
                anim.SetBool("Shooting", true);
                //sRenderer.sprite = shootSprite;
                if (Input.GetKeyDown(control.playerShoot()) && currentGun.getCurrentGunAmmo() > 0)
                {
                    firedShot();
                }
            }

            else
            {
                anim.SetBool("Shooting", false);
                Cursor.visible = true;
                shoot_cursor.GetComponent<Renderer>().enabled = false;
                if (!isReloading)
                {
                    if (isEquipped && Input.GetKeyDown(control.reloadGun()))
                    {
                        reloadGun();
                    }
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
                    if (Input.GetKeyDown(control.switchWeaponLeft()))
                    {
                        swapWeapon('l');
                    }
                    if (Input.GetKeyDown(control.switchWeaponRight()))
                    {
                        swapWeapon('r');
                    }
                }

            }

            if (!Input.GetKey(control.characterDash()))
            {
                recoverStamina();

            }

            if (isReloading)
            {
                if (tempReloadTime > 0)
                {
                    tempReloadTime -= Time.deltaTime;
                }
                else
                {
                    currentGun.reload();
                    anim.SetBool("Reloading", false);
                    playerSounds.clip = completeReloadSound;
                    playerSounds.Play();
                    isReloading = false;
                }
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
    }

    void FixedUpdate()
    {
        if (!isDead())
        {
            rigidBody.velocity = new Vector3(0, 0, 0);
            rigidBody.angularVelocity = 0f;
            rigidBody.velocity = moveDirection * moveSpeed;
        }
        else
        {
            rigidBody.velocity = new Vector3(0, 0, 0);
        }
    }

    public int getHP()
    {
        return currentHP;
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

    public void swapWeapon(char dir)
    {
        bool weaponAvailable = false;
        if(dir == 'l')
        {
            if(currentWeaponPos == 0)
            {
                currentWeaponPos = -1;
                isEquipped = false;
            }
            else
            {
                if(currentWeaponPos == -1)
                {
                    currentWeaponPos = maxWeaponPos + 1;
                }
                currentWeaponPos--;
                for (int i = currentWeaponPos; i >= 0; i--)
                {
                    if (availableGuns[i].getGunAvailable())
                    {
                        currentGun = availableGuns[i];
                        currentWeaponPos = i;
                        weaponAvailable = true;
                        break;
                    }
                }
                if (weaponAvailable)
                {
                    isEquipped = true;
                    playerSounds.clip = equipSound;
                    playerSounds.Play();
                }
                else
                {
                    currentWeaponPos = -1;
                    isEquipped = false;
                }
            }
        }
        else if (dir == 'r')
        {
            if (currentWeaponPos == maxWeaponPos)
            {
                currentWeaponPos = -1;
                isEquipped = false;
            }
            else
            {
                currentWeaponPos++;
                for (int i = currentWeaponPos; i <= maxWeaponPos; i++)
                {
                    if (availableGuns[i].getGunAvailable())
                    {
                        currentGun = availableGuns[i];
                        currentWeaponPos = i;
                        weaponAvailable = true;
                        break;
                    }
                }
                if (weaponAvailable)
                {
                    isEquipped = true;
                    playerSounds.clip = equipSound;
                    playerSounds.Play();
                }
                else
                {
                    currentWeaponPos = -1;
                    isEquipped = false;
                }
            }
        }
    }

    public GunTypes getCurrentGun()
    {
        if (isEquipped)
        {
            return currentGun;
        }
        return null;
    }

    public void firedShot()
    {
        StartCoroutine(gameObject.transform.GetChild(0).GetComponent<Gunshot_Effect>().gunshot());
        anim.Play("lee_shoot", 0, 0);
        GameObject enemyTarget = shoot_cursor.GetComponent<ShootCursor>().getEnemyTarget();
        shoot_cursor.GetComponent<ShootCursor>().anim.Play("default", 0, 0);
        currentGun.reduceAmmo();
        playerSounds.clip = shootSound;
        playerSounds.Play();
        if (enemyTarget != null)
        {
            float damageDealt = (currentGun.getGunDamage() * shoot_cursor.GetComponent<ShootCursor>().getLockOnTime());
            enemyTarget.GetComponent<Enemy>().takeDamage((int)damageDealt + 1);
        }
        //anim.SetBool("Shot Fired", false);

    }

    public void reloadGun()
    {
       tempReloadTime = currentGun.getGunReloadTime();
       anim.SetBool("Reloading", true);
        playerSounds.clip = reloadSound;
        playerSounds.Play();
        isReloading = true;
    }

    public int getMaxHP()
    {
        return maxHitPoints;
    }

    public int getMaxStamina()
    {
        return maxStamina;
    }

    public bool getEquipped()
    {
        return isEquipped;
    }

    public void setEquipped(bool e)
    {
        isEquipped = e;
    }

    public bool playerInside()
    {
        return isInside;
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

        if(collision.collider.gameObject.tag == "Door")
        {
            Door d = collision.collider.gameObject.GetComponent<Door>();
            gameObject.transform.position = new Vector3(d.xPlacement, d.yPlacement, 0);
            isInside = d.indoors;
        }

    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Enemy"
            && Input.GetKey(control.playerPush())
            && Vector3.Dot(faceDirection, Vector3.Normalize(collision.collider.gameObject.transform.position - transform.position)) > 0)
        {
            if (isEquipped)
            {
                collision.collider.gameObject.GetComponent<Enemy>().takeDamage(currentGun.getMeleeDamage());
            }
            collision.collider.gameObject.GetComponent<Enemy>().setPushed();
            //collision.collider.gameObject.GetComponent<Enemy>().takeDamage(1);
            //Debug.Log("Dot is " + Vector3.Dot(faceDirection, Vector3.Normalize(collision.collider.gameObject.transform.position - transform.position)));
            
        }
    }


}
