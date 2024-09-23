using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeLib;
using UnityEngine.InputSystem;


namespace OtobeGame
{
    /// <summary>
    /// ゲームのメイン処理を記述するクラス
    /// GameMain以外のクラスではStart、Updateを使用しない
    /// ※処理の順番を管理するため
    /// </summary>
    public class GameMain : MonoBehaviour
    {
        //0を扱う定数
        public const int NULL = 0;

        //右矢印キーが押された時の定数
        public const float RIGHTPUSH = 1.0f;

        //左矢印キーが押された時の定数
        public const float LEFTPUSH = -1.0f;

        // リソースの管理クラス
        private ResourceManager m_resourceManagar = null;

        //シーンの管理クラス
        private SceneManager m_sceneManager = null;

        //ステータスの管理クラス
        private StatusManager m_statusManager = null;

        // キーコンフィグの管理クラス
        private KeyConfigManager m_keyConfigManager = null;

        // InputSystemの管理クラス
        private InputSystemManager m_inputSystemManager = null;

        // フェードイン、フェードアウトの管理クラス
        private FadeManager m_fadeManager = null;

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Start()
        {
            // リソースの管理クラスを生成する
            m_resourceManagar = GameObject.Find(ResourceManager.OBJECT_NAME).GetComponent<ResourceManager>();
            m_resourceManagar.Load();

            //シーンの制御クラスを生成する
            m_sceneManager = new SceneManager();
            m_sceneManager.Init();

            //各シーンの生成関数を登録する
            m_sceneManager.AddScene(TitleScene.SCENE_NAME, () => { return new TitleScene(); });
            m_sceneManager.AddScene(PlayScene.SCENE_NAME, () => { return new PlayScene(); });
            m_sceneManager.AddScene(ResultScene.SCENE_NAME, () => { return new ResultScene(); });
            m_sceneManager.AddScene(OptionScene.SCENE_NAME, () => { return new OptionScene(); });

            //最初のシーンをプレイシーンに設定する
            m_sceneManager.ChangeScene(PlayScene.SCENE_NAME);

            //StatusManagerを探す
            m_statusManager = GameObject.Find(StatusManager.OBJECT_NAME).GetComponent<StatusManager>();
            m_statusManager.Init(); 

            //KeyConfigManagerを探す
            m_keyConfigManager = GameObject.Find("KeyConfigManager").GetComponent<KeyConfigManager>();
             
            // InputSystemManagerを探す
            m_inputSystemManager = GameObject.Find("InputManager").GetComponent<InputSystemManager>();
            m_inputSystemManager.InitPlayerInput();

            // FadeManagerを探す
            m_fadeManager = GameObject.Find("FadeManager").GetComponent<FadeManager>();
            m_fadeManager.InitFadeCanvas(FadeManager.FADESTATE.NONE);
            m_fadeManager.FadeInFadeOut();

            // サービスロケーターにResourceManagerを登録する
            Locater.Bind(m_resourceManagar);

            //サービスロケーターにSceneManagerを登録する
            Locater.Bind(m_sceneManager);

            //サービスロケーターにGameMainを登録する
            Locater.Bind(this);

            //サービスロケーターにStatusManagerを登録する
            Locater.Bind(m_statusManager);

            //サービスロケーターにKeyConfigManagerを登録する
            Locater.Bind(m_keyConfigManager);

            // サービスロケーターにInputSystemManagerを登録する
            Locater.Bind(m_inputSystemManager);

            // サービスロケーターにFadeManagerを登録する
            Locater.Bind(m_fadeManager);

            //CoroutineHandlerのゲームオブジェクトを生成する
            CoroutineHandler handler = CoroutineHandler.instance;
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        private void Update()
        {
            //シーンの管理クラスの更新処理
            m_sceneManager.Update();
        }

        /// <summary>
        /// 更新処理
        /// ※物理演算を使用する処理を記述する
        /// </summary>
        private void FixedUpdate()
        {
            //シーンの管理クラスの更新処理
            m_sceneManager.FixedUpdate();
        }

        /// <summary>
        /// カメラの処理を記述する
        /// </summary>
        private void LateUpdate()
        {
            //シーンの管理クラスの更新処理
            m_sceneManager.LateUpdate();
        }
    }
}
