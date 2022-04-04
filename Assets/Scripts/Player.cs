using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player instance;

    public float speed;
    public GameObject[] weapons;
    public bool[] hasWeapons;
    public int iLeveltoLoad;
    public string sLevelToLoad;

    public int health;
    public int coin;
    public int hasGrenades;
    public int weaponIndex = -1;
    public bool useIntegerToLoadLevel = false;


    public int maxHealth;
    public int maxCoin;
    public int maxHasGrenades;

    float hAxis;
    float vAxis;
    bool wDown;
    bool iDown;
    bool spaceDown;

    //공격
    bool fDown;
    bool isFireReady = true;

    bool SDown1;
    bool sDown2;


    bool isJump;
    bool isDodge;
    bool isSwap;

    bool isBorder;

    public AudioSource moveSound;
    public AudioSource coinSound;
    public AudioSource attackSound;


    Vector3 moveVec;
    Vector3 dodgeVec;

    Rigidbody rigid;
    Animator anim;

    GameObject nearObject; // 트리거 된 아이템을 저장하기 위한 변수
    
    [HideInInspector]
    public Weapon equipWeapon;
    public int equipWeaponIndex = -1;
    float fireDelay;



    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Move();
        Turn();
       // Jump();
        Attack();
       // Dodge();
        Interaction();
        Swap();
        Die();
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        //spaceDown = Input.GetKey("space");
        fDown = Input.GetButtonDown("Fire1");
        iDown = Input.GetButtonDown("Interaction");
        SDown1 = Input.GetButtonDown("Swap1");
        sDown2 = Input.GetButtonDown("Swap2");
    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (isDodge)
            moveVec = dodgeVec;

        if (isSwap || !isFireReady)
            moveVec = Vector3.zero;
        /*
               if (wDown)
               {
                   transform.position += moveVec * 0.3f * speed * Time.deltaTime;
               }
               else
               {
                   transform.position += moveVec * speed * Time.deltaTime;
               }
       */

        if (!isBorder)
        {

            // 삼항연산자로 
            transform.position += moveVec * (wDown ? 0.3f : 1f) * speed * Time.deltaTime;

        }

        anim.SetBool("isRun", moveVec != Vector3.zero);

        if (moveVec != Vector3.zero)
        {
            if (!moveSound.isPlaying)
                moveSound.Play();
        }
        else
        {
            if (moveSound.isPlaying)
                moveSound.Stop();
        }

        anim.SetBool("isWalk", wDown);

    }

    void Turn()
    {
        transform.LookAt(transform.position + moveVec);
    }

    void Jump()
    {
        if(spaceDown && moveVec == Vector3.zero && !isJump && !isDodge && !isSwap)
        {
            rigid.AddForce(Vector3.up * 15, ForceMode.Impulse);
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");
            isJump = true;
        }
    }

    void Attack()
    {
        if (equipWeapon == null)
            return;

        fireDelay += Time.deltaTime;
        isFireReady = equipWeapon.rate < fireDelay;

        if (fDown)
            // Debug.Log(fDown.ToString() + isFireReady.ToString() + isDodge.ToString() + isSwap.ToString());
        if(fDown && isFireReady && !isDodge && !isSwap)
        {
            attackSound.Play();

            equipWeapon.Use();

            //anim.SetTrigger(equipWeapon.type == Weapon.Type.Melee ? "doSwing" : "doShot");
            anim.SetTrigger("doSwing");
            fireDelay = 0;
        } 
    }

    void Dodge()
    {
        if (spaceDown && moveVec != Vector3.zero && !isJump && !isDodge && !isSwap)
        {
            dodgeVec = moveVec;
            speed *= 2;
            anim.SetTrigger("doDodge");
            isDodge = true;

            //함수 시간차 주기위해
            Invoke("DdogeOut", 0.5f);
        }
    }

    void DdogeOut()
    {
        speed *= 0.5f;
        isDodge = false;
    }

    void Swap()
    {
        if (SDown1)
            // Debug.Log(hasWeapons[0].ToString() + equipWeaponIndex.ToString());
        if (SDown1 && (!hasWeapons[0] || equipWeaponIndex == 0))
            return;
        if (sDown2 && (!hasWeapons[1] || equipWeaponIndex == 1))
            return;
        weaponIndex = -1;
        if (SDown1) weaponIndex = 0;
        if (sDown2) weaponIndex = 1;

        if((SDown1 || sDown2) && !isJump && !isDodge)
        {
            if(equipWeapon != null)
                equipWeapon.gameObject.SetActive(false);

            equipWeaponIndex = weaponIndex;
            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
            equipWeapon.gameObject.SetActive(true);

            anim.SetTrigger("doSwap");

            isSwap = true;

            Invoke("SwapOut", 0.4f);
        }
    }

    void SwapOut()
    {
        isSwap = false;
    }

    void Interaction()
    {
        if(iDown && nearObject != null && !isJump && !isDodge)
        {
            if(nearObject.tag == "Weapon")
            {
                Item item = nearObject.GetComponent<Item>();
                int weaponIndex = item.value;
                hasWeapons[weaponIndex] = true;

                Destroy(nearObject);
            }
        }
    }

    void FreezeRotation()
    {
        
        rigid.angularVelocity = Vector3.zero;
    }

    void StopToWall()
    {
        // Debug.DrawRay(transform.position, transform.forward * 3, Color.green);
        isBorder = Physics.Raycast(transform.position, transform.forward, 3, LayerMask.GetMask("Wall"));
       
    }

    void FixedUpdate()
    {
        FreezeRotation();
        StopToWall();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            anim.SetBool("isJump", false);
            isJump = false;
        }    
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Item")
        {
            Item item = other.GetComponent<Item>();
            switch (item.type)
            {
                case Item.Type.Heart:
                    health += item.value;
                    if (health > maxHealth)
                        health = maxHealth;
                    break;

                case Item.Type.Coin:
                    coin += item.value;
                    coinSound.Play();
                    if (coin > maxCoin)
                        coin = maxCoin;
                    // Debug.Log("민규가 코인 쳐먹음");
                    break;

                case Item.Type.Grenade:
                    hasGrenades += item.value;
                    if (hasGrenades > maxHasGrenades)
                        hasGrenades = maxHasGrenades;
                    break;

            }
            Destroy(other.gameObject);
        }
        else if (other.tag == "EnemyBullet")
        {
            Bullet enemyBullet = other.GetComponent<Bullet>();
            health -= enemyBullet.damage;
            // Debug.Log("충돌");
            StartCoroutine(OnDamage());
        }
    }

    IEnumerator OnDamage()
    {

        yield return null;
    }


    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Weapon")
            nearObject = other.gameObject;
//        Debug.Log(nearObject.name);
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Weapon")
            nearObject = null;
    }
    public void Die()
    {
        if (health <= 0)
        {
            
            if (useIntegerToLoadLevel)
            {
                SceneManager.LoadScene(iLeveltoLoad);
            }
            else
            {
                if (sLevelToLoad != "ending")
                {
                    SceneManager.LoadScene(sLevelToLoad);
                }

                else
                {
                    SceneManager.LoadScene(sLevelToLoad);
                    FindObjectOfType<GameManager>().isStart = false;
                }

            }
            

            
            gameObject.SetActive(false);
            GameManager.instance.isStart = false;

            // Debug.Log("피격");
            // Debug.Log(health);

            

        }
    }


}
