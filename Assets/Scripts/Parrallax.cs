using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parrallax : MonoBehaviour
{

    private float length, startPosition, offset;
    public GameObject camera;
    public float parallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x; //Get the sprite renderer's x position
    }

    // Update is called once per frame
    void Update()
    {
        //Added an offset to keep the player roundabouts in the middle of all the sets for each layer
        float temp = ((camera.transform.position.x) * (1 - parallaxEffect)); //relative to the camera
        float dist = ((camera.transform.position.x) * parallaxEffect); //Multiplier allows you to create depth without z axis

        transform.position = new Vector3(startPosition + dist, transform.position.y, 0);

        //This is what makes the infinite scrolling effect. If you reach the end it moves them
        if (temp > startPosition + length - 15) 
            startPosition += length;
        else if (temp < startPosition - length + 10) 
            startPosition -= length;
    }
}
