using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
   CharacterController m_characterController;

    float m_movementSpeed = 4.0f;

    //Управление
    float m_horizontalInput;
    float m_verticalInput;

    Vector3 m_movementInput;
    Vector3 m_movement;

    [SerializeField]
    Joystick m_joystick;

    void Start()
    {
       m_characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        m_horizontalInput = m_joystick.Horizontal;
        m_verticalInput = m_joystick.Vertical;

        m_movementInput = new Vector3(m_horizontalInput, 0, m_verticalInput);
       
    }

    private void FixedUpdate()
    {
        m_movement = m_movementInput * m_movementSpeed * Time.deltaTime;

        m_characterController.Move(m_movement);
    }

}
