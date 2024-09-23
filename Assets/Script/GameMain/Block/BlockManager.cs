using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeLib;

namespace OtobeGame
{
    // ブロックの管理クラス
    public class BlockManager : MonoBehaviour
    {
        // ブロックの種類
        public enum BLOCK_KIND
        {
            NONE,       // 特になし
            NORMAL,     // 通常ブロック
            EXPROSION   // 爆発ブロック
        }

        // 障害物のアニメーション
        public enum OBSTACLE_ANIME : int
        {
            NONE,   // 特になし
            START,  // アニメーション開始
            FINISH  // アニメーション終了
        }

        // 連鎖爆発するブロックの管理クラス
        private List<ExBlockManager> m_exBlockManagers = new List<ExBlockManager>();
        public List<ExBlockManager> exBlockManagers => m_exBlockManagers;

        // 通常ブロックのリスト
        private List<Block> m_blocks = new List<Block>();

        /// <summary>
        /// ブロックの初期化処理(ステージがロードするたびに呼ばれる)
        /// </summary>
        /// <param name="stageManager">ステージの管理クラス</param>
        public void InitBlocks(StageManager stageManager)
        {
            // 連鎖爆発するブロックの管理クラスを検索して取得する
            m_exBlockManagers = stageManager.FindExManagers();
            m_exBlockManagers.ForEach(exBlock => exBlock.InitBlocks());

            m_blocks = stageManager.FindBlocks();
            foreach (Block block in m_blocks)
            {
                block.InitObstacle();
            }
        }

        /// <summary>
        /// ブロックの更新処理
        /// </summary>
        /// <param name="hero">操作キャラ</param>
        public void UpdateBlocks(ICharacter hero)
        {
            // 操作対象がプレイヤーの場合のみ
            if (hero.control is not UserController) return;

            // 爆発ブロックの爆破処理
            OnObstacleBreakStart(StageManager.EXBLOCK_TAG, hero);

            // 通常ブロックの破壊処理
            OnObstacleBreakStart(StageManager.BOX_TAG, hero);
        }

        /// <summary>
        /// 指定したブロックの破壊開始
        /// </summary>
        /// <param name="kind">当たり判定の種類</param>
        /// <param name="hero">キャラクター</param>
        private void OnObstacleBreakStart(string kind, ICharacter hero)
        {
            // 爆発ブロックの爆破処理
            foreach (ActionHitchecker actionHit in hero.collisionManager.GetHitCheckers(kind))
            {
                // 当たり判定の実行判定が許可されている
                if (actionHit.hitCondition.Invoke())
                {
                    // 爆発ブロックと当たったとき
                    foreach (GameObject hitObject in actionHit.hitChecker.FindHitObjects(kind))
                    {
                        // 当たった爆発ブロックを取得する
                        IObstacle obstacle = hitObject.GetComponent<IObstacle>();

                        // 障害物の破壊アニメーションが開始されてないとき
                        if (!obstacle.onDestory)
                        {
                            // 障害物の破壊アニメーションを開始する
                            hero.OnDestoryObstacle(obstacle);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 当たったオブジェクトの障害物クラスを取得する
        /// </summary>
        /// <param name="hit">当たり判定のインターフェース</param>
        /// <returns>障害物のインターフェース</returns>
        private IObstacle GetComponent_Obstacle(IHitChecker hit)
        {
            // 障害物のインターフェースを初期化する
            IObstacle obstacle = null;

            // 爆発ブロックと当たったとき
            if (hit.CheckHitObject(StageManager.EXBLOCK_TAG))
            {
                // 爆発ブロックのComponentを取得する
                obstacle = hit.FindHitObject(StageManager.EXBLOCK_TAG).GetComponent<ExplosionBlock>();
            }
            // 通常ブロックと当たったとき
            else if (hit.CheckHitObject(StageManager.BOX_TAG))
            {
                // 通常ブロックのComponentを取得する
                obstacle = hit.FindHitObject(StageManager.BOX_TAG).GetComponent<Block>();
            }

            return obstacle;
        }
    }
}
