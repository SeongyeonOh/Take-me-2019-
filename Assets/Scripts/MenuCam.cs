using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCam : MonoBehaviour
{

    private void Awake()
    {
        if(GameManager.instance.isRestart)
        {
            gameObject.SetActive(false);
            GameManager.instance.isRestart = false;
        }
    }

}
