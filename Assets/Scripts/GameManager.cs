using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets._2D;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;

    public int deaths = 0;
    public int collectables = 0;

    public Image credits;
    public bool reset = false;
    public float restartDelay = 1f;
    private Vector3 lastCheckpoint;

    public PlatformerCharacter2D m_Character;

    void Awake()
    {
        lastCheckpoint = new Vector3(-50, 14, 0);
    }
    public void EndGame ()
    {
        LoadLastCheckpoint();
    }

    void FixedUpdate()
    {
        if (reset) reset = false;
    }

    public void SetLastCheckpoint(Vector3 pos)
    {
        pos.y = pos.y + 30;
        lastCheckpoint = pos;
    }

    public void LoadLastCheckpoint()
    {
        deaths = deaths++;
        reset = true;
        GameObject.FindWithTag("Player").transform.position = lastCheckpoint;


        m_Character.Move(0, false, false);

        GameObject.FindWithTag("Player").GetComponent<PlatformerCharacter2D>().enabled = false;
        GameObject.FindWithTag("Player").GetComponent<Platformer2DUserControl>().enabled = false;
        StartCoroutine("WaitForGround");
    }

    IEnumerator WaitForGround()
    {
        yield return new WaitForSeconds(0.6f);
        GameObject.FindWithTag("Player").GetComponent<PlatformerCharacter2D>().enabled = true;
        GameObject.FindWithTag("Player").GetComponent<Platformer2DUserControl>().enabled = true;
        StopCoroutine("WaitForGround");
    }

    public void addCollectable()
    {
        collectables++;
    }

    public void removeCollectable()
    {
        collectables--;
    }

    public void StartEndGame()
    {
        Color color = this.credits.color;

        color.a = 1;
        this.credits.color = color;
        GameObject.Find("sky").GetComponent<CameraFollow>().followX = false;
        GameObject.Find("moon").GetComponent<CameraFollow>().followX = false;

        GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>().followX = false;
        GameObject.FindWithTag("Player").GetComponent<UnityStandardAssets._2D.Platformer2DUserControl>().enabled = false;
        GameObject.FindWithTag("Player").GetComponent<PlayerActionController>().enabled = false;
        StartCoroutine(Credits());

    }

    IEnumerator Credits()
    {

        yield return new WaitForSeconds(5);
        Application.LoadLevel(0);

    }
}
