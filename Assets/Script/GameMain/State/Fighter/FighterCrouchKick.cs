using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeLib;

namespace OtobeGame
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
            // FighterStateManagerを取得する
            FighterStateManager stateManager = owner.stateManager as FighterStateManager;

            //キックのアニメーションが終了したら、ステートを切り替える
            if (owner.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= m_finishAnime)
                stateManager.stateMachine.ChangeState(stateManager.crounchState);
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
