using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type {Melee, Range};
    public Type type;
    public int damage;
    public float rate;
    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;
    public Transform bulletPos;
    public GameObject bullet;
    public Transform bulletCasePos;
    public GameObject bulletCase;

    public void Use()
    {
        if(type == Type.Melee)
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        }
        else if(type == Type.Range)
        {
            StartCoroutine("Shot");
        }
    }

    IEnumerator Swing()
    {
        //1
        //yield 결과를 전달하는 키워드
        yield return new WaitForSeconds(0.5f); // 0.1초대기
        meleeArea.enabled = true;
        trailEffect.enabled = true;
        //2
        yield return new WaitForSeconds(0.4f); // 1프레임 대기
        meleeArea.enabled = false;
        //3
        yield return new WaitForSeconds(0.3f); // 1프레임 대기
        trailEffect.enabled = false;
    }

    //Use() 메인루틴 -> Swing() 서브루틴 -> Use() 메인루틴
    //Use() 메인루틴 + Swing() 코루틴 (Co-Op)


    IEnumerator Shot()
    {
        // 발싸
        GameObject intantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 50;

        yield return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
