using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHoverEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       // GetComponent<Text>().material.color = Color.black;
    }

    void OnMouseEnter()
    {
       // GetComponent<Text>().material.color = Color.red;
    }

    void OnMouseExit()
    {
      //  GetComponent<Text>().material.color = Color.black;
    }
}
