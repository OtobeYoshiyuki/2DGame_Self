using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeLib;
using OtobeGame;
using System;
using UnityEngine.SceneManagement;

namespace OtobeLib
{
    /// <summary>
    /// シーンの管理クラス
    /// </summary>
    public class SceneManager
    {
        //シーンのリスト
        private LinkedList<ISceneBase> m_sceneList = null;

        //シーンの生成を行うファクトリー
        private Dictionary<string, Func<ISceneBase>> m_sceneFactory = null;

        //シーンの情報
        private LinkedList<string> m_sceneInfo = null;

        //シーンの消去する情報
        private LinkedList<string> m_unloadInfo = null;

        //シーンを削除する数
        private int m_popCount = GameMain.NULL;

        /// <summary>
        /// SceneManagerの初期化処理
        /// </summary>
        public void Init()
        {
            //シーンのリストを生成する
            m_sceneList = new LinkedList<ISceneBase>();

            //シーンの生成を行うファクトリーを生成する
            m_sceneFactory = new Dictionary<string, Func<ISceneBase>>();

            //シーンの情報を生成する
            m_sceneInfo = new LinkedList<string>();

            //シーンの消去する情報を生成する
            m_unloadInfo = new LinkedList<string>();

            //シーンを削除する数を初期化する
            m_popCount = GameMain.NULL;
        }

        /// <summary>
        /// SceneManagerの更新処理
        /// </summary>
        public void Update()
        {
            //シーンの削除および終了処理
            for (int i = GameMain.NULL; i < m_popCount; i++)
            {
                m_sceneList.Last.Value.Final();
                m_sceneList.RemoveLast();
                //稼働しているシーンを消去する
                UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(m_unloadInfo.Last.Value);
                //アセットも消す
                Resources.UnloadUnusedAssets();
                m_unloadInfo.RemoveLast();
            }

            //削除カウントを初期化する
            m_popCount = GameMain.NULL;

            //シーンの生成を行う
            foreach (string info in m_sceneInfo)
            {
                //シーンをロードする
                UnityEngine.SceneManagement.SceneManager.LoadScene(info, LoadSceneMode.Additive);   
                //次のシーンを生成する
                m_sceneList.AddLast(m_sceneFactory[info]());
                //生成したシーンの初期化を行う
                m_sceneList.Last.Value.Init();
            }

            //シーンの情報を削除する
            m_sceneInfo.Clear();

            //シーンが存在するとき
            if (m_sceneList.Count > 0)
            {
                //一番後ろのシーンを更新する
                m_sceneList.Last.Value.Update();
            }
        }

        /// <summary>
        /// SceneManagerの更新処理
        /// ※物理演算を伴う更新
        /// </summary>
        public void FixedUpdate()
        {
            //シーンが存在するとき
            if (m_sceneList.Count > 0)
            {
                //一番後ろのシーンを更新する
                m_sceneList.Last.Value.FixedUpdate();
            }
        }

        public void LateUpdate()
        {
            //シーンが存在するとき
            if (m_sceneList.Count > 0)
            {
                //一番後ろのシーンを更新する
                m_sceneList.Last.Value.LateUpdate();
            }
        }

        /// <summary>
        /// シーンの生成用の関数を登録する
        /// </summary>
        /// <param name="name">シーンの名前</param>
        /// <param name="func">new関数</param>
        public void AddScene(string name, Func<ISceneBase> func) { m_sceneFactory[name] = func; }

        /// <summary>
        /// シーンを切り替える
        /// ※稼働しているシーンをすべて消す
        /// </summary>
        /// <param name="next">次のシーンの情報</param>
        public void ChangeScene(string next)
        {
            //シーンを追加生成する
            PushScene(next);

            //削除数を稼働中のシーンの数にする
            m_popCount = m_sceneList.Count;
        }

        /// <summary>
        /// 次に生成するSceneの情報を追加する
        /// </summary>
        /// <param name="next">次のシーンの情報</param>
        public void PushScene(string next)
        {
            m_sceneInfo.AddLast(next);
            m_unloadInfo.AddLast(next);
        }

        /// <summary>
        /// シーンの消去する数を設定する
        /// </summary>
        /// <param name="count">シーンの消す数</param>
        public void PopScene(int count = 1)
        {
            //変数の初期化
            int Inspection = GameMain.NULL;

            //追加したものを消して、消去数を減らす
            if (m_sceneInfo.Count > 0)
            {
                m_sceneInfo.RemoveLast();
                m_unloadInfo.RemoveLast();
                count--;
            }

            //シーンの消す数を補正する
            Inspection = Mathf.Max(count, GameMain.NULL);
            Inspection = Mathf.Min(Inspection, m_sceneList.Count - 1);

            //消去数を設定する
            m_popCount = Inspection;
        }

        public IEnumerator Start(string sceneName)
        {
            // 非同期でロードを行う
            var asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Additive);

            // ロードが完了していても，シーンのアクティブ化は許可しない
            asyncLoad.allowSceneActivation = false;

            // フェードアウト等の何かしらの処理
            //yield return new WaitForSeconds(10);

            // ロードが完了したときにシーンのアクティブ化を許可する
            asyncLoad.allowSceneActivation = true;

            //次のシーンを生成する
            m_sceneList.AddLast(m_sceneFactory[sceneName]());
            //生成したシーンの初期化を行う
            m_sceneList.Last.Value.Init();

            // ロードが完了するまで待つ
            yield return asyncLoad;
        }
    }
}

