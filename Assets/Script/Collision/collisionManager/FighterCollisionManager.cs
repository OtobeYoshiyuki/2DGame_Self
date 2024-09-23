using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeLib;

namespace OtobeGame
{
    /// <summary>
    /// Fighterの当たり判定管理クラス
    /// </summary>
    public class FighterCollisionManager : CollisionManager
    {
        //子供（足）の当たり判定を検出するクラス
        private HitCheckerCollider m_footCollider = null;
        public HitCheckerCollider footCollider { get { return m_footCollider; } }

        //子供（足の攻撃）の当たり判定を検出するクラス
        private HitCheckerCollider m_kickCollider = null;
        public HitCheckerCollider kickCollider { get { return m_kickCollider; } }

        // 子供(腕の攻撃)の当たり判定を検出するクラス
        private HitCheckerCollider m_armCollider = null;
        public HitCheckerCollider armCollider { get { return m_armCollider; } }

        // 子供(体)の当たり判定を検出するクラス
        private HitCheckerCollider m_bodyCollider = null;
        public override IHitChecker bodyCollider => m_bodyCollider;


        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="owner">キャラクター</param>
        public override void InitCollision(Character owner)
        {
            // ステートの管理クラスを取得する
            FighterStateManager fighterStateManager = owner.stateManager as FighterStateManager;

            // 子供にアタッチされている当たり判定用のスクリプトを取得する
            m_bodyCollider = owner.transform.GetChild((int)Fighter.CHILD_OBJECT.BODY).gameObject.GetComponent<HitCheckerCollider>();
            m_armCollider = owner.transform.GetChild((int)Fighter.CHILD_OBJECT.ARM).gameObject.GetComponent<HitCheckerCollider>();
            m_footCollider = owner.transform.GetChild((int)Fighter.CHILD_OBJECT.FOOT).gameObject.GetComponent<HitCheckerCollider>();
            m_kickCollider = owner.transform.GetChild((int)Fighter.CHILD_OBJECT.KICK).gameObject.GetComponent<HitCheckerCollider>();

            // 爆発ブロックの当たり判定を一覧に追加する
            AddHitChecker(StageManager.EXBLOCK_TAG, m_armCollider);
            AddHitChecker(StageManager.EXBLOCK_TAG, m_kickCollider);

            // 通常ブロックの当たり判定を一覧に追加する
            AddHitChecker(StageManager.BOX_TAG, m_armCollider);
            AddHitChecker(StageManager.BOX_TAG, m_kickCollider);
            //AddHitChecker(StageManager.BOX_TAG, m_footCollider, () => fighterStateManager.stateMachine.currentState == fighterStateManager.flyingKickState);
        }
    }
}
