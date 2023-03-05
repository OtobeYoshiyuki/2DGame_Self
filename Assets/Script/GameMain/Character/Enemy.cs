using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeGame;

namespace OtobeGame
{
    /// <summary>
    /// 敵キャラの基底クラス
    /// </summary>
    public abstract class Enemy : Character
    {
        /// <summary>
        /// キャラクターの初期化
        /// </summary>
        public override void InitCharacter()
        {
            //キャラクターの初期化処理
            base.InitCharacter();
        }

        /// <summary>
        /// キャラクターの更新
        /// </summary>
        public override void UpdateCharacter()
        {
            //キャラクターの更新処理
            base.UpdateCharacter();
        }

        /// <summary>
        /// キャラクターの更新
        /// ※物理演算を伴う更新
        /// </summary>
        public override void FixedUpdateCharacter()
        {
            //キャラクターの更新処理
            base.FixedUpdateCharacter();
        }

        /// <summary>
        /// キャラクターの更新
        /// Updateの後に呼ばれる処理
        /// </summary>
        public override void LateUpdateCharacter()
        {
            //キャラクターの更新処理
            base.LateUpdateCharacter();
        }

        /// <summary>
        /// 死亡時に呼ばれる処理
        /// </summary>
        public override void FinalCharacter()
        {
            //キャラクターの終了処理
            base.FinalCharacter();
        }
    }
}
