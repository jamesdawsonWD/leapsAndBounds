using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectable : MonoBehaviour
{

    bool gemCollected = false;
    public GameObject gemLabel;

    void Start()
    {
        gemLabel.GetComponent<Text>().text = "";
    }


    private void Update()
    {
        if (FindObjectOfType<GameManager>().collectables > 0)
        {
            gemLabel.GetComponent<Text>().text = FindObjectOfType<GameManager>().collectables.ToString();
        }
    }

    void OnTriggerEnter2D(Collider2D o)
    {
        if (o.tag == "Player" && !gemCollected)
        {
            GetComponent<AudioSource>().Play();
            gemCollected = true;
            GetComponent<SpriteRenderer>().enabled = false;
            FindObjectOfType<GameManager>().addCollectable();
            StartCoroutine(ExampleCoroutine());
            
        }
    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }

}
