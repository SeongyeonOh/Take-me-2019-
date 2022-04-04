using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public int iLeveltoLoad;
    public string sLevelToLoad;
   
    public bool useIntegerToLoadLevel = false;
    public GameObject object1;
    public GameObject object2;


    public AudioSource nextSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            nextSound.Play();

            Player.instance.coin = 0;
            Player.instance.equipWeaponIndex = -1;
            Player.instance.weaponIndex = -1;
            Player.instance.hasWeapons[0] = false;
            Player.instance.hasWeapons[1] = false;
            Player.instance.equipWeapon = null;
            Player.instance.health = 10;

            //if (Player.instance.gameObject.activeSelf)
            //{
            // Debug.Log(Player.instance.weapons[0]);
            Player.instance.weapons[0].SetActive(false);
            Player.instance.weapons[1].SetActive(false);

            Player.instance.gameObject.SetActive(true);

            GameManager.instance.isRestart = true;
            GameManager.instance.playTime = 0;
            GameManager.instance.isStart = true;
            //}
            SceneManager.LoadScene("01spring");
        }
    }
}
