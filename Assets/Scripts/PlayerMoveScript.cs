using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{

    public float playerSpeed = 0.5f;
    public int playerMass;
    Vector3 dropDirection;
    Vector3 impulseDirection;
    Vector3 currentPosition;
    Vector3 impulse;
    bool grab = true;
    Vector3 mousePosition;
    


    // Start is called before the first frame update
    void Start()
    {
 
    }

    void FixedUpdate()
    {

        if (Input.GetMouseButtonDown(0))
        {

            currentPosition = Camera.main.WorldToScreenPoint(transform.position);
            mousePosition = Input.mousePosition;

            impulseDirection = (currentPosition - mousePosition).normalized;
            Debug.Log(impulseDirection);

        }

        if (Input.GetMouseButtonDown(0))
        {

            if (grab == true)
            {
                GetComponent<Rigidbody2D>().AddForce(impulseDirection * playerSpeed, ForceMode2D.Impulse);
                grab = false;
            }
        }

        void OnCollisionEnter2D(Collision2D coll)
        {
            if (coll.gameObject.tag == "box")
            {
                grab = true;
            }
        }
    }
}
