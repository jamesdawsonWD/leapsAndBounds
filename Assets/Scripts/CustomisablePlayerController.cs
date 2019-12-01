using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class CustomisablePlayerController : MonoBehaviour
{


    private Animator m_Anim;

    // Start is called before the first frame update
    void Awake()
    {
        m_Anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("DeathZone"))
        {
            PlayerDeath();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BearTrap") && gameObject.tag == "Player")
        {
            collision.GetComponent<Animator>().SetBool("IsActivated", true);
            PlayerDeath();
        }

        if (collision.CompareTag("Ghost") && gameObject.tag == "Player")
        {
            PlayerDeath();
        }
    }

    public void PlayerDeath()
    {
        m_Anim.SetFloat("Health", 0f);
        FindObjectOfType<GameManager>().EndGame();
        GetComponent<Platformer2DUserControl>().enabled = false ;

    }
}
