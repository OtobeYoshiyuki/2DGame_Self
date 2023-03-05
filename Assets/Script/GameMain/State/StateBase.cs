using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stateの基底クラス
/// ジェネリック型
/// 制約：StateBaseを継承しなければならない
/// </summary>
namespace OtobeLib
{
    public abstract class StateBase<T> where T : class
    {
        /// <summary>
        /// Stateの実行処理
        /// </summary>
        /// <param name="owner">インスタンスの所有者</param>
        public virtual void OnExecute(T owner) { }

        /// <summary>
        /// Stateの実行処理
        /// ※物理演算を伴う更新
        /// </summary>
        /// <param name="owner">インスタンスの所有者</param>
        public virtual void OnFixedExecute(T owner) { }

        /// <summary>
        /// Stateの実行処理
        /// ※Update後に呼ばれる処理
        /// </summary>
        /// <param name="owner">インスタンスの所有者</param>
        public virtual void OnLateExecute(T owner) { }

        /// <summary>
        /// Stateの開始処理
        /// </summary>
        /// <param name="owner">インスタンスの所有者</param>
        /// <param name="preState">前回のステート</param>
        public virtual void OnEnter(T owner, StateBase<T> preState) { }

        /// <summary>
        /// Stateが終了処理
        /// </summary>
        /// <param name="owner">インスタンスの所有者</param>
        /// <param name="nextState">次のState</param>
        public virtual void OnExit(T owner, StateBase<T> nextState) { }
    }
}
