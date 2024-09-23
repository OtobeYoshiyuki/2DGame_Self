using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeLib;

namespace OtobeGame
{
    /// <summary>
    /// ステート（ジャンプの状態）
    /// </summary>
    [System.Serializable]
    public class FighterJump : StateBase<Fighter>
    {
        //ジャンプの力
        [SerializeField]
        private float m_jumpPower = 0.0f;

        /// <summary>
        /// Stateの実行処理
        /// </summary>
        /// <param name="owner">インスタンスの所有者</param>
        public override void OnExecute(Fighter owner)
        {
            // FighterStateManagerを取得する
            FighterStateManager stateManager = owner.stateManager as FighterStateManager;

            //キーから入力された値を取得する
            Vector2 direct = owner.control.OnMove();

            //キーの入力が1or-1の時
            if (!Mathf.Approximately(direct.x, 0.0f))
            {
                //ダッシュに割り当てられたキーが押されている間、アニメーションの速度を変更する
                if (owner.control.OnDash())
                {
                    owner.animator.SetFloat("moveSpeed", stateManager.walkState.runMotion);
                }
                //キーが離されたら、アニメーションの速度をもとに戻す
                else
                {
                    owner.animator.SetFloat("moveSpeed", stateManager.walkState.defaltMotion);
                }

                //現在の歩きのアニメーションのモーションスピードを取得する
                float motionSpeed = owner.animator.GetFloat("moveSpeed");

                //キーの入力に合わせて、オブジェクトを反転させる
                owner.transform.localScale = new Vector3(direct.x * owner.scale.x, owner.scale.y, owner.scale.z);

                //Fighterを移動させる
                owner.rigidBody2D.velocity = new Vector2(owner.moveSpeed.x * direct.x * motionSpeed, owner.rigidBody2D.velocity.y);

                //ステートの時間を初期化する
                owner.time = 0.0f;
            }

            //Fighterが落下し始めたら、ステートを切り替える
            if (owner.rigidBody2D.velocity.y <= stateManager.fallState.fallPower)
            {
                stateManager.stateMachine.ChangeState(stateManager.fallState);
            }
            //空中でキックに対応するキーが押された時
            else if (owner.control.OnKick())
            {
                // ステートを切り替える
                stateManager.stateMachine.ChangeState(stateManager.flyingKickState);
            }
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
            Debug.Log("ジャンプステート");

            //Fighterのアニメーションを呼吸に切り替える
            owner.animator.SetInteger("Fighter_Anime", (int)Fighter.FITER_ANIMATION.JUMP);

            //Stateの計測時間を初期化する
            owner.time = 0.0f;

            //ジャンプさせる
            owner.rigidBody2D.velocity = Vector2.zero;
            owner.rigidBody2D.AddForce(Vector2.up * m_jumpPower, ForceMode2D.Impulse);
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
