using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class CheckpointController : MonoBehaviour
{
    private bool checkPointReached;
    private Vector3 originalPos;
    private PlatformerCharacter2D m_Character;
    // Start is called before the first frame update
    void Start()
    {
        checkPointReached = false;
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(checkPointReached)
        {
            Vector3 temp = transform.position;

            if(temp.y < originalPos.y + 7)
                temp.y++;
      
            transform.position = Vector3.Lerp(transform.position, temp, 5);
        }
    }

    void OnTriggerEnter2D(Collider2D o)
    {
        if(o.tag == "Player")
        {
            checkPointReached = true;
            Destroy(gameObject.GetComponent("BoxCollider2D"));
            FindObjectOfType<GameManager>().SetLastCheckpoint(transform.position);
        }
    }
}
