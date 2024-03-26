using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace OtobeLib
{
    /// <summary>
    /// キーコンフィグの制御クラス
    /// </summary>
    public class KeyConfigController : MonoBehaviour
    {
        public enum GAME_DEVICE
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
        public const int MAX_SCROLL = 4;
        public const int MIN_SCROLL = 0;

        // メニュー一覧の初期座礁
        public Vector3 INIT_TOP = new Vector3(-930.0f, 400.0f, 0.0f);
        public Vector3 INIT_BOTOM = new Vector3(-930.0f, 915.0f, 0.0f);

        // 1スクロールの値
        public const float ONE_SCROLL = 50.0f;

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
                    // Actions直下のゲームオブジェクトを取得する
                    GameObject action = gameObject.transform.GetChild(i).gameObject;

                    // キーボードとゲームパッドのオブジェクトを取得する
                    GameObject keybord = action.transform.GetChild(0).GetChild(0).gameObject;
                    GameObject gamepad = action.transform.GetChild(0).GetChild(1).gameObject;

                    // 入力情報を取得する
                    KeyConfigManager.InputAcitonReferenceInfo info = keyConfigManager.GetActionReference(i);
                    KeyConfigManager.ChangeKeyConfig config = keyConfigManager.GetChangeConfig(i);

                    if (config.keybordText == null)
                    {
                        // キーボードのテキストを変更する
                        keybord.GetComponent<Text>().text = info.keybordText;

                        // キーボードのカラーを変更する
                        keybord.GetComponent<Text>().color = Color.white;
                    }
                    else 
                    {
                        // キーボードのテキストを変更する
                        keybord.GetComponent<Text>().text = config.keybordText;

                        // キーボードのカラーを変更する
                        keybord.GetComponent<Text>().color = config.keybordColor;
                    }

                    if (config.gamepadText == null)
                    {
                        // ゲームパッドのテキストを変更する
                        gamepad.GetComponent<Text>().text = info.gamepadText;

                        // ゲームパッドのカラーを変更する
                        gamepad.GetComponent<Text>().color = Color.white;
                    }
                    else 
                    {
                        // ゲームパッドのテキストを変更する
                        gamepad.GetComponent<Text>().text = config.gamepadText;

                        // ゲームパッドのカラーを変更する
                        gamepad.GetComponent<Text>().color = config.gamepadColor;
                    }
                }
                // 2DVectorのとき
                else
                {
                    int shiftIndex;
                    if (i == (int)KeyConfigManager.ACTION2D.MOVE_LEFT)
                    {
                        shiftIndex = 0;
                    }
                    else
                    {
                        shiftIndex = 1;
                    }

                    // Actions直下のゲームオブジェクトを取得する
                    GameObject action = gameObject.transform.GetChild(i - shiftIndex).gameObject;

                    // キーボードとゲームパッドのオブジェクトを取得する
                    GameObject keybord = action.transform.GetChild(shiftIndex).GetChild(0).gameObject;
                    GameObject gamepad = action.transform.GetChild(shiftIndex).GetChild(1).gameObject;

                    // 入力情報を取得する
                    KeyConfigManager.InputAcitonReferenceInfo info = keyConfigManager.GetActionReference(i);
                    KeyConfigManager.ChangeKeyConfig config = keyConfigManager.GetChangeConfig(i);

                    if (config.keybordText == null)
                    {
                        // キーボードのテキストを変更する
                        keybord.GetComponent<Text>().text = info.keybordText;

                        // キーボードのカラーを変更する
                        keybord.GetComponent<Text>().color = Color.white;
                    }
                    else
                    {
                        // キーボードのテキストを変更する
                        keybord.GetComponent<Text>().text = config.keybordText;

                        // キーボードのカラーを変更する
                        keybord.GetComponent<Text>().color = config.keybordColor;
                    }

                    if (config.gamepadText == null)
                    {
                        // ゲームパッドのテキストを変更する
                        gamepad.GetComponent<Text>().text = info.gamepadText;

                        // ゲームパッドのカラーを変更する
                        gamepad.GetComponent<Text>().color = Color.white;
                    }
                    else
                    {
                        // ゲームパッドのテキストを変更する
                        gamepad.GetComponent<Text>().text = config.gamepadText;

                        // ゲームパッドのカラーを変更する
                        gamepad.GetComponent<Text>().color = config.gamepadColor;
                    }
                }
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
