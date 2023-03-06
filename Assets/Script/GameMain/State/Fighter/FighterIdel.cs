using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeLib;

namespace OtobeGame
{
    /// <summary>
    /// ステート（呼吸の状態）
    /// </summary>
    public class FighterIdel : StateBase<Fighter>
    {
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

            //左右の矢印が押された時、ステートを切り替える
            if (!Mathf.Approximately(vel.x, 0.0f)) owner.stateMachine.ChangeState(owner.walkState);
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


        }

        /// <summary>
        /// Stateが終了処理
        /// </summary>
        /// <param name="owner">インスタンスの所有者</param>
        /// <param name="nextState">次のState</param>
        public override void OnExit(Fighter owner, StateBase<Fighter> nextState) { }
    }
}
