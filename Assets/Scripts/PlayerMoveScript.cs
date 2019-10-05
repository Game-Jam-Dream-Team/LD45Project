using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{

    public float playerSpeed = 0.5f;
  //  public int playerMass;
    Vector3 dropDirection;
    Vector3 impulseDirection;
    Vector3 currentPosition;
    Vector3 impulse;

    Vector3 mousePosition;
    ObjectScript grabbedObject;
    bool isStarted = false;
    Rigidbody2D rb;
    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
     //   Debug.Log(transform.position);
        if (Input.GetMouseButtonDown(0))
        {

            currentPosition = Camera.main.WorldToScreenPoint(transform.position);
            mousePosition = Input.mousePosition;

            impulseDirection = (currentPosition - mousePosition).normalized;
            Debug.Log(impulseDirection);

        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!isStarted)
            {
                isStarted = true;
                rb.AddForce(impulseDirection * playerSpeed, ForceMode2D.Impulse);
                return;
            }

            if (grabbedObject != null)
            {


                throwObject(impulseDirection);
                rb.AddForce(impulseDirection * playerSpeed, ForceMode2D.Impulse);

            }
        }
    }

    private void throwObject(Vector3 impulseDirection)
    {
        rb.mass -= grabbedObject.objectMass;
        grabbedObject.transform.SetParent(null);
        grabbedObject.GetComponent<Collider2D>().enabled = false;
        grabbedObject.GetComponent<Rigidbody2D>().velocity = -impulseDirection;
        grabbedObject = null;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "object")
        {

            processObjectCollision(coll.gameObject);

        }
    }

    void processObjectCollision(GameObject obj)
    {
        if (grabbedObject != null)
        {
            return;
        }
        else
        {
            grabbedObject = obj.GetComponent<ObjectScript>();
            obj.transform.SetParent(transform);

            rb.AddForce(-impulseDirection * playerSpeed, ForceMode2D.Impulse);
            rb.mass += grabbedObject.objectMass;
            rb.AddForce(impulseDirection * playerSpeed, ForceMode2D.Impulse);


        }

    }

}
