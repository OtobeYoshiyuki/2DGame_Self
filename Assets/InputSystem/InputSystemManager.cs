using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemManager : MonoBehaviour
{
    // PlayerInputComponent
    private PlayerInput m_playerInput = null;
    public PlayerInput playerInput { get { return m_playerInput; } }

    /// <summary>
    /// PlayerInput�̏���������
    /// </summary>
    public void InitPlayerInput()
    {
        // �X�N���v�g�ɃA�^�b�`����Ă���PlayerInput���擾����
        m_playerInput = gameObject.GetComponent<PlayerInput>();
    }   
}
