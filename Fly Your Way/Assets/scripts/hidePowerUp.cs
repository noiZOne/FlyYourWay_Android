using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hidePowerUp : MonoBehaviour
{
    

    //Hide the PowerUp after collect
    void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.tag == "Player")
        {
            GetComponent<Renderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            

        }

    }
  
        
    
}
        