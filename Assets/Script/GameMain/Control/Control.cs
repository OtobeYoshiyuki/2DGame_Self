using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeGame;

namespace OtobeLib
{
    /// <summary>
    /// 入力を制御するインターフェース
    /// </summary>
    public interface IControl
    {
        /// <summary>
        /// 制御クラスの実行関数
        /// </summary>
        public void OnExecute();

        /// <summary>
        /// 制御クラスの移動処理
        /// </summary>
        /// <returns>1フレーム当たりの移動量</returns>
        public Vector2 OnMove();

        /// <summary>
        /// 制御クラスの加速処理
        /// </summary>
        /// <returns>ダッシュの可否</returns>
        public bool OnDash();

        /// <summary>
        /// 制御クラスのジャンプ処理
        /// </summary>
        /// <returns>ジャンプの可否</returns>
        public bool OnJump();

        /// <summary>
        /// 制御クラスのしゃがみ処理
        /// </summary>
        /// <returns>しゃがみの可否</returns>
        public bool OnCrounch();

        /// <summary>
        /// 制御クラスのパンチ処理
        /// </summary>
        /// <returns>パンチの可否</returns>
        public bool OnPunch();

        /// <summary>
        /// 制御クラスのキック処理
        /// </summary>
        /// <returns>キックの可否</returns>
        public bool OnKick();
    }
}
