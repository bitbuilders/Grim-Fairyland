using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopChain : MonoBehaviour
{
    [SerializeField] Roll m_roll = null;
    [SerializeField] Chain m_chain = null;

    Quaternion m_angleOffset;
    Quaternion m_invAngleOffset;

    private void Start()
    {
        m_angleOffset = Quaternion.Euler(Vector3.up * 90.0f);
        m_invAngleOffset = Quaternion.Euler(Vector3.up * -90.0f);
    }


    void Update()
    {
        Vector3 tail = GetLocalBack();
        Vector3 head = m_roll.transform.position;
        Vector3 dir = (tail - head).normalized;
        head += dir * (m_roll.transform.localScale.x * 0.8f);

        Quaternion tR = Quaternion.LookRotation(-transform.forward) * m_angleOffset;
        Quaternion hR = Quaternion.LookRotation(dir) * m_invAngleOffset;
        Chain.ChainTransform t = new Chain.ChainTransform() { Position = tail, Rotation = tR };
        Chain.ChainTransform h = new Chain.ChainTransform() { Position = head, Rotation = hR };

        m_chain.SetTransforms(t, h);
    }
    
    Vector3 GetLocalBack()
    {
        return -transform.forward + transform.position + new Vector3(0.0f, -0.5f, 0.5f);
    }
}
