using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerPos : MonoBehaviour
{
    // Start is called before the first frame update

    private void Awake()
    {
        if(Player.instance != null)
        {
            Player.instance.transform.position = transform.position;
        }
        else
        {
            Debug.Log("Player is missing");
        }
    }
}
