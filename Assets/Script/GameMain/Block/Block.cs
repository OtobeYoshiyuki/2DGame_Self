using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeLib;

namespace OtobeGame
{
    using BLOCK_KIND = BlockManager.BLOCK_KIND;
    using OBSTACLE_ANIME = BlockManager.OBSTACLE_ANIME;

    // 通常ブロック
    public class Block : Obstacle
    {
        // animatorに渡すパラメーターのKey
        public const string BLOCK_KEY = "BlockState";

        // animatorに渡すパラメーターのValue
        public const int BLOCK_VALUE = (int)OBSTACLE_ANIME.START;

        /// <summary>
        /// 通常ブロックの初期化処理
        /// </summary>
        public override void InitObstacle()
        {
            // animatorを取得する
            m_animator = gameObject.GetComponent<Animator>();

            // ブロックの種類を設定する
            m_kind = BLOCK_KIND.NORMAL;

            // 破壊済みのフラグを設定する
            m_onDestory = false;
        }

        /// <summary>
        /// 通常ブロックの破壊アニメーションの開始
        /// </summary>
        public override void DestoryAnimeStart()
        {
            // 爆発アニメーションを開始する
            m_animator.SetInteger(BLOCK_KEY, BLOCK_VALUE);

            // 破壊済みにする
            m_onDestory = true;
        }
    }
}
