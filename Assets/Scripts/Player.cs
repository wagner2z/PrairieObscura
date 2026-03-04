using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class Player : MonoBehaviour
{
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
    const int maxWeaponPos = 8;
    const int maxGunUpgrade = 3;
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
    public RuntimeAnimatorController handgun2;
    public RuntimeAnimatorController rifle2;
    public RuntimeAnimatorController shotgun2;
    public RuntimeAnimatorController handgun3;
    public RuntimeAnimatorController rifle3;
    public RuntimeAnimatorController shotgun3;
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
        availableGuns[0] = new GunTypes("Revolver", 1, 5, 6, 0, 1, 3f, 1, true, canvas.transform.Find("SelectedGun/RevolverUI (1)"), handgun1, true);
        availableGuns[1] = new GunTypes("Revolver", 2, 7, 6, 0, 1, 2.5f, 1, false, canvas.transform.Find("SelectedGun/RevolverUI (2)"), handgun2, true);
        availableGuns[2] = new GunTypes("Revolver", 3, 9, 10, 0, 1, 4f, 1, false, canvas.transform.Find("SelectedGun/RevolverUI (3)"), handgun3, true);
        availableGuns[3] = new GunTypes("Bolt Rifle", 1, 8, 3, 1, 1, 2f, 1, false, canvas.transform.Find("SelectedGun/BoltRifleUI (1)"), rifle1, false);
        availableGuns[4] = new GunTypes("Bolt Rifle", 2, 8, 3, 1, 1, 2f, 3, false, canvas.transform.Find("SelectedGun/BoltRifleUI (2)"), rifle2, false);
        availableGuns[5] = new GunTypes("Bolt Rifle", 3, 12, 3, 1, 1, 2f, 3, false, canvas.transform.Find("SelectedGun/BoltRifleUI (3)"), rifle3, false);
        availableGuns[6] = new GunTypes("Double Barrel Shotgun", 1, 12, 2, 2, 1, 0.82f, 1, false, canvas.transform.Find("SelectedGun/DoubleBarrelUI (1)"), shotgun1, false);
        availableGuns[7] = new GunTypes("Double Barrel Shotgun", 2, 15, 2, 2, 1, 0.82f, 1, false, canvas.transform.Find("SelectedGun/DoubleBarrelUI (2)"), shotgun2, false);
        availableGuns[8] = new GunTypes("Double Barrel Shotgun", 3, 15, 4, 2, 1, 1.32f, 1, false, canvas.transform.Find("SelectedGun/DoubleBarrelUI (3)"), shotgun3, false);


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
        moveDirection = new Vector3(0, 0, 0);
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

            if (Input.GetKeyDown(ControlAssignment.pickUpOrDrop()) && !firingPosition && !isCarryingObject && tempWaitTime <= 0
                && foundCarryObject)
            {
                foundObject.GetComponent<Renderer>().enabled = false;
                foundObject.transform.position = new Vector3(offScreenX, offScreenY, 0);
                
                setCarryableObject();
            }

            if (Input.GetKeyDown(ControlAssignment.pickUpOrDrop()) && isCarryingObject && !firingPosition && tempWaitTime <= 0)
            {
                dropCarryableObject();
            }

            if (Input.GetKey(ControlAssignment.playerFirePosition()) && isEquipped && !isReloading)
            {
                Cursor.visible = false;
                shoot_cursor.GetComponent<Renderer>().enabled = true;
                firingPosition = true;
                shoot_cursor.transform.position = mouseWorldPos;
                anim.SetBool("Shooting", true);
                //sRenderer.sprite = shootSprite;
                if (Input.GetKeyDown(ControlAssignment.playerShoot()) && currentGun.getCurrentGunAmmo() > 0)
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
            if (isEquipped && !isCarryingObject && Input.GetKeyDown(ControlAssignment.reloadGun()))
            {
                reloadGun();
            }
            if (ControlAssignment.getMoveByWorldAxis())
            {
                if (Input.GetKey(ControlAssignment.characterMoveBack()))
                {
                    moveDirection += new Vector3(0, -1, 0);
                }
                if (Input.GetKey(ControlAssignment.characterMoveForward()))
                {
                    moveDirection += new Vector3(0, 1, 0);
                }
                if (Input.GetKey(ControlAssignment.characterMoveLeft()))
                {
                    moveDirection += new Vector3(-1, 0, 0);
                }
                if (Input.GetKey(ControlAssignment.characterMoveRight()))
                {
                    moveDirection += new Vector3(1, 0, 0);
                }
            }
            else
            {
                if (Input.GetKey(ControlAssignment.characterMoveBack()))
                {
                    moveDirection = -transform.up;
                }
                if (Input.GetKey(ControlAssignment.characterMoveForward()))
                {
                    moveDirection = transform.up;
                }
                if (Input.GetKey(ControlAssignment.characterMoveLeft()))
                {
                    moveDirection = -transform.right;
                }
                if (Input.GetKey(ControlAssignment.characterMoveRight()))
                {
                    moveDirection = transform.right;
                }
            }

            if (Input.GetKey(ControlAssignment.characterMoveBack()))
            {
                // Calculate the backward direction based on the character's current forward vector
                // Multiplying by -1 reverses the direction
                if (Input.GetKey(ControlAssignment.characterDash()) && currentStamina > 0
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
                //moveDirection = -transform.up;
                

                // Move the character in the backward direction
                // Time.deltaTime ensures frame-rate independent movement
                //transform.Translate(backwardDirection * moveSpeed * Time.deltaTime, Space.World);
            }
            if (Input.GetKey(ControlAssignment.characterMoveForward()))
            {
                // Calculate the backward direction based on the character's current forward vector
                // Multiplying by -1 reverses the direction
                if (Input.GetKey(ControlAssignment.characterDash()) && currentStamina > 0
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
                //moveDirection = transform.up;
                

                // Move the character in the backward direction
                // Time.deltaTime ensures frame-rate independent movement
                //transform.Translate(forwardDirection * moveSpeed * Time.deltaTime, Space.World);
            }
            if (Input.GetKey(ControlAssignment.characterMoveLeft()))
            {
                // Calculate the backward direction based on the character's current forward vector
                // Multiplying by -1 reverses the direction
                if (Input.GetKey(ControlAssignment.characterDash()) && currentStamina > 0
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
                //moveDirection = -transform.right;
                

                // Move the character in the backward direction
                // Time.deltaTime ensures frame-rate independent movement
                //transform.Translate(leftDirection * moveSpeed * Time.deltaTime, Space.World);
            }
            if (Input.GetKey(ControlAssignment.characterMoveRight()))
            {
                // Calculate the backward direction based on the character's current forward vector
                // Multiplying by -1 reverses the direction
                if (Input.GetKey(ControlAssignment.characterDash()) && currentStamina > 0
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
                //moveDirection = transform.right;
                

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


            if (!Input.GetKey(ControlAssignment.characterDash()))
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
        shoot_cursor.GetComponent<ShootCursor>().anim.Play("remove_cursor", 0, shoot_cursor.GetComponent<ShootCursor>().getLockOnTime());
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

    public List<GunTypes> getNewLockedGuns()
    {
        List<GunTypes> lockedGuns = new List<GunTypes>();
        //int temp = 0;
        for (int i = 0; i < maxWeaponPos + 1; i++)
        {
            if (!availableGuns[i].getGunAvailable() && availableGuns[i].getGunUpgrade() == 1)
            {
                lockedGuns.Add(availableGuns[i]);
                //temp++;
            }
        }

        return lockedGuns;
    }

    public List<GunTypes> getUpgradeLockedGuns()
    {
        List<GunTypes> lockedGuns = new List<GunTypes>();
        //int temp = 0;
        for (int i = 0; i < maxWeaponPos + 1; i++)
        {
            if (availableGuns[i].getGunAvailable() && availableGuns[i].getGunUpgrade() < maxGunUpgrade)
            {
                int gunUpgradeLevel = availableGuns[i].getGunUpgrade() + 1;
                string gunName = availableGuns[i].getGunName();
                for (int j = 0; j < maxWeaponPos + 1; j++)
                {
                    if (!availableGuns[j].getGunAvailable() && availableGuns[j].getGunName() == gunName && availableGuns[j].getGunUpgrade() == gunUpgradeLevel)
                    {
                        lockedGuns.Add(availableGuns[j]);
                        break;
                    }
                }
                //temp++;
            }
        }

        return lockedGuns;
    }

    public void unlockGun(string gunName)
    {
        for (int i = 0; i < maxWeaponPos + 1; i++)
        {
            if (availableGuns[i].getGunName() == gunName && availableGuns[i].getGunUpgrade() == 1)
            {
                availableGuns[i].setGunAvailable(true);
                break;
            }
        }
    }

    public void upgradeGun(string gunName, int gunUpgrade)
    {
        for (int i = 0; i < maxWeaponPos + 1; i++)
        {
            if (availableGuns[i].getGunName() == gunName && availableGuns[i].getGunUpgrade() == gunUpgrade)
            {
                availableGuns[i].setGunAvailable(true);
            }
            else if (availableGuns[i].getGunName() == gunName && availableGuns[i].getGunUpgrade() == gunUpgrade - 1)
            {
                availableGuns[i].setGunAvailable(false);
            }
        }
    }

    public void setCarryableObject()
    {
        carryable.GetComponent<Renderer>().enabled = true;
        unequipWeapon();
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
    public void unequipWeapon()
    {
        currentWeaponPos = -1;
        isEquipped = false;
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
            && Input.GetKey(ControlAssignment.playerPush())
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
