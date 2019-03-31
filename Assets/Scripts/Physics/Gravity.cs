using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
      [Tooltip("If the rigid velocity is traveling upwards (like jumping), this value will dampen it by adding gravity")]
    [SerializeField] [Range(0.01f, 100.0f)] float m_upwardDampening = 1.0f;
      [Tooltip("If the rigid velocity is traveling downwards (like falling), this value will increase it by adding gravity")]
    [SerializeField] [Range(0.01f, 100.0f)] float m_downwardForce = 1.0f;
      [Tooltip("Will only add gravity if the rigid velocity is greater than this value (less than the negative of the value for downward force)")]
    [SerializeField] [Range(0.0001f, 0.1f)] float m_benchmark = 0.001f;
      [Tooltip("A multiplier for how fast the object will fall (0.0 = will not affect, 1.0 = will double every frame)")]
    [SerializeField] [Range(0.0f, 1.0f)] float m_gravityMultiplier = 0.5f;

    Rigidbody m_rigidBody;

    private void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float y = m_rigidBody.velocity.y;
        if (y > m_benchmark)
        {
            m_rigidBody.velocity += Physics.gravity * (m_upwardDampening - 1.0f) * Time.deltaTime;
        }
        else if (y < -m_benchmark)
        {
            m_rigidBody.velocity += Physics.gravity * (m_downwardForce - 1.0f) * Time.deltaTime;
            m_rigidBody.velocity += y * Vector3.up * m_gravityMultiplier;
        }
    }
}
