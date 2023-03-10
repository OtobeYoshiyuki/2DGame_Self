using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeLib;

namespace OtobeGame
{
    /// <summary>
    /// ステート（落下の状態）
    /// </summary>
    [System.Serializable]
    public class FighterFall : StateBase<Fighter>
    {
        //落下するベクトルの力
        [SerializeField]
        private float m_fallPower = 0.0f;
        public float fallPower { get { return m_fallPower; } }

        /// <summary>
        /// Stateの実行処理
        /// </summary>
        /// <param name="owner">インスタンスの所有者</param>
        public override void OnExecute(Fighter owner)
        {
            //キーから入力された値を取得する
            Vector2 direct = owner.playerInput.currentActionMap["Move"].ReadValue<Vector2>();

            //キーの入力が1or-1の時
            if (!Mathf.Approximately(direct.x, 0.0f))
            {
                //ダッシュに割り当てられたキーが押されている間、アニメーションの速度を変更する
                if (owner.playerInput.currentActionMap["Dash"].IsPressed()) 
                    owner.animator.SetFloat("moveSpeed", owner.walkState.runMotion);
                //キーが離されたら、アニメーションの速度をもとに戻す
                else owner.animator.SetFloat("moveSpeed", owner.walkState.defaltMotion);

                //現在の歩きのアニメーションのモーションスピードを取得する
                float motionSpeed = owner.animator.GetFloat("moveSpeed");

                //キーの入力に合わせて、オブジェクトを反転させる
                owner.transform.localScale = new Vector3(direct.x * owner.scale.x, owner.scale.y, owner.scale.z);

                //Fighterを移動させる
                owner.rigidBody2D.velocity = new Vector2(owner.moveSpeed.x * direct.x * motionSpeed, owner.rigidBody2D.velocity.y);

                //ステートの時間を初期化する
                owner.time = 0.0f;
            }

            //着地した
            if (owner.footCollider.isCollision)
            {
                //ステートを切り替える
                owner.stateMachine.ChangeState(owner.ideleState);

                //空中蹴りのフラグを初期化する
                owner.flyingKickState.isAtack = false;
            }

            //空中でキックに対応するキーが押されていて、まだ攻撃していないとき
            else if (owner.playerInput.currentActionMap["Kick"].WasPressedThisFrame() && !owner.flyingKickState.isAtack)
            {
                //ステートを切り替える
                owner.stateMachine.ChangeState(owner.flyingKickState);

                //攻撃フラグを立てる
                owner.flyingKickState.isAtack = true;
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
            Debug.Log("落下ステート");

            //Fighterのアニメーションを呼吸に切り替える
            owner.animator.SetInteger("Fighter_Anime", (int)Fighter.FITER_ANIMATION.FALL);

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
