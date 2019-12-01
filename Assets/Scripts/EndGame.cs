using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D o)
    {
        if (o.tag == "Player")
        {
            FindObjectOfType<GameManager>().StartEndGame();
        }
    }
}
