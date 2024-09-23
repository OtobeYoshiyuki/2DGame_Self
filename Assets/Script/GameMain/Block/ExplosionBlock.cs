using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OtobeGame
{
    using BLOCK_KIND = BlockManager.BLOCK_KIND;

    // 爆発ブロック
    public class ExplosionBlock : Block
    {
        // GameObjectのタグ
        public const string EXPROSION_TAG = "Explosion";

        /// <summary>
        /// 障害物の初期化処理
        /// </summary>
        public override void InitObstacle()
        {
            // animatorを取得する
            m_animator = gameObject.GetComponent<Animator>();

            // ブロックの種類を設定する
            m_kind = BLOCK_KIND.EXPROSION;

            // 破壊済みのフラグを設定する
            m_onDestory = false;
        }

        public override void DestoryAnimeStart()
        {
            // 爆発ブロックの管理クラスを取得する
            ExBlockManager exBlockManager = transform.parent.gameObject.GetComponent<ExBlockManager>();

            // 爆発アニメーションを開始する
            m_animator.SetInteger(BLOCK_KEY, BLOCK_VALUE);

            // 破壊済みにする
            m_onDestory = true;

            // 障害物の破壊を開始する
            CoroutineHandler.instance.StartCoroutine(exBlockManager.ExplosionAllBlocks());
        }
    }

}
