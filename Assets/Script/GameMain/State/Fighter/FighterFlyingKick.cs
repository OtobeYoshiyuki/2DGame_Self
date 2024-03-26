using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeGame;
using UnityEngine.InputSystem;

namespace OtobeLib
{
    /// <summary>
    /// ステート（キックの状態）
    /// </summary>
    [System.Serializable]
    public class FighterFlyingKick : StateBase<Fighter>
    {
        //攻撃時間
        [SerializeField]
        private float m_atackTime = 2.0f;

        //攻撃する力
        [SerializeField]
        private Vector2 m_kickPower = Vector2.zero;

        //攻撃判定用のフラグ
        private bool m_isAtack = false;
        public bool isAtack { get { return m_isAtack; } set { m_isAtack = value; } }

        /// <summary>
        /// Stateの実行処理
        /// </summary>
        /// <param name="owner">インスタンスの所有者</param>
        public override void OnExecute(Fighter owner)
        {
            // 爆発物に当たったとき
            if (owner.kickCollider.CheckHitObject(ExBlockManager.EXPROSION_TAG))
            {
                owner.ExBlockBrakeStart(owner.kickCollider);
            }
            //敵に攻撃が当たるか時間が経過したときにステートを切り替える
            else if (owner.kickCollider.isCollision || owner.footCollider.CheckHitObject("Stage") ||
                owner.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= m_atackTime)
            {
                owner.stateMachine.ChangeState(owner.fallState);
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
            Debug.Log("空中蹴りステート");

            //Fighterのアニメーションを呼吸に切り替える
            owner.animator.SetInteger("Fighter_Anime", (int)Fighter.FITER_ANIMATION.FLYINGKICK);
            owner.animator.SetFloat("moveSpeed", owner.walkState.defaltMotion);

            //移動速度を初期化する
            owner.rigidBody2D.velocity = Vector2.zero;
            owner.rigidBody2D.angularVelocity = 0.0f;

            //移動方向の斜め下に攻撃する
            owner.rigidBody2D.AddForce(new Vector2(m_kickPower.x * Mathf.Clamp(owner.transform.localScale.x, -1, 1), m_kickPower.y));

            //Stateの計測時間を初期化する
            owner.time = 0.0f;
        }

        /// <summary>
        /// Stateが終了処理
        /// </summary>
        /// <param name="owner">インスタンスの所有者</param>
        /// <param name="nextState">次のState</param>
        public override void OnExit(Fighter owner, StateBase<Fighter> nextState)
        {
        }
    }

}
