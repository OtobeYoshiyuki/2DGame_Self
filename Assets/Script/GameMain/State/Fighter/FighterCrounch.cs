using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeLib;

namespace OtobeGame
{
    /// <summary>
    /// ステート（しゃがむ状態）
    /// </summary>
    [System.Serializable]
    public class FighterCrounch : StateBase<Fighter>
    {
        /// <summary>
        /// Stateの実行処理
        /// </summary>
        /// <param name="owner">インスタンスの所有者</param>
        public override void OnExecute(Fighter owner)
        {
            //InputSystemを取得する
            InputCs inputActions = Locater.Get<InputCs>();

            //キーの入力が1or-1の時
            if (inputActions.Player.Move.WasPressedThisFrame()) owner.stateMachine.ChangeState(owner.walkState);

            //しゃがみに対応するキーが離された時は、ステートを切り替える
            else if (inputActions.Player.Crounch.WasReleasedThisFrame()) owner.stateMachine.ChangeState(owner.ideleState);
        }

        /// <summary>
        /// Stateの実行処理
        /// ※物理演算を伴う更新
        /// </summary>
        /// <param name="owner">インスタンスの所有者</param>
        public override void OnFixedExecute(Fighter owner) { }

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
            Debug.Log("しゃがみステート");

            //Fighterのアニメーションをしゃがみに切り替える
            owner.animator.SetInteger("Fighter_Anime", (int)Fighter.FITER_ANIMATION.CROUCH);
            owner.animator.SetFloat("moveSpeed", owner.walkState.defaltMotion);

            //移動速度を初期化する
            owner.rigidBody2D.velocity = Vector2.zero;
            owner.rigidBody2D.angularVelocity = 0.0f;

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
