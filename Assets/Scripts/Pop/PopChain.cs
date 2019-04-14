using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopChain : MonoBehaviour
{
    [SerializeField] Roll m_roll = null;
    [SerializeField] Chain m_chain = null;
    
    
    void Update()
    {
        Vector3 tail = GetLocalBack();
        Vector3 head = m_roll.transform.position;
        Vector3 dir = tail - head;
        head += dir.normalized * (m_roll.transform.localScale.x * 0.7f);

        Chain.ChainTransform t = new Chain.ChainTransform() { Position = tail, Rotation = Quaternion.identity };
        Chain.ChainTransform h = new Chain.ChainTransform() { Position = head, Rotation = Quaternion.identity };

        m_chain.SetTransforms(t, h);
    }
    
    Vector3 GetLocalBack()
    {
        return -transform.forward + transform.position;
    }
}
