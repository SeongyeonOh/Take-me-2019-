using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int damage;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 3f);
        }

    }

    /*
    void OnTriggerEnter(Collider other)
    {
     if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                player.Die();
            }
        }   
    }
*/

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
