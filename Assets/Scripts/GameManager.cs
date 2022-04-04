using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject menuCam;
    public GameObject mainCam;
    public GameObject howCam;
    public GameObject gameOverCam;
    public Player player;
    public bool isStart, isRestart = false;
    //public int stage;
    public string stage;
    public float playTime;
    public int coin;


    public GameObject menuPanel;
    public GameObject gamePanel;
    public GameObject howPanel;
    public GameObject gameOverPanel;
    public Text coinTxt;
    public Text stageTxt;
    public Text playTimeTxt;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    public void GameStart()
    {
        menuCam.SetActive(false);
        howCam.SetActive(false);
        mainCam.SetActive(true);
        gameOverCam.SetActive(false);

        menuPanel.SetActive(false);
        howPanel.SetActive(false);
        gamePanel.SetActive(true);
        gameOverPanel.SetActive(false);

        // player.gameObject.SetActive(true);
        Player.instance.gameObject.SetActive(true);
        isStart = true;
    }

    public void HowToPlay()
    {
        menuCam.SetActive(false);
        howCam.SetActive(true);
        mainCam.SetActive(false);
        gameOverCam.SetActive(false);

        menuPanel.SetActive(false);
        howPanel.SetActive(true);
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void ReturnToMenu()
    {
        menuCam.SetActive(true);
        howCam.SetActive(false);
        mainCam.SetActive(false);
        gameOverCam.SetActive(false);

        menuPanel.SetActive(true);
        howPanel.SetActive(false);
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);

        isStart = false;
    }

    
    public void GameOver()
    {
        
        menuCam.SetActive(false);
        howCam.SetActive(false);
        mainCam.SetActive(false);
        gameOverCam.SetActive(true);

        menuPanel.SetActive(false);
        howPanel.SetActive(false);
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);

        isStart = false;
        
    }
    

    // Update is called once per frame
    void Update()
    {
        if (isStart)
            playTime += Time.deltaTime;
    }

    void LateUpdate()
    {
        coinTxt.text = string.Format("{0:n0}", player.coin);
        //coinTxt.text =  " " + player.coin;
        //stageTxt.text = "STAGE " + stage;
        stageTxt.text = SceneManager.GetActiveScene().name;
        playTimeTxt.text = " " + playTime;


        int hour = (int)(playTime / 3600);
        int min = (int)((playTime - hour * 3600) / 60);
        int sec = (int)(playTime % 60);
        //playTimeTxt.text = string.Format("{0:00", hour) + ":" + string.Format("{0:00", min) + ":" + string.Format("{0:00", sec);

        playTimeTxt.text = string.Format("{0:D2}:{1:D2}:{2:D2}", hour, min, sec);
    }
}