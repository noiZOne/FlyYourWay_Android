using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy : MonoBehaviour
{


    // Zerstören des Kanisters beim Treffen mit dem Flugzeug
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(gameObject);
         
        }

    }
  
        
    
}
        