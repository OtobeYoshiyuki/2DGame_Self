using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 有限StateMachine
/// ジェネリック型（テンプレートにはStateの所有者の型を入れる）
/// </summary>
/// <typeparam name="T">StateBaseを継承した型を入れる</typeparam>
namespace OtobeLib
{
    public class StateMachine<T> where T : class
    {
        //Stateの所有者
        private T m_owner = null;

        //現在のState
        private StateBase<T> m_currentState = null;

        //1つ前のState
        private StateBase<T> m_preState = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public StateMachine(T owner, StateBase<T> newState)
        {
            //インスタンスの所有者を設定（引数を省略するため）
            m_owner = owner;

            //現在のStateを設定
            m_currentState = newState;

            //現在のStateの開始処理
            m_currentState?.OnEnter(m_owner, m_preState);
        }

        /// <summary>
        /// 有限StateMachineの更新
        /// </summary>
        public void UpdateState()
        {
            //現在のStateを実行
            m_currentState?.OnExecute(m_owner);
        }

        /// <summary>
        /// 有限StateMachineの更新
        /// ※物理演算を伴う更新
        /// </summary>
        public void FixedUpdateState()
        {
            //現在のStateを実行
            m_currentState?.OnFixedExecute(m_owner);
        }

        /// <summary>
        /// 有限StateMachineの更新
        /// ※Update後に呼ばれる処理
        /// </summary>
        public void LateUpdateState()
        {
            //現在のStateを実行
            m_currentState?.OnLateExecute(m_owner);
        }

        /// <summary>
        /// Stateを切り替える
        /// </summary>
        /// <param name="nextState">新しく設定するState</param>
        public void ChangeState(StateBase<T> nextState)
        {
            //現在のStateの終了処理（引数に次のStateを渡す）
            m_currentState?.OnExit(m_owner, nextState);

            //次のステートの開始処理（引数に1つ前のStateを渡す）
            nextState?.OnEnter(m_owner, m_currentState);

            //1つ前のStateを設定する
            m_preState = m_currentState;

            //現在のStateを設定する
            m_currentState = nextState;
        }

        /// <summary>
        /// 現在のStateを取得
        /// </summary>
        public StateBase<T> currentState { get { return m_currentState; } }

        /// <summary>
        /// 1つ前のStateを取得
        /// </summary>
        public StateBase<T> preState { get { return m_preState; } }
    }
}
