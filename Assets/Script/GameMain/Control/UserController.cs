using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeLib;
using UnityEngine.InputSystem;

namespace OtobeGame
{
    /// <summary>
    /// ユーザーの入力を制御するクラス
    /// </summary>
    public class UserController : IControl
    {
        // ActionMapに紐づいているMove
        public const string MOVE_ACTION = "Move";

        // ActionMapに紐づいているDash
        public const string DASH_ACTION = "Dash";

        // ActionMapに紐づいているJump
        public const string JUMP_ACTION = "Jump";

        // ActionMapに紐づいているCrounch
        public const string CROUNCH_ACTION = "Crounch";

        // ActionMapに紐づいているPunch
        public const string PUNCH_ACTION = "Punch";

        // ActionMapに紐づいているKick
        public const string KICK_ACTION = "Kick";

        // KeybordのControlScheme
        public const string KEYBORD_SCHEME = "Keybord";

        // GamepadのControlScheme
        public const string GAMEPAD_SCHEME = "Gamepad";

        /// <summary>
        /// 制御クラスの実行関数
        /// 基底クラスでは何もしない
        /// </summary>
        public void OnExecute()
        {
            // InputSystemManagerを取得する
            InputSystemManager inputSystemManager = Locater.Get<InputSystemManager>();

            //ゲームパッドが接続されていないときは、キーボード操作に切り替える
            if (Gamepad.current == null)
            {
                inputSystemManager.playerInput.SwitchCurrentControlScheme(KEYBORD_SCHEME, Keyboard.current);
            }
            //ゲームパッドが接続されているときは、ゲームパッド操作に切り替える
            else
            {
                inputSystemManager.playerInput.SwitchCurrentControlScheme(GAMEPAD_SCHEME, Gamepad.current);
            }
        }

        /// <summary>
        /// 制御クラスの移動処理
        /// </summary>
        /// <returns>1フレーム当たりの移動量</returns>
        public Vector2 OnMove()
        {
            // InputSystemManagerを取得する
            InputSystemManager inputSystemManager = Locater.Get<InputSystemManager>();

            // 左右キーの入力値を返す
            return inputSystemManager.playerInput.actions[MOVE_ACTION].ReadValue<Vector2>();
        }

        /// <summary>
        /// 制御クラスの加速処理
        /// </summary>
        /// <returns>true=ダッシュする false=ダッシュしない</returns>
        public bool OnDash()
        {
            // InputSystemManagerを取得する
            InputSystemManager inputSystemManager = Locater.Get<InputSystemManager>();

            return inputSystemManager.playerInput.currentActionMap[DASH_ACTION].IsPressed();
        }

        /// <summary>
        /// 制御クラスのジャンプ処理
        /// </summary>
        /// <returns>ジャンプの可否</returns>
        public bool OnJump()
        {
            // InputSystemManagerを取得する
            InputSystemManager inputSystemManager = Locater.Get<InputSystemManager>();

            return inputSystemManager.playerInput.currentActionMap[JUMP_ACTION].WasPressedThisFrame();
        }

        /// <summary>
        /// 制御クラスのしゃがみ処理
        /// </summary>
        /// <returns>しゃがみの可否</returns>
        public bool OnCrounch()
        {
            // InputSystemManagerを取得する
            InputSystemManager inputSystemManager = Locater.Get<InputSystemManager>();

            return inputSystemManager.playerInput.currentActionMap[CROUNCH_ACTION].IsPressed();
        }

        /// <summary>
        /// 制御クラスのパンチ処理
        /// </summary>
        /// <returns>パンチの可否</returns>
        public bool OnPunch()
        {
            // InputSystemManagerを取得する
            InputSystemManager inputSystemManager = Locater.Get<InputSystemManager>();

            return inputSystemManager.playerInput.currentActionMap[PUNCH_ACTION].WasPressedThisFrame();
        }

        /// <summary>
        /// 制御クラスのキック処理
        /// </summary>
        /// <returns>キックの可否</returns>
        public bool OnKick()
        {
            // InputSystemManagerを取得する
            InputSystemManager inputSystemManager = Locater.Get<InputSystemManager>();

            return inputSystemManager.playerInput.currentActionMap[KICK_ACTION].WasPressedThisFrame();
        }
    }
}
