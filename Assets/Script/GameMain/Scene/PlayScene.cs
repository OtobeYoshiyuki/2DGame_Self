using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeLib;
using Cinemachine;

namespace OtobeGame
{
    /// <summary>
    /// プレイシーン
    /// </summary>
    public class PlayScene : ISceneBase
    {
        //シーンの名前
        public const string SCENE_NAME = "PlayScene";

        //キャラクターの管理クラス
        private CharaManager m_characterManager = null;
        public CharaManager characterManager { get { return m_characterManager; } }

        // 背景のスクロールクラス
        private BackGroundMover m_backMover = null;
        public BackGroundMover backMover { get { return m_backMover; } }

        // 爆発ブロックの管理クラス
        private ExBlockManager m_exBlockManager = null;

        private CinemachineVirtualCamera m_virtualCamera = null;

        // シーンの切り替えを管理するフラグ
        private bool m_playChange = false;
        public bool playChange
        {
            set { m_playChange = value; }
            get { return m_playChange; }
        }

        /// <summary>
        /// 初期化処理
        /// </summary>
        public void Init()
        {
            // 背景のスクロールクラスを取得する
            m_backMover = GameObject.Find("PlayUI").transform.GetChild(0).GetComponent<BackGroundMover>();
            m_backMover.InitBackGrond();

            // サービスロケーターにPlaySceneを登録する
            Locater.Bind(this);

            // サービスロケーターにBackMoverを登録する
            Locater.Bind(m_backMover);

            // CharacterManagerを初期化する
            m_characterManager = new CharaManager();
            m_characterManager.InitCharaManager();

            // サービスロケーターにcharaManagerを登録する
            Locater.Bind(m_characterManager);

            m_exBlockManager = GameObject.Find("Blocks1").GetComponent<ExBlockManager>();
            m_exBlockManager.InitBlocks();

            m_virtualCamera = GameObject.Find("CMCamera").GetComponent<CinemachineVirtualCamera>();
            m_virtualCamera.Follow = m_characterManager.GetHero().transform;

            CompositeCollider2D compositeCollider2D = GameObject.Find("Area").GetComponent<CompositeCollider2D>();
            m_virtualCamera.gameObject.GetComponent<CinemachineConfiner>().m_BoundingShape2D = compositeCollider2D;

            //FadeManager fadeManager = Locater.Get<FadeManager>();
            //fadeManager.FadeOut();

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

            // CharacterManagerを更新する
            m_characterManager.UpdateCharacters();

            // BackMoverを更新する
            m_backMover.Move();

            // オプションボタンが押されたら
            if (inputSystemManager.playerInput.currentActionMap["Open"].WasPressedThisFrame() && m_playChange)
            {
                // シーンを切り替える
                sceneManager.PushScene(OptionScene.SCENE_NAME);
            }
        }

        /// <summary>
        /// 更新処理
        /// ※物理演算を伴う更新
        /// </summary>
        public void FixedUpdate()
        {
            //CharacterManagerを更新する
            m_characterManager.FixedUpdateCharacters();
        }

        /// <summary>
        /// 更新処理
        /// カメラの処理などを記述
        /// </summary>
        public void LateUpdate()
        {
            //CharacterManagerを更新する
            m_characterManager.LateUpdateCharacters();
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Final()
        {
            m_backMover.Destory();
        }
    }
}
