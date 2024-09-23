using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeGame;

namespace OtobeLib
{
    /// <summary>
    /// ステートの管理クラスのインターフェース
    /// </summary>
    public interface IStateManager
    {
        /// <summary>
        /// ステートの初期化処理
        /// </summary>
        /// <param name="owner">ステートの所有者</param>
        void InitState(ICharacter owner);

        /// <summary>
        /// 更新時に呼ばれる処理
        /// </summary>
        void UpdateState();

        /// <summary>
        /// 物理演算の更新時に呼ばれる処理
        /// </summary>
        void FixedUpdateState();

        /// <summary>
        /// 更新後に呼ばれる処理
        /// </summary>
        void LateUpdateState();
    }
}
