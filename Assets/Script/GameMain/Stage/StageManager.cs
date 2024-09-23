using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using OtobeLib;

namespace OtobeGame
{
    /// <summary>
    /// ステージの管理クラス
    /// </summary>
    public class StageManager : MonoBehaviour
    {
        // GameObjectの名前
        public const string OBJECT_NAME = "StageManager";

        // 床オブジェクトのタグ
        public const string FLOOR_TAG = "Floor";

        // 爆発ブロックのタグ
        public const string EXBLOCK_TAG = "Explosion";

        // Boxオブジェクトのタグ
        public const string BOX_TAG = "Box";

        // 読み込むステージの情報
        public const string LOAD_STAGE_INFO = "Stage{0}-{1}";

        // 初期ステージの連番
        public const int INIT_STAGE_INDEX = 1;

        // 初回読み込みか
        private bool m_init = false;

        // 読み込んだステージの前回値
        private string m_prevStageInfo = "";

        // Activeなステージ
        private GameObject m_activeStage = null;
        public GameObject activeStage => m_activeStage;

        // Blockの管理クラス
        private BlockManager m_blockManager = new BlockManager();

        /// <summary>
        /// ステージ情報を読み込む
        /// </summary>
        /// <param name="stageNum">読み込むステージ数</param>
        /// <param name="stageIndex">ステージの連番</param>
        /// <returns>成功=true 失敗=false</returns>
        public void Load(int stageNum, int stageIndex = 1)
        {
            // ResourceManagaerを取得する
            ResourceManager resourceManager = Locater.Get<ResourceManager>();

            // 初回読み込み時のとき
            if (!m_init)
            {
                // 連番を強制的に初期ステージにする
                stageIndex = INIT_STAGE_INDEX;

                // 初回読み込みを済みにする
                m_init = true;
            }
            else
            {
                // 読み込まれているステージを削除する
                GameObject prevStage = GameObject.Find(m_prevStageInfo);
                Destroy(prevStage);
            }

            // ステージ情報をもとにステージを生成する
            string stageInfo = string.Format(LOAD_STAGE_INFO, stageNum, /*stageIndex*/ 2);
            GameObject gameObject = resourceManager.GetResource(stageInfo);
            m_activeStage = Instantiate(gameObject);
            SceneManager.MoveObjectNowScene(PlayScene.SCENE_NAME, m_activeStage);

            // ブロックを初期化する
            m_blockManager.InitBlocks(this);

            // 前回値を更新
            m_prevStageInfo = stageInfo;
        }

        public void UpdateStage()
        {
            // キャラクターの管理クラスを取得する
            CharaManager charaManager = Locater.Get<CharaManager>();

            // ブロックの更新を行う
            m_blockManager.UpdateBlocks(charaManager.GetHero());
        }

        /// <summary>
        /// 生成したステージ内の爆発ブロックの管理クラスを取得する
        /// </summary>
        /// <returns>爆発ブロックの管理クラスのリスト</returns>
        public List<ExBlockManager> FindExManagers()
        {
            // 爆発ブロックの管理クラスのリストを生成する
            List<ExBlockManager> exBlocks = new List<ExBlockManager>();

            // 生成したステージの子供オブジェクトを取得する
            foreach (Transform child in m_activeStage.transform)
            {
                // 検索したGameObjectではないときはスキップする
                if (!child.gameObject.name.Contains(ExBlockManager.EXBLOCK_SEARCH)) continue;

                // Componentを追加する
                exBlocks.Add(child.gameObject.GetComponent<ExBlockManager>()); 
            }

            return exBlocks;
        }

        /// <summary>
        /// 生成したステージ内の通常ブロッククラスを取得する
        /// </summary>
        /// <returns></returns>
        public List<Block> FindBlocks()
        {
            // 通常ブロックのリストを生成する
            List<Block> blocks = new List<Block>();

            // 生成したステージの床オブジェクトの子供を取得する
            foreach (Transform child in m_activeStage.transform)
            {
                // 検索したGameObjectではないときはスキップする
                if (!child.gameObject.name.Contains("NormalBlocks")) continue;

                foreach (Transform box in child.transform)
                {
                    // 検索したGameObjectではないときはスキップする
                    if (!box.gameObject.name.Contains(BOX_TAG)) continue;

                    // Componentを追加する
                    blocks.Add(box.gameObject.GetComponent<Block>());
                }
            }

            return blocks;
        }
    }

}
