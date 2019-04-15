using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopMovement : MonoBehaviour
{
    [SerializeField] [Range(0.0f, 50000.0f)] float m_acceleration = 1.0f;
      [Tooltip("When going the opposite direction of your rigid velocity, your speed will be multiplied by this value")]
    [SerializeField] [Range(1.0f, 10.0f)] float m_oppositeDirMult = 2.0f;
    [SerializeField] [Range(0.0f, 50000.0f)] float m_topSpeed = 10.0f;
    [SerializeField] [Range(0.0f, 5000.0f)] float m_jumpForce = 75.0f;

    Rigidbody m_rigidBody;
    Vector3 m_velocity;
    float m_sqrTopSpeed;

    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_sqrTopSpeed = m_topSpeed * m_topSpeed;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        float horz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        float horzMult = 1.0f;
        float vertMult = 1.0f;
        if (horz != 0.0f && Mathf.Sign(m_velocity.x) != Mathf.Sign(m_rigidBody.velocity.x))
        {
            horzMult = m_oppositeDirMult;
        }
        if (vert != 0.0f && Mathf.Sign(m_velocity.z) != Mathf.Sign(m_rigidBody.velocity.z))
        {
            vertMult = m_oppositeDirMult;
        }

        m_velocity += new Vector3(horz * horzMult, 0.0f, vert * vertMult) * m_acceleration * Time.deltaTime;
        if (m_velocity.sqrMagnitude > m_sqrTopSpeed) m_velocity = m_velocity.normalized * m_topSpeed;

        if (horz == 0.0f && Mathf.Abs(m_velocity.x) > 0.01f) m_velocity.x -= Mathf.Sign(m_velocity.x) * m_acceleration * Time.deltaTime;
        if (vert == 0.0f && Mathf.Abs(m_velocity.z) > 0.01f) m_velocity.z -= Mathf.Sign(m_velocity.z) * m_acceleration * Time.deltaTime;

        m_rigidBody.AddForce(m_velocity, ForceMode.Force);
    }
}
