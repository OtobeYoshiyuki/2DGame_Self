using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeGame;

namespace OtobeGame
{
    /// <summary>
    /// 操作キャラ
    /// </summary>
    public abstract class Hero : Character
    {
        /// <summary>
        /// 操作キャラの初期化
        /// </summary>
        public override void InitCharacter()
        {
            //操作キャラの初期化処理
            base.InitCharacter();
        }

        /// <summary>
        /// 操作キャラの更新
        /// </summary>
        public override void UpdateCharacter()
        {
            //操作キャラの更新処理
            base.UpdateCharacter();
        }

        /// <summary>
        /// 操作キャラの更新
        /// ※物理演算を伴う更新
        /// </summary>
        public override void FixedUpdateCharacter()
        {
            //操作キャラの更新処理
            base.FixedUpdateCharacter();
        }

        /// <summary>
        /// 操作キャラの更新
        /// Updateの後に呼ばれる処理
        /// </summary>
        public override void LateUpdateCharacter()
        {
            //操作キャラの更新処理
            base.LateUpdateCharacter();
        }

        /// <summary>
        /// 死亡時に呼ばれる処理
        /// </summary>
        public override void FinalCharacter()
        {
            //操作キャラの終了処理
            base.FinalCharacter();
        }

    }

}
