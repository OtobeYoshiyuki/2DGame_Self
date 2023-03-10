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

        //シーンの管理クラス
        private SceneManager m_sceneManager = null;

        //ステータスの管理クラス
        private StatusManager m_statusManager = null;

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Start()
        {
            //シーンの制御クラスを生成する
            m_sceneManager = new SceneManager();
            m_sceneManager.Init();

            //各シーンの生成関数を登録する
            m_sceneManager.AddScene(TitleScene.SCENE_NAME, () => { return new TitleScene(); });
            m_sceneManager.AddScene(PlayScene.SCENE_NAME, () => { return new PlayScene(); });
            m_sceneManager.AddScene(ResultScene.SCENE_NAME, () => { return new ResultScene(); });

            //最初のシーンをプレイシーンに設定する
            m_sceneManager.ChangeScene(PlayScene.SCENE_NAME);

            //StatusManagerを探す
            m_statusManager = GameObject.Find(StatusManager.OBJECT_NAME).GetComponent<StatusManager>();
            m_statusManager.Init();

            //サービスロケーターにGameMainを登録する
            Locater.Bind(this);

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
