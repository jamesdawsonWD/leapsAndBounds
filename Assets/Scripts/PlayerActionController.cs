using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionController : MonoBehaviour
{
    public TimeLeap    TL;
    private bool       AtPresentTime;
    public GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        AtPresentTime = true;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (AtPresentTime)
            {
                TL.leapTime(TimeLeap.TimePlane.TWILIGHT);
            }
            else
            {
                TL.leapTime(TimeLeap.TimePlane.NORMAL);
            }
            AtPresentTime = !AtPresentTime;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.bounds.Intersects(GetComponent<BoxCollider2D>().bounds) && (collision.collider.CompareTag("Platform")))
        {
            gameManager.GetComponent<GameManager>().EndGame();
        }

    }
}
