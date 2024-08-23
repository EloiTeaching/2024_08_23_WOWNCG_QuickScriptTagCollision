using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnCollisionActionMono : MonoBehaviour
{

    public UnityEvent m_onEnterCollision;

    public void OnCollisionEnter(Collision collision)
    {
        m_onEnterCollision.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        m_onEnterCollision.Invoke();

    }
    public void OnCollisionStay(Collision collision)
    {
        m_onEnterCollision.Invoke();
    }

    private void OnTriggerStay(Collider other)
    {
        m_onEnterCollision.Invoke();

    }

}
