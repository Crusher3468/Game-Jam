using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string hitTagName = string.Empty;
    public CollisionEvent onEnter;
    public CollisionEvent onExit;
    public CollisionEvent onStay;

    public delegate void CollisionEvent(GameObject other);

    private void OnCollisionEnter(Collision collision)
    {
        if (hitTagName == string.Empty || collision.gameObject.CompareTag(hitTagName))
        {
            onEnter.Invoke(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (hitTagName == string.Empty || collision.gameObject.CompareTag(hitTagName))
        {
            onExit.Invoke(collision.gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (hitTagName == string.Empty || collision.gameObject.CompareTag(hitTagName))
        {
            onStay.Invoke(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hitTagName == string.Empty || other.gameObject.CompareTag(hitTagName))
        {
            onEnter.Invoke(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (hitTagName == string.Empty || other.gameObject.CompareTag(hitTagName))
        {
            onExit.Invoke(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (hitTagName == string.Empty || other.gameObject.CompareTag(hitTagName))
        {
            onStay.Invoke(other.gameObject);
        }
    }
}
