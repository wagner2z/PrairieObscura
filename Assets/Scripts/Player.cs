using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class Player : MonoBehaviour
{
    ControlAssignment control = new ControlAssignment();
    PointCounter point;
    WinHandler won;
    TextMeshProUGUI subMessageUI;
    Inventory inventory;
    const float startX = 16.6f;
    const float startY = -46.3f;
    const float offScreenX = -105f;
    const float offScreenY = 80f;
    GunTypes[] availableGuns;
    GunTypes currentGun;
    GameObject canvas;
    GameObject carryable;
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
    bool firingPosition;
    bool isInside;
    bool isCarryingObject;
    bool foundCarryObject;
    const float pickUpWaitTime = 0.2f;
    float tempWaitTime;
    //int testGunDamage = 5;
    Vector3 faceDirection;
    Vector3 moveDirection;
    public Animator anim;
    Tilemap tilemap;
    public RuntimeAnimatorController baseAnim;
    public RuntimeAnimatorController handgun1;
    public RuntimeAnimatorController rifle1;
    public RuntimeAnimatorController shotgun1;
    GameObject foundObject;

    public AudioSource playerMoveSounds;
    public AudioSource playerActionSounds;
    public AudioClip shootSound;
    public AudioClip reloadSound;
    public AudioClip completeReloadSound;
    public AudioClip equipSound;
    public AudioClip doorOpen;
    public AudioClip dirtWalk;
    public AudioClip dirtRun;
    public AudioClip grassWalk;
    public AudioClip grassRun;
    public AudioClip defaultWalk;
    public AudioClip defaultRun;

    // Start is called before the first frame update
    void Start()
    {
        //Physics.IgnoreLayerCollision(0, 5);
        tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
        won = GameObject.Find("SceneHandler").GetComponent<WinHandler>();
        inventory = GameObject.Find("SceneHandler").GetComponent<Inventory>();
        point = GameObject.Find("Point Counter").GetComponent<PointCounter>();
        subMessageUI = GameObject.Find("SubMessage").GetComponent<TextMeshProUGUI>();
        subMessageUI.enabled = false;
        transform.position = new Vector3(startX, startY, -0.72f);
        currentHP = maxHitPoints;
        isEquipped = false;
        currentWeaponPos = -1;
        canvas = GameObject.Find("Canvas");
        carryable = transform.GetChild(1).gameObject;
        carryable.GetComponent<Renderer>().enabled = false;
        //Transform uiParent = canvas.Find("SelectedGun").transform;
        availableGuns = new GunTypes[maxWeaponPos + 1];
        availableGuns[0] = new GunTypes("revolver", 1, 5, 6, 0, 1, 3f, 1, true, canvas.transform.Find("SelectedGun/RevolverUI (1)"), handgun1, true);
        availableGuns[1] = new GunTypes("bolt rifle", 1, 8, 3, 1, 1, 2f, 2, true, canvas.transform.Find("SelectedGun/BoltRifleUI (1)"), rifle1, false);
        availableGuns[2] = new GunTypes("double barrel shotgun", 1, 12, 2, 2, 2, 0.82f, 3, true, canvas.transform.Find("SelectedGun/DoubleBarrelUI (1)"), shotgun1, false);

        currentStamina = maxStamina;
        isHitTime = 0f;
        tempRecoverTime = recoverTime;
        tempDiffTime = staminaDiffTime;
        tempWaitTime = 0f;
        tempReloadTime = 0f;
        isReloading = false;
        isInside = false;
        isCarryingObject = false;
        foundCarryObject = false;
        //this.sRenderer = gameObject.GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector3(0, 0, 0);
        moveSpeed = 0;
        shoot_cursor = GameObject.Find("Shoot_Cursor");
        anim.SetBool("Walking", false);
        anim.SetBool("Shooting", false);
        anim.SetBool("Reloading", false);

        //playerMoveSounds; = GetComponent<AudioSource>();
        //playerActionSounds; //= GetComponent<AudioSource>();
        //this.sRenderer.sprite = shootSprite;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead() && !hasWon())
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0;
            faceDirection = mouseWorldPos - new Vector3(rigidBody.position.x, rigidBody.position.y, 0);
            float angle = Mathf.Atan2(faceDirection.y, faceDirection.x) * Mathf.Rad2Deg;
            angle -= 90; // Example adjustment
            transform.rotation = Quaternion.Euler(0, 0, angle);
            anim.SetBool("Walking", false);
            anim.SetBool("Shooting", false);
            Vector3Int gridPosition = tilemap.WorldToCell(transform.position);
            Sprite tileSprite = tilemap.GetSprite(gridPosition);
            moveSpeed = 0;

            if (tempWaitTime > 0)
            {
                tempWaitTime -= Time.deltaTime;
            }

            if (isEquipped)
            {
                anim.runtimeAnimatorController = currentGun.getAnimator();
            }
            else
            {
                anim.runtimeAnimatorController = baseAnim;
            }

            if (Input.GetKeyDown(control.pickUpOrDrop()) && !firingPosition && !isCarryingObject && tempWaitTime <= 0
                && foundCarryObject)
            {
                foundObject.GetComponent<Renderer>().enabled = false;
                foundObject.transform.position = new Vector3(offScreenX, offScreenY, 0);
                
                setCarryableObject();
            }

            if (Input.GetKeyDown(control.pickUpOrDrop()) && isCarryingObject && !firingPosition && tempWaitTime <= 0)
            {
                dropCarryableObject();
            }

            if (Input.GetKey(control.playerFirePosition()) && isEquipped && !isReloading)
            {
                Cursor.visible = false;
                shoot_cursor.GetComponent<Renderer>().enabled = true;
                firingPosition = true;
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
                firingPosition = false;
                anim.SetBool("Shooting", false);
                Cursor.visible = true;
                shoot_cursor.GetComponent<Renderer>().enabled = false;
            }

            
            //if (!isReloading)
            //{
            if (isEquipped && !isCarryingObject && Input.GetKeyDown(control.reloadGun()))
            {
                reloadGun();
            }
            if (Input.GetKey(control.characterMoveBack()))
            {
                // Calculate the backward direction based on the character's current forward vector
                // Multiplying by -1 reverses the direction
                if (Input.GetKey(control.characterDash()) && currentStamina > 0
                    && !isReloading && !firingPosition)
                {
                    moveSpeed = maxRunSpeed;
                    useStamina();
                    anim.SetBool("Walking", true);
                }
                else
                {
                    if (isReloading || firingPosition)
                    {
                        moveSpeed = maxWalkSpeed / 2f;
                    }
                    else
                    {
                        moveSpeed = maxWalkSpeed;
                        anim.SetBool("Walking", true);
                    }
                }
                moveDirection = -transform.up;
                

                // Move the character in the backward direction
                // Time.deltaTime ensures frame-rate independent movement
                //transform.Translate(backwardDirection * moveSpeed * Time.deltaTime, Space.World);
            }
            if (Input.GetKey(control.characterMoveForward()))
            {
                // Calculate the backward direction based on the character's current forward vector
                // Multiplying by -1 reverses the direction
                if (Input.GetKey(control.characterDash()) && currentStamina > 0
                    && !isReloading && !firingPosition)
                {
                    moveSpeed = maxRunSpeed;
                    useStamina();
                    anim.SetBool("Walking", true);
                }
                else
                {
                    if (isReloading || firingPosition)
                    {
                        moveSpeed = maxWalkSpeed / 2f;
                    }
                    else
                    {
                        moveSpeed = maxWalkSpeed;
                        anim.SetBool("Walking", true);
                    }
                }
                moveDirection = transform.up;
                

                // Move the character in the backward direction
                // Time.deltaTime ensures frame-rate independent movement
                //transform.Translate(forwardDirection * moveSpeed * Time.deltaTime, Space.World);
            }
            if (Input.GetKey(control.characterMoveLeft()))
            {
                // Calculate the backward direction based on the character's current forward vector
                // Multiplying by -1 reverses the direction
                if (Input.GetKey(control.characterDash()) && currentStamina > 0
                    && !isReloading && !firingPosition)
                {
                    moveSpeed = maxRunSpeed;
                    useStamina();
                    anim.SetBool("Walking", true);
                }
                else
                {
                    if (isReloading || firingPosition)
                    {
                        moveSpeed = maxWalkSpeed / 2f;
                    }
                    else
                    {
                        moveSpeed = maxWalkSpeed;
                        anim.SetBool("Walking", true);
                    }
                }
                moveDirection = -transform.right;
                

                // Move the character in the backward direction
                // Time.deltaTime ensures frame-rate independent movement
                //transform.Translate(leftDirection * moveSpeed * Time.deltaTime, Space.World);
            }
            if (Input.GetKey(control.characterMoveRight()))
            {
                // Calculate the backward direction based on the character's current forward vector
                // Multiplying by -1 reverses the direction
                if (Input.GetKey(control.characterDash()) && currentStamina > 0
                    && !isReloading && !firingPosition)
                {
                    moveSpeed = maxRunSpeed;
                    anim.SetBool("Walking", true);
                    useStamina();
                }
                else
                {
                    if (isReloading || firingPosition)
                    {
                        moveSpeed = maxWalkSpeed / 2f;
                    }
                    else
                    {
                        moveSpeed = maxWalkSpeed;
                        anim.SetBool("Walking", true);
                    }

                }
                moveDirection = transform.right;
                

                // Move the character in the backward direction
                // Time.deltaTime ensures frame-rate independent movement
                //transform.Translate(rightDirection * moveSpeed * Time.deltaTime, Space.World);
            }

            if(moveSpeed > 0)
            {
                if(tileSprite == null)
                {
                    if (moveSpeed == maxRunSpeed)
                    {
                        playerMoveSounds.clip = defaultRun;

                    }
                    else
                    {
                        playerMoveSounds.clip = defaultWalk;
                    }
                }
                else if(tileSprite.name == "desat grass 2_11")
                {
                    if(moveSpeed == maxRunSpeed)
                    {
                        playerMoveSounds.clip = dirtRun;
                        
                    }
                    else
                    {
                        playerMoveSounds.clip = dirtWalk;
                    }
                }
                else if(tileSprite.name == "desat grass 2_34")
                {
                    if (moveSpeed == maxRunSpeed)
                    {
                        playerMoveSounds.clip = grassRun;

                    }
                    else
                    {
                        playerMoveSounds.clip = grassWalk;
                    }
                }
                else
                {
                    if (moveSpeed == maxRunSpeed)
                    {
                        playerMoveSounds.clip = defaultRun;

                    }
                    else
                    {
                        playerMoveSounds.clip = defaultWalk;
                    }
                }
                if (!playerMoveSounds.isPlaying)
                {
                    playerMoveSounds.Play();
                }
            }
            
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll < 0f)
            {
                swapWeapon('l');
            }
            if (scroll > 0f)
            {
                swapWeapon('r');
            }
            //}


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
                    int invReduce = currentGun.reload(inventory.getAmmoCount(currentGun.getAmmoInventoryPosition()));
                    inventory.reduceAmmo(currentGun.getAmmoInventoryPosition(), invReduce);
                    anim.SetBool("Reloading", false);
                    playerActionSounds.clip = completeReloadSound;
                    playerActionSounds.Play();
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

    public bool hasWon()
    {
        return won.hasWon();
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
        if (dir == 'l')
        {
            if (currentWeaponPos == 0)
            {
                currentWeaponPos = -1;
                isEquipped = false;
            }
            else
            {
                if (currentWeaponPos == -1)
                {
                    currentWeaponPos = maxWeaponPos + 1;
                }
                currentWeaponPos--;
                for (int i = currentWeaponPos; i >= 0; i--)
                {
                    if (availableGuns[i].getGunAvailable())
                    {
                        if (!isCarryingObject)
                        {
                            currentGun = availableGuns[i];
                            currentWeaponPos = i;
                            weaponAvailable = true;
                            break;
                        }
                        else
                        {
                            if (availableGuns[i].isSingleHanded())
                            {
                                currentGun = availableGuns[i];
                                currentWeaponPos = i;
                                weaponAvailable = true;
                                break;
                            }
                        }
                    }
                }
                if (weaponAvailable)
                {
                    isEquipped = true;
                    playerActionSounds.clip = equipSound;
                    playerActionSounds.Play();
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
                        if (!isCarryingObject)
                        {
                            currentGun = availableGuns[i];
                            currentWeaponPos = i;
                            weaponAvailable = true;
                            break;
                        }
                        else
                        {
                            if (availableGuns[i].isSingleHanded())
                            {
                                currentGun = availableGuns[i];
                                currentWeaponPos = i;
                                weaponAvailable = true;
                                break;
                            }
                        }
                    }
                }
                if (weaponAvailable)
                {
                    isEquipped = true;
                    playerActionSounds.clip = equipSound;
                    playerActionSounds.Play();
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
        playerActionSounds.clip = shootSound;
        playerActionSounds.Play();
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
        playerActionSounds.clip = reloadSound;
        playerActionSounds.Play();
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

    public bool carryingObject()
    {
        return isCarryingObject;
    }

    public GameObject carriedObject()
    {
        return foundObject;
    }

    public GunTypes[] getAllGuns()
    {
        return availableGuns;
    }

    public List<GunTypes> getUnlockedGuns()
    {
        List<GunTypes> unlockedGuns = new List<GunTypes>();
        //int temp = 0;
        for (int i = 0; i < maxWeaponPos + 1; i++)
        {
            if (availableGuns[i].getGunAvailable())
            {
                unlockedGuns.Add(availableGuns[i]);
                //temp++;
            }
        }

        return unlockedGuns;
    }

    public void setCarryableObject()
    {
        carryable.GetComponent<Renderer>().enabled = true;
        currentWeaponPos = -1;
        isEquipped = false;
        carryable.GetComponent<SpriteRenderer>().sprite = foundObject.GetComponent<SpriteRenderer>().sprite;
        isCarryingObject = true;
        tempWaitTime = pickUpWaitTime;
    }

    public void removeCarryableObject()
    {
        carryable.GetComponent<Renderer>().enabled = false;
        isCarryingObject = false;
    }

    public void dropCarryableObject()
    {
        foundObject.GetComponent<Renderer>().enabled = true;
        foundObject.transform.position = transform.position;
        carryable.GetComponent<Renderer>().enabled = false;
        isCarryingObject = false;
        tempWaitTime = pickUpWaitTime;
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

        if (collision.collider.gameObject.tag == "Door")
        {
            subMessageUI.text = "";
            Door door = collision.collider.gameObject.GetComponent<Door>();
            if (!door.isDoorUnlocked())
            {
                if (point.getPoints() >= door.pointsNeeded)
                {
                    door.unlockDoor();
                }
                else
                {
                    subMessageUI.text = "You need " + door.pointsNeeded + " points to unlock this door";
                    subMessageUI.enabled = true;
                    moveSpeed = 0;
                }
            }
            else
            {
                Door d = collision.collider.gameObject.GetComponent<Door>();
                gameObject.transform.position = new Vector3(d.xPlacement, d.yPlacement, 0);
                playerActionSounds.clip = doorOpen;
                playerActionSounds.Play();
                isInside = d.indoors;
            }
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

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "CarryObject")
        {
            foundCarryObject = true;
            foundObject = collider.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "CarryObject")
        {
            foundCarryObject = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Door")
        {
            subMessageUI.text = "";
            subMessageUI.enabled = false;
        }
    }


}
