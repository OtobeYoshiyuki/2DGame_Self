using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeLib;

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

        //キー入力の制御クラス
        private InputCs m_inputCs = null;

        //シーンの管理クラス
        private SceneManager m_sceneManager = null;

        //キャラクターの管理クラス
        private CharaManager m_characterManager = null;

        //ステータスの管理クラス
        private StatusManager m_statusManager = null;

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Start()
        {
            //キー入力の制御クラスを生成する
            m_inputCs = new InputCs();

            //キー入力を利用可能な状態にする
            m_inputCs.Enable();

            //シーンの制御クラスを生成する
            m_sceneManager = new SceneManager();
            m_sceneManager.Init();

            //各シーンの生成関数を登録する
            m_sceneManager.AddScene(TitleScene.SCENE_NAME, () => { return new TitleScene(); });
            m_sceneManager.AddScene(PlayScene.SCENE_NAME, () => { return new PlayScene(); });
            m_sceneManager.AddScene(ResultScene.SCENE_NAME, () => { return new ResultScene(); });

            //最初のシーンをプレイシーンに設定する
            m_sceneManager.ChangeScene(PlayScene.SCENE_NAME);

            //CharacterManagerを探す
            m_characterManager = GameObject.Find(CharaManager.OBJECT_NAME).GetComponent<CharaManager>();

            //StatusManagerを探す
            m_statusManager = GameObject.Find(StatusManager.OBJECT_NAME).GetComponent<StatusManager>();
            m_statusManager.Init();

            FileManager fileManager = new FileManager();
            fileManager.InputDataCsv("character_Status");

            //サービスロケーターにGameMainを登録する
            Locater.Bind(this);

            //サービスロケーターにInputCsを登録する
            Locater.Bind(m_inputCs);

            //サービスロケーターにcharaManagerを登録する
            Locater.Bind(m_characterManager);

            //サービスロケーターにstatusManagerを登録する
            Locater.Bind(m_statusManager);

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
