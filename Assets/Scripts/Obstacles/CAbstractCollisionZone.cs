using System;
using UnityEngine;

public abstract class CAbstractCollisionZone : MonoBehaviour
{
    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            ProcessPlayerCollision();
        }
    }

    protected virtual void ProcessPlayerCollision()
    {
        // override me
    }
}
