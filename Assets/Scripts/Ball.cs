using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody m_Rigidbody;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Wall"))
        {
            m_Rigidbody.AddForce(other.transform.right * 0.05f, ForceMode.Impulse);
        }
        if (other.gameObject.CompareTag("Paddle"))
        {
            m_Rigidbody.AddForce(other.transform.up * 0.05f, ForceMode.Impulse);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        Vector3 velocity = m_Rigidbody.velocity;
        
        //after a collision we accelerate a bit
        velocity += velocity.normalized * 0.05f;
        
        //check if we are not going totally vertically as this would lead to being stuck, we add a little vertical force
        if (Vector3.Dot(velocity.normalized, Vector3.up) < 0.1f)
        {
            velocity += velocity.y > 0 ? Vector3.up * 0.75f : Vector3.down * 0.75f;
        }

        //max velocity
        if (velocity.magnitude > 5.0f)
        {
            velocity = velocity.normalized * 5.0f;
        }

        m_Rigidbody.velocity = velocity;
    }
}
