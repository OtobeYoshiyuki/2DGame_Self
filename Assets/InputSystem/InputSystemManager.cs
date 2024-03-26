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
    /// PlayerInputの初期化処理
    /// </summary>
    public void InitPlayerInput()
    {
        // スクリプトにアタッチされているPlayerInputを取得する
        m_playerInput = gameObject.GetComponent<PlayerInput>();
    }   
}
