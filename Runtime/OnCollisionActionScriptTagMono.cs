using UnityEngine;
using UnityEngine.Events;

public class OnCollisionActionScriptTagMono<T> : MonoBehaviour where T: MonoBehaviour
{

    public UnityEvent m_onCollisionDetected;
    public LayerMask m_allowCollisionWith;

    [Header("Debug")]
    public GameObject m_lastCollisionSentByUnity;
    public GameObject m_lastValideCollision;
    public T m_lastScriptTagFound;


    public void DebugLog(string message) { 
    
        Debug.Log(message);
    }

    public T GetScriptTag(GameObject target) {

        if (target == null)
            return null;
        T lookFor=null;
        lookFor = target.GetComponent<T>();
        if (lookFor != null) return lookFor;
        lookFor = target.GetComponentInChildren<T>();
        if (lookFor != null) return lookFor;
        lookFor = target.GetComponentInParent<T>();
        return lookFor;

    }

    public bool IsGameObjectInLayerMask(GameObject obj, LayerMask layerMask)
    {
        // 🤖 Shift the bit corresponding to the object's layer, and then perform a bitwise AND with the LayerMask.
        // If the result is not zero, the layer is in the LayerMask.
        return (layerMask.value & (1 << obj.layer)) != 0;
    }


    private bool IsAllowed(GameObject gameObject)
    {
        m_lastValideCollision = null;
        m_lastCollisionSentByUnity = gameObject;
        if (!IsGameObjectInLayerMask(gameObject, m_allowCollisionWith))
            return false;
        T need = GetScriptTag(gameObject);
        m_lastScriptTagFound = need;
        if (need == null) 
            return false ;
        m_lastValideCollision = gameObject;
        return true;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!IsAllowed(collision.gameObject)) return;
        m_onCollisionDetected.Invoke();
    }

 

    private void OnTriggerEnter(Collider other)
    {
        if (!IsAllowed(other.gameObject)) return;
        m_onCollisionDetected.Invoke();

    }
    public void OnCollisionStay(Collision collision)
    {
        if (!IsAllowed(collision.gameObject)) return;
        m_onCollisionDetected.Invoke();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!IsAllowed(other.gameObject)) return;
        m_onCollisionDetected.Invoke();

    }

}
