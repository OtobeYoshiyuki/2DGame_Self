using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeGame;

using BLOCK_KIND = OtobeGame.BlockManager.BLOCK_KIND;
using OBSTACLE_ANIME = OtobeGame.BlockManager.OBSTACLE_ANIME;

namespace OtobeLib
{
    // 障害物のインターフェース
    public interface IObstacle
    {
        // 障害物のアニメーションのプロパティ
        Animator animator { get; }

        // 障害物の種類
        BLOCK_KIND kind { get; }

        // 障害物の破壊フラグ
        bool onDestory { get; }

        /// <summary>
        /// 障害物の初期化処理
        /// </summary>
        void InitObstacle();

        /// <summary>
        /// 障害物の破壊アニメーションの開始
        /// </summary>
        void DestoryAnimeStart();

        /// <summary>
        /// 障害物の破壊アニメーションの終了
        /// </summary>
        void DestoryAnimeFinish();
    }

    // 障害物の抽象クラス
    public abstract class Obstacle : MonoBehaviour, IObstacle
    {
        // 障害物のアニメーション
        protected Animator m_animator = null;
        public Animator animator => m_animator;

        // 障害物の種類
        protected BLOCK_KIND m_kind = BLOCK_KIND.NONE;
        public BLOCK_KIND kind => m_kind;

        // 障害物の破壊フラグ
        protected bool m_onDestory = false;
        public bool onDestory => m_onDestory;

        /// <summary>
        /// 障害物の初期化処理
        /// </summary>
        public abstract void InitObstacle();

        /// <summary>
        /// 障害物の破壊アニメーションの開始
        /// </summary>
        public abstract void DestoryAnimeStart();

        /// <summary>
        /// 障害物の破壊アニメーションの終了
        /// </summary>
        public void DestoryAnimeFinish() => Destroy(gameObject);
    }

}