using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    public struct ChainTransform
    {
        public Vector3 Position;
        public Quaternion Rotation;
    }

    [SerializeField] GameObject m_chainHead = null;
    [SerializeField] GameObject m_chainTail = null;
    [SerializeField] [Range(0.0f, 25.0f)] float m_maxChainLength = 5.0f;

    public void SetTransforms(ChainTransform tailTransform, ChainTransform headTransform)
    {
        SetTailTransform(tailTransform);
        SetHeadTransform(headTransform);
    }

    public void SetHeadTransform(ChainTransform transform)
    {
        if (m_chainHead)
        {
            m_chainHead.transform.position = transform.Position;
            m_chainHead.transform.rotation = transform.Rotation;
            Vector3 dir = m_chainHead.transform.position - m_chainTail.transform.position;
            float dist = dir.magnitude;
            if (dist > m_maxChainLength)
            {
                m_chainHead.transform.position = dir.normalized * m_maxChainLength + m_chainTail.transform.position;
            }
        }
    }

    public void SetTailTransform(ChainTransform transform)
    {
        if (m_chainTail)
        {
            m_chainTail.transform.position = transform.Position;
            m_chainTail.transform.rotation = transform.Rotation;
        }
    }
}
