using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeLib;
using UnityEngine.InputSystem;

namespace OtobeGame
{
    /// <summary>
    /// ���[�U�[�̓��͂𐧌䂷��N���X
    /// </summary>
    public class UserController : IControl
    {
        // ActionMap�ɕR�Â��Ă���Move
        public const string MOVE_ACTION = "Move";

        // ActionMap�ɕR�Â��Ă���Dash
        public const string DASH_ACTION = "Dash";

        // ActionMap�ɕR�Â��Ă���Jump
        public const string JUMP_ACTION = "Jump";

        // ActionMap�ɕR�Â��Ă���Crounch
        public const string CROUNCH_ACTION = "Crounch";

        // ActionMap�ɕR�Â��Ă���Punch
        public const string PUNCH_ACTION = "Punch";

        // ActionMap�ɕR�Â��Ă���Kick
        public const string KICK_ACTION = "Kick";

        // Keybord��ControlScheme
        public const string KEYBORD_SCHEME = "Keybord";

        // Gamepad��ControlScheme
        public const string GAMEPAD_SCHEME = "Gamepad";

        /// <summary>
        /// ����N���X�̎��s�֐�
        /// ���N���X�ł͉������Ȃ�
        /// </summary>
        public void OnExecute()
        {
            // InputSystemManager���擾����
            InputSystemManager inputSystemManager = Locater.Get<InputSystemManager>();

            //�Q�[���p�b�h���ڑ�����Ă��Ȃ��Ƃ��́A�L�[�{�[�h����ɐ؂�ւ���
            if (Gamepad.current == null)
            {
                inputSystemManager.playerInput.SwitchCurrentControlScheme(KEYBORD_SCHEME, Keyboard.current);
            }
            //�Q�[���p�b�h���ڑ�����Ă���Ƃ��́A�Q�[���p�b�h����ɐ؂�ւ���
            else
            {
                inputSystemManager.playerInput.SwitchCurrentControlScheme(GAMEPAD_SCHEME, Gamepad.current);
            }
        }

        /// <summary>
        /// ����N���X�̈ړ�����
        /// </summary>
        /// <returns>1�t���[��������̈ړ���</returns>
        public Vector2 OnMove()
        {
            // InputSystemManager���擾����
            InputSystemManager inputSystemManager = Locater.Get<InputSystemManager>();

            // ���E�L�[�̓��͒l��Ԃ�
            return inputSystemManager.playerInput.actions[MOVE_ACTION].ReadValue<Vector2>();
        }

        /// <summary>
        /// ����N���X�̉�������
        /// </summary>
        /// <returns>true=�_�b�V������ false=�_�b�V�����Ȃ�</returns>
        public bool OnDash()
        {
            // InputSystemManager���擾����
            InputSystemManager inputSystemManager = Locater.Get<InputSystemManager>();

            return inputSystemManager.playerInput.currentActionMap[DASH_ACTION].IsPressed();
        }

        /// <summary>
        /// ����N���X�̃W�����v����
        /// </summary>
        /// <returns>�W�����v�̉�</returns>
        public bool OnJump()
        {
            // InputSystemManager���擾����
            InputSystemManager inputSystemManager = Locater.Get<InputSystemManager>();

            return inputSystemManager.playerInput.currentActionMap[JUMP_ACTION].WasPressedThisFrame();
        }

        /// <summary>
        /// ����N���X�̂��Ⴊ�ݏ���
        /// </summary>
        /// <returns>���Ⴊ�݂̉�</returns>
        public bool OnCrounch()
        {
            // InputSystemManager���擾����
            InputSystemManager inputSystemManager = Locater.Get<InputSystemManager>();

            return inputSystemManager.playerInput.currentActionMap[CROUNCH_ACTION].IsPressed();
        }

        /// <summary>
        /// ����N���X�̃p���`����
        /// </summary>
        /// <returns>�p���`�̉�</returns>
        public bool OnPunch()
        {
            // InputSystemManager���擾����
            InputSystemManager inputSystemManager = Locater.Get<InputSystemManager>();

            return inputSystemManager.playerInput.currentActionMap[PUNCH_ACTION].WasPressedThisFrame();
        }

        /// <summary>
        /// ����N���X�̃L�b�N����
        /// </summary>
        /// <returns>�L�b�N�̉�</returns>
        public bool OnKick()
        {
            // InputSystemManager���擾����
            InputSystemManager inputSystemManager = Locater.Get<InputSystemManager>();

            return inputSystemManager.playerInput.currentActionMap[KICK_ACTION].WasPressedThisFrame();
        }
    }
}
