using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipIdle : MonoBehaviour
{
    GameObject ship;
    // Start is called before the first frame update
    void Start()
    {
     //   ship = GameObject.FindGameObjectWithTag("ship");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, 1, Space.Self);
    }
}
