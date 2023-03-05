using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtobeLib
{
    /// <summary>
    /// クラスに記述する共通処理
    /// </summary>
    public interface IGameCommon
    {
        /// <summary>
        /// 処理化処理
        /// </summary>
        public void Initialize();

        /// <summary>
        /// 更新処理
        /// </summary>
        public void Update();
    }
}
