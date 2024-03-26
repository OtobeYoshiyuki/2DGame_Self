using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeGame;

namespace OtobeLib
{
    public class OptionScene : ISceneBase
    {
        // シーンの名前
        public const string SCENE_NAME = "OptionScene";

        // KeyConfigManagerの制御クラス
        KeyConfigController m_keyConfigCs = null;

        // ScrollManagerの管理クラス
        ScrollManager m_scrollManager = null;

        /// <summary>
        /// 初期化処理
        /// </summary>
        public void Init()
        {
            // InputSystemManagerを取得する
            InputSystemManager inputSystemManager = Locater.Get<InputSystemManager>();

            // KeyConfigControllerのGameObjectを取得する
            GameObject actions = GameObject.Find("Actions");
            m_keyConfigCs = actions.GetComponent<KeyConfigController>();
            Locater.Bind(m_keyConfigCs);

            List<GameObject> menyuList = new List<GameObject>();
            for(int i = 0;i < (int)KeyConfigManager.ACTION.MAX;i++)
            {
                GameObject child = actions.transform.GetChild(i).gameObject;
                menyuList.Add(child);
            }

            KeyConfigManager keyConfigManager = Locater.Get<KeyConfigManager>();
            keyConfigManager.InitBinding(m_keyConfigCs);

            m_scrollManager = new ScrollManager();
            m_scrollManager.InitScroll(m_keyConfigCs.INIT_TOP, m_keyConfigCs.INIT_BOTOM, (int)KeyConfigManager.ACTION.SELECTUP,
                (int)KeyConfigManager.ACTION.CLOSE, actions, menyuList, KeyConfigController.ONE_SCROLL, KeyConfigController.MIN_SCROLL);

            inputSystemManager.playerInput.SwitchCurrentActionMap("UI");
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        public void Update()
        {
            // SceneManagerを取得する
            SceneManager sceneManager = Locater.Get<SceneManager>();

            // InputSystemManagerを取得する
            InputSystemManager inputSystemManager = Locater.Get<InputSystemManager>();

            // KeyConfigManagerを取得する
            KeyConfigManager keyConfigManager = Locater.Get<KeyConfigManager>();

            // キーコンフィグの表示クラスを更新する
            m_keyConfigCs.MoveSelectUpdate(m_scrollManager);

            if (inputSystemManager.playerInput.actions["Close"].WasPressedThisFrame())
            {
                //プレイシーンに戻る
                sceneManager.PopScene();

                inputSystemManager.playerInput.SwitchCurrentActionMap("Player");
            }

            if (inputSystemManager.playerInput.actions["ConfigStart"].WasPerformedThisFrame())
            {
                // Move以外の場合(Moveは別の処理でリバインディングを実施する)
                if (m_keyConfigCs.targetIndex < (int)KeyConfigManager.ACTION.MOVE)
                {
                    // ボタンのリバインディング開始
                    keyConfigManager.StartRebindingButton(m_keyConfigCs.targetIndex, m_keyConfigCs);
                }
                // Moveの場合
                else if (m_keyConfigCs.targetIndex < (int)KeyConfigManager.ACTION.DEFAULT)
                {
                    // 2DVectorのリバインディング開始
                    keyConfigManager.StartRebinding2DVector(m_keyConfigCs.targetIndex,
                        0, m_keyConfigCs, (int)KeyConfigManager.ACTION2D.MOVE_MAX);
                }
                // Defaultの場合
                else
                {
                    // リバインディング状態をリセットする
                    keyConfigManager.DefaultReBind(m_keyConfigCs);
                }
            }

        }

        /// <summary>
        /// 更新処理
        /// ※物理演算を伴う更新
        /// </summary>
        public void FixedUpdate()
        {

        }

        /// <summary>
        /// 更新処理
        /// カメラの処理などを記述
        /// </summary>
        public void LateUpdate()
        {

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Final()
        {

        }
    }
}
