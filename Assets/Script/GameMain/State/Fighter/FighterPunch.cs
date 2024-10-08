﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeLib;
using UnityEngine.InputSystem;

namespace OtobeGame
{
    /// <summary>
    /// ステート（パンチの状態）
    /// </summary>
    [System.Serializable]
    public class FighterPunch : StateBase<Fighter>
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

            // FighterCollisionManagerを取得する
            FighterCollisionManager collisionManager = owner.collisionManager as FighterCollisionManager;

            // パンチのアニメーションが終了
            if (owner.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= m_finishAnime)
            {
                // ステートを切り替える
                stateManager.stateMachine.ChangeState(stateManager.ideleState);
            }
            //パンチ中に落下したら、ステートを切り替える
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
            Debug.Log("パンチステート");

            // FighterStateManagerを取得する
            FighterStateManager stateManager = owner.stateManager as FighterStateManager;

            //Fighterのアニメーションを呼吸に切り替える
            owner.animator.SetInteger("Fighter_Anime", (int)Fighter.FITER_ANIMATION.PUNCH);
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
