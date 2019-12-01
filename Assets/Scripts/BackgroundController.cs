using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public GameObject[] layers;     // Each layer of the background
    private Camera mainCamera;    // Main Camera
    private Vector2 screenBounds;   // Bounds of the screen
    public float overlap;    // Used for preventing gaps inbetween sets of children
    private float[] parallaxScales; // The proportion of the camera's movement to move the backgrounds by
    private Vector3 previousCamPos; // The position of the camera in the previous frame
    private float startPosition, length, offset;

    /*
     * Called once
     */
    void Start()
    {
        offset = 20;
        startPosition = transform.position.x;
        mainCamera = gameObject.GetComponent<Camera>();
        previousCamPos = mainCamera.transform.position; // Last camera position
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));  //  The bigger the screen, the more child backgrounds that need to be created
        foreach (GameObject obj in layers)
        {
            LoadChildObjects(obj);
        }
        parallaxScales = new float[layers.Length];
        for (int i = 0; i < layers.Length; i++)
        {
            parallaxScales[i] = layers[i].transform.position.z * -1; //  Works out what the parallex effect should be for each layer
            //parallaxScales[i] = 0;
        }
    }

    /*
     *  Create sets of children of the background layers and position them side by side
     */
    void LoadChildObjects(GameObject obj)
    {
        float objectWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x - overlap;   //Minus overlap so that the sets of children cross over a little bit to prevent gaps
        int childrenNeeded = (int)Mathf.Ceil(screenBounds.x * 2 / objectWidth);  // How many sets of each layer will be created
        GameObject clone = Instantiate(obj) as GameObject;
        for (int i = 0; i <= childrenNeeded; i++)
        {
            GameObject c = Instantiate(clone) as GameObject;
            c.transform.SetParent(obj.transform);
            // Position each set appropriately so they are next to each other
            c.transform.position = new Vector3(objectWidth * i, obj.transform.position.y, obj.transform.position.z);
            c.name = obj.name + i;
        }
        Destroy(clone);
        Destroy(obj.GetComponent<SpriteRenderer>());
    }

    void Update()
    {

    }

    /*
     * Called each frame after Update()
     * Reposition the child objects as camera moves
     */
    void LateUpdate()
    {
        //  
        for (int i = 0; i < layers.Length; i++)
        {
            
            GameObject obj = layers[i];
            Transform[] children = obj.GetComponentsInChildren<Transform>();
            if (children.Length > 1)
            {
                //Work out if the camera's edge is going past the bounds of either the first or last child. If it is true for one, the other needs to move
                GameObject firstChild = children[1].gameObject;
                GameObject lastChild = children[children.Length - 1].gameObject;
                float halfObjectWidth = lastChild.GetComponent<SpriteRenderer>().bounds.extents.x - overlap;
                if (transform.position.x + screenBounds.x > lastChild.transform.position.x + halfObjectWidth)
                {
                    //Camera view is crossing past the half way point of the last child, so reposition first child
                    firstChild.transform.SetAsLastSibling();
                    firstChild.transform.position = new Vector3((lastChild.transform.position.x + halfObjectWidth * 2), lastChild.transform.position.y, lastChild.transform.position.z);
                }
                else if (transform.position.x - screenBounds.x < firstChild.transform.position.x - halfObjectWidth)
                {
                    //Camera view is crossing past the half way point of the first child, so reposition last child
                    lastChild.transform.SetAsFirstSibling();
                    lastChild.transform.position = new Vector3((firstChild.transform.position.x - halfObjectWidth * 2), firstChild.transform.position.y, firstChild.transform.position.z);
                }
            }
        }
    }
}
