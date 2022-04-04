using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Nextstage : MonoBehaviour
{
    public int iLeveltoLoad;
    public string sLevelToLoad;
    int Coin;
    public bool useIntegerToLoadLevel = false;
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;
    public GameObject object4;

    public AudioSource nextSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider collision)
    {
        GameObject collisionGameObject = collision.gameObject;
        Coin = Player.instance.coin;


        if (collisionGameObject.name == "Player" && Coin >= 3)
        {
            nextSound.Play();

            Player.instance.coin = 0;
            Player.instance.equipWeaponIndex = -1;
            Player.instance.weaponIndex = -1;
            Player.instance.hasWeapons[0] = false;
            Player.instance.hasWeapons[1] = false;
            Player.instance.equipWeapon = null;

            //if (Player.instance.gameObject.activeSelf)
            //{
            // Debug.Log(Player.instance.weapons[0]);
            Player.instance.weapons[0].SetActive(false);
            Player.instance.weapons[1].SetActive(false);
            // object1.GetComponent<BoxCollider>().enabled = false;
            // object2.GetComponent<BoxCollider>().enabled = false;
            LoadScene();
        }
    }

    void LoadScene()
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
    }
}
