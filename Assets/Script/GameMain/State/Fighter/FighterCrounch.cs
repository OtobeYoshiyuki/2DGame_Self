﻿using System.Collections;
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
            // FighterStateManagerを取得する
            FighterStateManager stateManager = owner.stateManager as FighterStateManager;

            // FighterCollisionManagerを取得する
            FighterCollisionManager collisionManager = owner.collisionManager as FighterCollisionManager;

            // しゃがみに対応するキーが離された時は、ステートを切り替える
            if (!owner.control.OnCrounch())
            {
                stateManager.stateMachine.ChangeState(stateManager.ideleState);
            }
            // キックに対応するキーが押された時、ステートを切り替える
            else if (owner.control.OnKick())
            {
                stateManager.stateMachine.ChangeState(stateManager.crouchKickState);
            }
            // 滑って落ちたら、ステートを切り替える
            else if (!owner.IsFloor(collisionManager.footCollider))
            {
                stateManager.stateMachine.ChangeState(stateManager.fallState); 
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
            Debug.Log("しゃがみステート");

            // FighterStateManagerを取得する
            FighterStateManager stateManager = owner.stateManager as FighterStateManager;

            //Fighterのアニメーションをしゃがみに切り替える
            owner.animator.SetInteger("Fighter_Anime", (int)Fighter.FITER_ANIMATION.CROUCH);
            owner.animator.SetFloat("moveSpeed", stateManager.walkState.defaltMotion);

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
