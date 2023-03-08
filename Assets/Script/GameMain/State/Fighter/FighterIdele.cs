using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeLib;

namespace OtobeGame
{
    /// <summary>
    /// ステート（呼吸の状態）
    /// </summary>
    [System.Serializable]
    public class FighterIdele : StateBase<Fighter>
    {
        //WalkStateからideleStateへ移行する時間
        [SerializeField]
        private float m_changeTime = 0.0f;
        public float changeTime { get { return m_changeTime; } }

        /// <summary>
        /// Stateの実行処理
        /// </summary>
        /// <param name="owner">インスタンスの所有者</param>
        public override void OnExecute(Fighter owner) 
        {
            //InputSystemを取得する
            InputCs inputActions = Locater.Get<InputCs>();

            //キーから入力された値を取得する
            Vector2 vel = inputActions.Player.Move.ReadValue<Vector2>();

            //ジャンプに対応するキーが押された時、ステートを切り替える
            if (inputActions.Player.Jump.WasPressedThisFrame()) owner.stateMachine.ChangeState(owner.jumpState);

            //しゃがみに対応するキーが押された時、ステートを切り替える
            else if (inputActions.Player.Crounch.WasPressedThisFrame()) owner.stateMachine.ChangeState(owner.crounchState);

            //左右の矢印が押された時、ステートを切り替える
            else if (!Mathf.Approximately(vel.x, 0.0f)) owner.stateMachine.ChangeState(owner.walkState);
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
            Debug.Log("アイドルステート");

            //Fighterのアニメーションを呼吸に切り替える
            owner.animator.SetInteger("Fighter_Anime", (int)Fighter.FITER_ANIMATION.IDLE);
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
