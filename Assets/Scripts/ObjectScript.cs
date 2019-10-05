using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    public float objectMass = 5f;
    public float tilt = 1f;

    
    // Start is called before the first frame update
    void Start()
    {
        tilt =  Random.Range(-20, 20) / 5;
    }

    // Update is called once per frame
    void Update()
    {
        objectRotate();
    }

    void objectRotate()
    {
        transform.Rotate(Vector3.forward * tilt);
    }
}
