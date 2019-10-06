using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    public float objectMass = 1f;
    public float objectVelocityK;
    public float tilt;

    
    // Start is called before the first frame update
    void Start()
    {
        tilt =  Random.Range(-30, 30) / 5;
        if (tilt == 0)
        {
            tilt = 1;
        }
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
