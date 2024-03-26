using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeGame;

namespace OtobeLib
{
    /// <summary>
    /// ステート（しゃがみ蹴りの状態）
    /// </summary>
    [System.Serializable]
    public class FighterCrouchKick : StateBase<Fighter>
    {
        //アニメーションの終了する時間
        [SerializeField]
        private float m_finishAnime = 1.0f;

        /// <summary>
        /// Stateの実行処理
        /// </summary>
        /// <param name="owner">インスタンスの所有者</param>
        public override void OnExecute(Fighter owner)
        {
            //キックのアニメーションが終了したら、ステートを切り替える
            if (owner.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= m_finishAnime)
                owner.stateMachine.ChangeState(owner.crounchState);

            // 爆発物に当たったとき
            else if (owner.kickCollider.CheckHitObject(ExBlockManager.EXPROSION_TAG))
            {
                // ブロックを爆発させる
                owner.ExBlockBrakeStart(owner.kickCollider);
            }
        }

        /// <summary>
        /// Stateの実行処理
        /// ※物理演算を伴う更新
        /// </summary>
        /// <param name="owner">インスタンスの所有者</param>
        public override void OnFixedExecute(Fighter owner)
        {
        }

        /// <summary>
        /// Stateの実行処理
        /// ※Update後に呼ばれる処理
        /// </summary>
        /// <param name="owner">インスタンスの所有者</param>
        public override void OnLateExecute(Fighter owner) { }

        /// <summary>
        /// Stateの開始処理
        /// </summary>
        /// <param name="owner">インスタンスの所有者</param>
        /// <param name="preState">前回のステート</param>
        public override void OnEnter(Fighter owner, StateBase<Fighter> preState)
        {
            Debug.Log("しゃがみ蹴りステート");

            //Fighterのアニメーションをしゃがみ蹴りに切り替える
            owner.animator.SetInteger("Fighter_Anime", (int)Fighter.FITER_ANIMATION.CROUCHKICK);

            //Stateの計測時間を初期化する
            owner.time = 0.0f;
        }

        /// <summary>
        /// Stateが終了処理
        /// </summary>
        /// <param name="owner">インスタンスの所有者</param>
        /// <param name="nextState">次のState</param>
        public override void OnExit(Fighter owner, StateBase<Fighter> nextState) { }
    }

}
