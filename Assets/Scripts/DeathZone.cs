using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D o)
    {
        if (o.tag == "Player")
        {
            FindObjectOfType<GameManager>().LoadLastCheckpoint();
        }
    }
}
