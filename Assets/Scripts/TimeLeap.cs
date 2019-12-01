using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLeap : MonoBehaviour
{

    public enum TimePlane { NORMAL, TWILIGHT };
    public CanvasGroup myCG;
    public CanvasGroup twilightZone;
    private bool flash = false;

    public AudioClip normalMusic;
    public AudioClip twilightMusic;

    private AudioSource gameAudio;

    private float twilightClipLength;
    private float normalClipLenght;

    private float twilightTime;
    private float normalTime;
    
    private SpriteMask mask;
    private int playerLayer;
    private int bgLayer1;
    private int bgLayer2;

    public GameObject[] ghosts;
    public TimePlane currentTimePlane;


    void Start()
    {
        mask        = gameObject.GetComponent<SpriteMask>();
        gameAudio   = GameObject.Find("GameManager").GetComponent<AudioSource>();
        playerLayer = LayerMask.NameToLayer("Player");
        bgLayer1    = LayerMask.NameToLayer("Background1");
        bgLayer2    = LayerMask.NameToLayer("Background2");
        currentTimePlane = TimePlane.NORMAL;

        myCG.alpha          = 0;
        twilightZone.alpha  = 0;
        twilightClipLength  = twilightMusic.length;
        normalClipLenght    = normalMusic.length;

        Debug.Log(twilightClipLength);
        Debug.Log(normalClipLenght);

        foreach (GameObject ghost in ghosts)
        {
            ghost.SetActive(false);
        }

    }


    void Update()
    {
        if (flash)
        {
            myCG.alpha = myCG.alpha - 0.1f;
            if (myCG.alpha <= 0)
            {
                myCG.alpha = 0;
                flash = false;
            }
        }
    }
    


    /** Leap to a specific plane of time
     *  Disables / enables the sprite mask & changes the collision matrix 
     *  in relation to the player to ignore the different level Layes
     *  
     * @Param tp - the timeplane we want to leap to
    */

    public void leapTime(TimePlane tp)
    {
        flash = true;
        myCG.alpha = 1;
        switch (tp)
        {
            case TimePlane.TWILIGHT:
                currentTimePlane = TimePlane.TWILIGHT;
                displayMask();
                twilightZone.alpha = 1;
                normalTime = gameAudio.time;
                gameAudio.clip = twilightMusic;
                gameAudio.time = twilightTime;
                Physics2D.IgnoreLayerCollision(bgLayer2, playerLayer, false);
                Physics2D.IgnoreLayerCollision(bgLayer1, playerLayer, true);
                foreach (GameObject ghost in ghosts)
                {
                    ghost.SetActive(true);
                }
                break;
            case TimePlane.NORMAL:
                currentTimePlane = TimePlane.NORMAL;
                hideMask();
                twilightZone.alpha = 0;
                twilightTime = gameAudio.time;
                gameAudio.clip = normalMusic;
                gameAudio.time = normalTime;
                Physics2D.IgnoreLayerCollision(bgLayer1, playerLayer, false);
                Physics2D.IgnoreLayerCollision(bgLayer2, playerLayer, true);
                foreach (GameObject ghost in ghosts)
                {
                    ghost.SetActive(false);
                }
                break;
        }
        gameAudio.Play();
    }
    public void displayMask()
    {
        mask.enabled = true;
    }

    public void hideMask()
    {
        mask.enabled = false;
    }


}
