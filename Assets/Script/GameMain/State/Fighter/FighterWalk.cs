using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeLib;

namespace OtobeGame
{
    /// <summary>
    /// ファイターの歩く状態
    /// </summary>
    [System.Serializable]
    public class FighterWalk : StateBase<Fighter>
    {
        //アニメーションの歩く速度
        //※移動速度にも影響する
        [SerializeField]
        private float m_defaltMotion = 0.0f;
        public float defaltMotion { get { return m_defaltMotion; } }

        //アニメーションの走る速度
        //※移動速度にも影響する
        [SerializeField]
        private float m_runMotion = 0.0f;
        public float runMotion { get { return m_runMotion; } }

        /// <summary>
        /// Stateの実行処理
        /// </summary>
        /// <param name="owner">インスタンスの所有者</param>
        public override void OnExecute(Fighter owner)
        {
            //キーによって方向を変える
            InputCs inputActions = Locater.Get<InputCs>();
            Vector2 direct = inputActions.Player.Move.ReadValue<Vector2>();

            //ジャンプに対応するキーが押された時
            if (inputActions.Player.Jump.WasPressedThisFrame())
                //ジャンプのステートに切り替える
                owner.stateMachine.ChangeState(owner.jumpState);

            //歩いているときに落下したとき
            else if (!owner.footCollider.isCollision)
                //落下のステートに切り替える
                owner.stateMachine.ChangeState(owner.fallState);

            //しゃがみに対応するキーが押された時
            else if(inputActions.Player.Crounch.WasPressedThisFrame())
                //しゃがみのステートに切り替える
                owner.stateMachine.ChangeState(owner.crounchState);

            //キーの入力が1or-1の時
            else if (!Mathf.Approximately(direct.x, 0.0f))
            {
                //ダッシュに割り当てられたキーが押されている間、アニメーションの速度を変更する
                if (inputActions.Player.Dash.IsPressed()) owner.animator.SetFloat("moveSpeed", m_runMotion);
                //キーが離されたら、アニメーションの速度をもとに戻す
                else owner.animator.SetFloat("moveSpeed", m_defaltMotion);

                //現在の歩きのアニメーションのモーションスピードを取得する
                float motionSpeed = owner.animator.GetFloat("moveSpeed");

                //キーの入力に合わせて、オブジェクトを反転させる
                owner.transform.localScale = new Vector3(direct.x * owner.scale.x, owner.scale.y, owner.scale.z);

                //Fighterを移動させる
                owner.rigidBody2D.velocity = new Vector2(owner.moveSpeed.x * direct.x * motionSpeed, owner.rigidBody2D.velocity.y);

                //ステートの時間を初期化する
                owner.time = 0.0f;
            }
            //キーの入力がないとき
            else
            {
                //ステートの時間を更新する
                owner.time += Time.deltaTime;

                //時間が一定値以上更新された場合は呼吸のステートに切り替える
                //※すぐに方向転換した場合は、ステートを移行しない
                if (owner.time >= owner.ideleState.changeTime) owner.stateMachine.ChangeState(owner.ideleState);
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
            Debug.Log("ウォークステート");

            //Fighterのアニメーションを歩行に切り替える
            owner.animator.SetInteger("Fighter_Anime", (int)Fighter.FITER_ANIMATION.WALK);

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
