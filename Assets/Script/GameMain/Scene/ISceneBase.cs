using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtobeLib
{
    /// <summary>
    /// シーンのベースとなるインターフェース
    /// </summary>
    public interface ISceneBase
    {
        /// <summary>
        /// 初期化処理
        /// </summary>
        public void Init();

        /// <summary>
        /// 更新処理
        /// </summary>
        public void Update();

        /// <summary>
        /// 更新処理
        /// ※物理演算を伴う更新
        /// </summary>
        public void FixedUpdate();

        /// <summary>
        /// 更新処理
        /// カメラの処理などを記述
        /// </summary>
        public void LateUpdate();

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Final();
    }

}
