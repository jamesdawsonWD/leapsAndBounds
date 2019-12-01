using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public Vector3 velocity;
    public Vector3 destination;
    public float speed = 10f;
    public  bool resetOnDeath       = true;
    public  bool loop               = false;
    public  bool moveWhenTouched    = false;
    public  bool justVelocity        = false; 
    private bool hit = false;
    private Vector3 originalPosition;
    private Vector3 originalDestination;

    // Start is called before the first frame update
    void Start()
    {

        originalPosition = transform.position;
        originalDestination = destination;

    }

    // Update is called once per frame
    void Update()
    {

        if(justVelocity)
        {
            transform.position += (velocity * Time.deltaTime);
            return;
        }

        float distance = Vector3.Distance(destination, transform.position);
        if (distance > 1)
        {

            if (resetOnDeath && FindObjectOfType<GameManager>().reset)
            {
                transform.position = originalPosition;
                hit = false;
            }
            if (hit && moveWhenTouched || !hit && !moveWhenTouched)
            {
                if (Vector3.Distance(destination, new Vector3(0, 0, 0)) < 1)
                {
                    transform.position += (velocity * Time.deltaTime);
                    return;
                }
                else
                {
                    Vector3 direction = (destination - transform.position).normalized;
                    velocity = direction * speed;
                    transform.position += (velocity * Time.deltaTime);
                }
            }

        }
        else
        {
            if (loop)
            {
                destination = Vector3.Distance(originalDestination, transform.position) < 1
                    ? originalPosition 
                    : originalDestination;

            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hit = true;
            collision.gameObject.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
