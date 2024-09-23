using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace OtobeLib
{
    public class KeyConfigManager : MonoBehaviour
    {
        public enum ACTION
        {
            PUNCH = 0,
            KICK,
            DASH,
            CROUCH,
            JUMP,
            OPEN,
            CLOSE,
            SELECTUP,
            SELECTDOWN,
            MOVE,
            DEFAULT,
            MAX,
        }

        public enum ACTION2D
        {
            MOVE_LEFT = ACTION.MOVE,
            MOVE_RIGHT,
            MOVE_MAX = 2
        }

        [System.Serializable]
        public class InputAcitonReferenceInfo
        {
            [SerializeField]
            private string m_keybordText;
            [SerializeField]
            private string m_gamepadText;
            [SerializeField]
            private InputActionReference m_reference;

            public string keybordText { get { return m_keybordText; } }
            public string gamepadText { get { return m_gamepadText; } }
            public InputActionReference reference { get { return m_reference; } }
        }

        public class ChangeKeyConfig
        {
            private string m_keybordText;
            private string m_gamepadText;
            private Color m_keybordColor;
            private Color m_gamepadColor;
            public string keybordText { get { return m_keybordText; } set { m_keybordText = value; } }
            public string gamepadText { get { return m_gamepadText; } set { m_gamepadText = value; } }
            public Color keybordColor { get { return m_keybordColor; } set { m_keybordColor = value; } }
            public Color gamepadColor { get { return m_gamepadColor; } set { m_gamepadColor = value; } }
        }

        [SerializeField]
        private List<InputAcitonReferenceInfo> m_inputActionInfos;
        public List<InputAcitonReferenceInfo> inputActions { get { return m_inputActionInfos; } }

        private List<ChangeKeyConfig> m_changeKeyConfigs = new List<ChangeKeyConfig>();

        private InputActionRebindingExtensions.RebindingOperation m_bindingOperation;

        private List<string> m_keybordDefaultTexts = new List<string>();
        private List<string> m_gamepadDefaultTexts = new List<string>();

        /// <summary>
        /// キーコンフィグの初期化処理
        /// </summary>
        /// <param name="contoller">キーコンフィグの表示の制御クラス</param>
        public void InitBinding(KeyConfigController contoller)
        {
            // DefaultTextの保存
            foreach (InputAcitonReferenceInfo info in m_inputActionInfos)
            {
                m_keybordDefaultTexts.Add(info.keybordText);
                m_gamepadDefaultTexts.Add(info.gamepadText);

                // 空のデータを入れる
                ChangeKeyConfig changeKeyConfig = new ChangeKeyConfig();
                changeKeyConfig.keybordColor = Color.white;
                changeKeyConfig.gamepadColor = Color.white;
                m_changeKeyConfigs.Add(changeKeyConfig);
            }

            // キーの状態を読み込む
            LoadBindData(contoller);

            //デバイスが切り替わったときの処理を登録する
            InputSystem.onDeviceChange += (device, change) =>
                        {
                            switch (change)
                            {
                                //新たなデバイスがシステムに追加された
                                case InputDeviceChange.Added:
                                case InputDeviceChange.Reconnected:
                                    LoadBindData(contoller);
                                    break;
                                // 既存のデバイスがシステムから削除された
                                case InputDeviceChange.Removed:
                                case InputDeviceChange.Disconnected:
                                    InputSystem.FlushDisconnectedDevices();
                                    break;
                            }
                        };
        }

        /// <summary>
        /// キーコンフィグのロード処理
        /// </summary>
        /// <param name="contoller">キーコンフィグの表示の制御クラス</param>
        public void LoadBindData(KeyConfigController contoller)
        {
            // InputSystemManagerを取得する
            InputSystemManager inputSystemManager = Locater.Get<InputSystemManager>();

            // キーボードのテキストを初期化する
            foreach (string text in m_keybordDefaultTexts)
            {
                int index = m_keybordDefaultTexts.IndexOf(text);
                m_changeKeyConfigs[index].keybordText = text;
            }

            // ゲームパッドのテキストを初期化する
            foreach (string text in m_gamepadDefaultTexts)
            {
                int index = m_gamepadDefaultTexts.IndexOf(text);
                m_changeKeyConfigs[index].gamepadText = text;
            }

            //すでにリバインディングしたことがある場合はシーン読み込み時に変更。
            string rebinds = PlayerPrefs.GetString("rebinds");

            if (!string.IsNullOrEmpty(rebinds))
            {
                // キー入力の情報をJSON形式で上書きする
                inputSystemManager.playerInput.actions.LoadBindingOverridesFromJson(rebinds);

                for (int i = 0; i < (int)KeyConfigController.GAME_DEVICE.MAX; i++)
                {
                    for (int j = 0; j < m_inputActionInfos.Count; j++)
                    {
                        InputAcitonReferenceInfo info = m_inputActionInfos[j];
                        ChangeKeyConfig changeKey = m_changeKeyConfigs[j];

                        // Button用ロード
                        if (j < (int)ACTION.MOVE)
                        {
                            string buttonText = InputControlPath.ToHumanReadableString(
                                                info.reference.action.bindings[i].effectivePath,
                                                InputControlPath.HumanReadableStringOptions.OmitDevice);

                            if (i == (int)KeyConfigController.GAME_DEVICE.KEYBORD)
                            {
                                // Keybordの設定を読み込む
                                changeKey.keybordText = buttonText;
                            }
                            else
                            {
                                // Gamepadの設定を読み込む
                                changeKey.gamepadText = buttonText;
                            }
                        }
                        // 2DVector用ロード
                        else
                        {
                            switch ((ACTION2D)j)
                            {
                                case ACTION2D.MOVE_LEFT:
                                    string vectorText = InputControlPath.ToHumanReadableString(
                                        info.reference.action.bindings[3 + (i * 5)].effectivePath,    // 左
                                        InputControlPath.HumanReadableStringOptions.OmitDevice);
                                    if (i == (int)KeyConfigController.GAME_DEVICE.KEYBORD)
                                    {
                                        // Keybordの設定を読み込む
                                        changeKey.keybordText = vectorText;
                                    }
                                    else
                                    {
                                        // Gamepadの設定を読み込む
                                        changeKey.gamepadText = vectorText;
                                    }
                                    break;
                                case ACTION2D.MOVE_RIGHT:
                                    vectorText = InputControlPath.ToHumanReadableString(
                                        info.reference.action.bindings[4 + (i * 5)].effectivePath,    // 右
                                        InputControlPath.HumanReadableStringOptions.OmitDevice);
                                    if (i == (int)KeyConfigController.GAME_DEVICE.KEYBORD)
                                    {
                                        // Keybordの設定を読み込む
                                        changeKey.keybordText = vectorText;
                                    }
                                    else
                                    {
                                        // Gamepadの設定を読み込む
                                        changeKey.gamepadText = vectorText;
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
            // キーコンフィグの表示を再ロードする
            contoller.LoadBindTextColor();
        }

        /// <summary>
        /// キーをリバインディングするときに呼ばれる処理(Button用)
        /// </summary>
        /// <param name="index">対象の配列の添え字</param>
        /// <param name="contoller">キーコンフィグ表示の制御クラス</param>
        public void StartRebindingButton(int index, KeyConfigController contoller)
        {
            // InputSystemManagerを取得する
            InputSystemManager inputSystemManager = Locater.Get<InputSystemManager>();

            // InputActionを取得する
            InputAction action = m_inputActionInfos[index].reference.action;

            // Actionを無効にする
            action.Disable();

            // キーボードかゲームパッドか判定する
            if (Gamepad.current == null)
            {
                // キーコンフィグをリバインディングしている間はカラーを赤色にする
                m_changeKeyConfigs[index].keybordColor = Color.red;
                contoller.LoadBindTextColor();

                // 選択したボタンのリバインディング開始
                m_bindingOperation = action.PerformInteractiveRebinding()
                    .WithTargetBinding((int)KeyConfigController.GAME_DEVICE.KEYBORD).WithControlsExcluding("Mouse")
                    .OnComplete(operation => RebindComplateButton(m_changeKeyConfigs[index], (int)KeyConfigController.GAME_DEVICE.KEYBORD, contoller, action))
                    .Start();
            }
            else
            {
                // キーコンフィグをリバインディングしている間はカラーを赤色にする
                m_changeKeyConfigs[index].gamepadColor = Color.red;
                contoller.LoadBindTextColor();

                // 選択したボタンのリバインディング開始
                m_bindingOperation = action.PerformInteractiveRebinding()
                    .WithTargetBinding((int)KeyConfigController.GAME_DEVICE.GAMEPAD).WithControlsExcluding("Mouse")
                    .OnComplete(operation => RebindComplateButton(m_changeKeyConfigs[index], (int)KeyConfigController.GAME_DEVICE.GAMEPAD, contoller, action))
                    .Start();
            }
        }

        /// <summary>
        /// キーのリバインディングが終了したら呼ばれる処理
        /// </summary>
        /// <param name="changeKey">キーコンフィグの情報</param>
        /// <param name="control">ゲームパッドの種類</param>
        /// <param name="contoller">キーコンフィグの表示の制御クラス</param>
        /// <param name="action">対象のアクション</param>
        public void RebindComplateButton(ChangeKeyConfig changeKey,
            int control, KeyConfigController contoller, InputAction action)
        {
            // InputSystemManagerを取得する
            InputSystemManager inputSystemManager = Locater.Get<InputSystemManager>();

            // キーボードが接続されているとき
            if (control == 0)
            {
                // キーコンフィグのテキストカラーを白色に戻す
                changeKey.keybordColor = Color.white;
            }
            // ゲームパッドが接続されているとき
            else
            {
                // キーコンフィグのテキストカラーを白色に戻す
                changeKey.gamepadColor = Color.white;
            }

            // リバインディングオペレーションを破棄する
            m_bindingOperation.Dispose();

            // アクションマップをもとに戻す
            action.Enable();

            // リバインディングしたキーを保存
            PlayerPrefs.SetString("rebinds", inputSystemManager.playerInput.actions.SaveBindingOverridesAsJson());

            // 読み込んだデータを読み込む
            LoadBindData(contoller);
        }

        /// <summary>
        /// キーをリバインディングするときに呼ばれる処理(2DVector用)
        /// </summary>
        /// <param name="index">対象の配列の添え字</param>
        /// <param name="contoller">キーコンフィグ表示の制御クラス</param>
        public void StartRebinding2DVector(int index, int indexPart,
            KeyConfigController contoller, int moveSize)
        {
            // InputActionを取得する
            InputAction action = m_inputActionInfos[index].reference.action;

            // Actionを無効にする
            action.Disable();

            // キーボードかゲームパッドか判定する
            if (Gamepad.current == null)
            {
                // キーコンフィグをリバインディングしている間はカラーを赤色にする
                m_changeKeyConfigs[index].keybordColor = Color.red;
                contoller.LoadBindTextColor();

                // 選択したボタンのリバインディング開始
                m_bindingOperation = action.PerformInteractiveRebinding()
                    .WithTargetBinding(3 + indexPart).WithControlsExcluding("Mouse")
                    .OnMatchWaitForAnother(0.15f)
                    .OnComplete(operation => RebindComplate2DVector(m_changeKeyConfigs[index], 0,
                    contoller, action, index, indexPart, moveSize))
                    .Start();
            }
            else
            {
                // キーコンフィグをリバインディングしている間はカラーを赤色にする
                m_changeKeyConfigs[index].gamepadColor = Color.red;
                contoller.LoadBindTextColor();

                // 選択したボタンのリバインディング開始
                m_bindingOperation = action.PerformInteractiveRebinding()
                    .WithTargetBinding(8 + indexPart).WithControlsExcluding("Mouse")
                    .OnMatchWaitForAnother(0.15f)
                    .OnComplete(operation => RebindComplate2DVector(m_changeKeyConfigs[index], 1,
                    contoller, action, index, indexPart, moveSize))
                    .Start();
            }
        }

        /// <summary>
        /// キーのリバインディングが終了したら呼ばれる処理
        /// </summary>
        /// <param name="changeKey">キーコンフィグの情報</param>
        /// <param name="control">ゲームパッドの種類</param>
        /// <param name="contoller">キーコンフィグの表示の制御クラス</param>
        /// <param name="action">対象のアクション</param>
        public void RebindComplate2DVector(ChangeKeyConfig changeKey,
            int control, KeyConfigController contoller, InputAction action, int index, int indexPart, int moveSize)
        {
            // Complate処理を呼ぶ
            RebindComplateButton(changeKey, control, contoller, action);

            // 参照用インデックスが途中の場合、処理を反復する
            if (indexPart != moveSize - 1)
            {
                indexPart++;
                index++;
                StartRebinding2DVector(index, indexPart, contoller, moveSize);
            }
        }

        /// <summary>
        /// バインディングのリセット処理
        /// </summary>
        /// <param name="playerInput">キー入力関連のクラス</param>
        /// <param name="contoller">キーコンフィグの表示の制御クラス</param>
        public void DefaultReBind(KeyConfigController contoller)
        {
            // InputSystemManagerを取得する
            InputSystemManager inputSystemManager = Locater.Get<InputSystemManager>();

            // リバインディング状態を解除する
            inputSystemManager.playerInput.actions.RemoveAllBindingOverrides();

            // JSON形式で保存する
            PlayerPrefs.SetString("rebinds", inputSystemManager.playerInput.actions.SaveBindingOverridesAsJson());

            // 保存したデータを再ロードする
            LoadBindData(contoller);
        }

        public InputAcitonReferenceInfo GetActionReference(int index) { return m_inputActionInfos[index]; }
        public ChangeKeyConfig GetChangeConfig(int index) { return m_changeKeyConfigs[index]; }

    }

}

