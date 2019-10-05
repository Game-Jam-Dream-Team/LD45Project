using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstacleScript : MonoBehaviour
{
    float tilt;
    // Start is called before the first frame update
    void Start()
    {
        tilt = Random.Range(-10, 10) / 5;
        if (System.Math.Abs(tilt) < 0.2)
        {
            tilt = 0.2f;
        }
    }

    void Update()
    {
        objectRotate();
    }

    void objectRotate()
    {
        transform.Rotate(Vector3.forward * tilt);
    }
}
