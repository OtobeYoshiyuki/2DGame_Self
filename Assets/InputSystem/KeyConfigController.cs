using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using DG.Tweening;
using OtobeGame;

namespace OtobeLib
{
    /// <summary>
    /// キーコンフィグの制御クラス
    /// </summary>
    public class KeyConfigController : MonoBehaviour
    {
        public enum GAME_DEVICE: int
        {
            KEYBORD = 0,
            GAMEPAD,
            MAX
        }

        //選択用のゲームオブジェクト
        [SerializeField]
        private GameObject m_selectObject = null;

        // キーコンフィグの対象のインデックス
        private int m_targetIndex = 0;
        public int targetIndex { get { return m_targetIndex; } }

        // スクロールの移動量
        //private int m_scrollMove = 0;
        public const int MAX_SCROLL = 5;
        public const int MIN_SCROLL = 0;

        // メニュー一覧の初期座礁
        public Vector3 INIT_TOP = new Vector3(-930.0f, 400.0f, 0.0f);
        public Vector3 INIT_BOTOM = new Vector3(-930.0f, 915.0f, 0.0f);

        // 1スクロールの値
        public const float ONE_SCROLL = 103.0f;

        /// <summary>
        /// キーバインドのテキストの初期化処理
        /// </summary>
        public void LoadBindTextColor()
        {
            // キーコンフィグを取得する
            KeyConfigManager keyConfigManager = Locater.Get<KeyConfigManager>();

            // ボタンに割り当てられているテキストを表示する
            for (int i = 0; i < keyConfigManager.inputActions.Count; i++)
            {
                // ボタンのとき
                if (i < (int)KeyConfigManager.ACTION.MOVE)
                {
                    // デバイス情報を初期化する
                    DeviceInfoDrawInit(i, GameMain.NULL);
                }
                // 2DVectorのとき
                else
                {
                    // デバイス情報を初期化する
                    int shiftIndex = i == (int)KeyConfigManager.ACTION2D.MOVE_LEFT ? 0 : 1;
                    DeviceInfoDrawInit(i, shiftIndex);
                }
            }
        }

        /// <summary>
        /// デバイス情報を初期化する
        /// </summary>
        /// <param name="nowLoop">現在のループ</param>
        /// <param name="targetIndex">対象のインデックス</param>
        private void DeviceInfoDrawInit(int nowLoop, int targetIndex)
        {
            // キーコンフィグを取得する
            KeyConfigManager keyConfigManager = Locater.Get<KeyConfigManager>();

            // Actions直下のゲームオブジェクトを取得する
            GameObject action = gameObject.transform.GetChild(nowLoop - targetIndex).gameObject;

            // キーボードとゲームパッドのオブジェクトを取得する
            GameObject keybord = action.transform.GetChild(targetIndex).GetChild((int)GAME_DEVICE.KEYBORD).gameObject;
            GameObject gamepad = action.transform.GetChild(targetIndex).GetChild((int)GAME_DEVICE.GAMEPAD).gameObject;

            // 入力情報を取得する
            KeyConfigManager.InputAcitonReferenceInfo info = keyConfigManager.GetActionReference(nowLoop);
            KeyConfigManager.ChangeKeyConfig config = keyConfigManager.GetChangeConfig(nowLoop);

            // キーボードの表示を変更する
            ChangeDrawDeviceInfo(info.keybordText, config.keybordText, config.keybordColor, keybord);

            // ゲームパッドの表示を変更する
            ChangeDrawDeviceInfo(info.gamepadText, config.gamepadText, config.gamepadColor, gamepad);
        }

        /// <summary>
        /// デバイスの表示情報を変更する
        /// </summary>
        /// <param name="initDevicePath">デバイス情報の初期化文字</param>
        /// <param name="changeDevicePath">デバイス情報の更新文字</param>
        /// <param name="device">対象のデバイス</param>
        private void ChangeDrawDeviceInfo(string initDevicePath, string changeDevicePath, Color changeDeviceColor, GameObject device)
        {
            if (changeDevicePath == null)
            {
                // 対象デバイスのテキストを変更する
                device.GetComponent<Text>().text = initDevicePath;

                // 対象デバイスのカラーを変更する
                device.GetComponent<Text>().color = Color.white;
            }
            else
            {
                // 対象デバイスのテキストを変更する
                device.GetComponent<Text>().text = changeDevicePath;

                // 対象デバイスのカラーを変更する
                device.GetComponent<Text>().color = changeDeviceColor;
            }
        }

        public void MoveSelectUpdate(ScrollManager scrollManager)
        {
            // InputSystemManagerを取得する
            InputSystemManager inputSystemManager = Locater.Get<InputSystemManager>();

            // カーソルの回転アニメーション
            m_selectObject.transform.Rotate(new Vector3(150.0f, 0.0f, 0.0f) * Time.deltaTime, Space.World);

            // カーソルの移動処理
            if (inputSystemManager.playerInput.currentActionMap["SelectUp"].WasPressedThisFrame())
            {
                scrollManager.UpMove(ref m_targetIndex, MAX_SCROLL, MIN_SCROLL);
            }

            // カーソルの移動処理
            if (inputSystemManager.playerInput.currentActionMap["SelectDown"].WasPressedThisFrame())
            {
                scrollManager.DownMove(ref m_targetIndex, MAX_SCROLL, MIN_SCROLL);
            }

            // Actions直下のゲームオブジェクトを取得する
            GameObject action = gameObject.transform.GetChild(m_targetIndex).gameObject;

            // カーソルの移動処理
            Vector3 tempPos = m_selectObject.transform.position;
            m_selectObject.transform.position = new Vector3(tempPos.x, action.transform.position.y + 3.5f, tempPos.z);
        }
    }
}
