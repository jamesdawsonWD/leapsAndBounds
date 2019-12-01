using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        Application.LoadLevel(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void startGameAnimation()
    {

        GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>().followX = false;
        StartCoroutine(Order());
         GameObject.Find("sky").GetComponent<CameraFollow>().followX = false;
        GameObject.Find("moon").GetComponent<CameraFollow>().followX = false;

    }


    IEnumerator Order()
    {
        yield return new WaitForSeconds(5);
        StartGame();
    }
}
