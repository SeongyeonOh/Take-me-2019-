using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public int maxHealth;
    public int curHealth;
   // public Transform target;
    public BoxCollider meleeArea;
    public bool isAttack;


    Transform target;

    Rigidbody rigid;
    BoxCollider boxCollider;
    Material mat;
    NavMeshAgent nav;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponentInChildren<MeshRenderer>().material;
        nav = GetComponent<NavMeshAgent>();
        target = FindObjectOfType<Player>().transform;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
        nav.SetDestination(target.position);
    }


    void FreezeVelocity()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }



    void FixedUpdate()
    {

        FreezeVelocity();
        
    }



    void OnTriggerEnter(Collider other)
    {
     if(other.tag == "Melee")
        {
            Weapon weapon = other.GetComponent<Weapon>();
            curHealth -= weapon.damage;

            StartCoroutine(OnDamage());
            //Debug.Log("Melee" + curHealth);
        }
        else if(other.tag == "Bullet")
        {
            Bullet bullet = other.GetComponent<Bullet>();
            curHealth -= bullet.damage;
            Debug.Log("피격했습니다.");
            Destroy(other.gameObject);
            StartCoroutine(OnDamage());
            //Debug.Log("Bullet" + curHealth);
        }
    }


    IEnumerator OnDamage()
    {
        mat.color = Color.red;
        yield return new WaitForSeconds(0.02f);

        if(curHealth > 0)
        {
            mat.color = Color.white;
        }
        else
        {
            mat.color = Color.gray;
            gameObject.layer = 13;
            Destroy(gameObject);
        }
    }



}
