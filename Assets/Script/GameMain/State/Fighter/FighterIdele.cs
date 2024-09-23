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
            // FighterStateManagerを取得する
            FighterStateManager stateManager = owner.stateManager as FighterStateManager;

            // FighterCollisionManagerを取得する
            FighterCollisionManager collisionManager = owner.collisionManager as FighterCollisionManager;

            // キーから入力された値を取得する
            Vector2 vel = owner.control.OnMove();

            // ジャンプに対応するキーが押された時
            if (owner.control.OnJump())
            {
                // ジャンプステートに切り替える
                stateManager.stateMachine.ChangeState(stateManager.jumpState);
            }
            // しゃがみに対応するキーが押された時
            else if (owner.control.OnCrounch())
            {
                // しゃがみステートに切り替える
                stateManager.stateMachine.ChangeState(stateManager.crounchState);
            }
            // 左右の矢印が押された時
            else if (!Mathf.Approximately(vel.x, 0.0f))
            {
                // 移動ステートに切り替える
                stateManager.stateMachine.ChangeState(stateManager.walkState);
            }
            //滑って落ちたら、ステートを切り替える
            else if (!owner.IsFloor(collisionManager.footCollider))
            {
                stateManager.stateMachine.ChangeState(stateManager.fallState);
            }
            //パンチに対応するキーが押された時、ステートを切り替える
            else if (owner.control.OnPunch())
            {
                stateManager.stateMachine.ChangeState(stateManager.punchState);
            }
            //キックに対応するキーが押された時、ステートを切り替える
            else if (owner.control.OnKick())
            {
                stateManager.stateMachine.ChangeState(stateManager.kickState);
            }
            else if (collisionManager.bodyCollider.CheckHitObject("Door"))
            {
                Debug.Log("ドア");
            }
        }

        /// <summary>
        /// Stateの実行処理
        /// ※物理演算を伴う更新
        /// </summary>
        /// <param name="owner">インスタンスの所有者</param>
        public override void OnFixedExecute(Fighter owner)
        {
            //移動速度を初期化する
            owner.rigidBody2D.velocity = Vector2.zero;
            owner.rigidBody2D.angularVelocity = 0.0f;
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
            Debug.Log("アイドルステート");

            // FighterStateManagerを取得する
            FighterStateManager stateManager = owner.stateManager as FighterStateManager;

            //Fighterのアニメーションを呼吸に切り替える
            owner.animator.SetInteger("Fighter_Anime", (int)Fighter.FITER_ANIMATION.IDLE);
            owner.animator.SetFloat("moveSpeed", stateManager.walkState.defaltMotion);

            //Stateの計測時間を初期化する
            owner.time = 0.0f;

            // OptionSceneへの切り替えを許可する
            PlayScene playScene = Locater.Get<PlayScene>();
            playScene.playChange = true;
        }

        /// <summary>
        /// Stateが終了処理
        /// </summary>
        /// <param name="owner">インスタンスの所有者</param>
        /// <param name="nextState">次のState</param>
        public override void OnExit(Fighter owner, StateBase<Fighter> nextState) 
        {
            // OptionSceneへの切り替えを不許可にする
            PlayScene playScene = Locater.Get<PlayScene>();
            playScene.playChange = false;
        }
    }
}
