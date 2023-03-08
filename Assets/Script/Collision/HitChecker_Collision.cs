using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtobeLib
{
    /// <summary>
    /// 当たり判定の衝突を検知するクラス
    /// ※isTriggerがfalseのものにアタッチする
    /// </summary>
    public class HitChecker_Collision : HitChecker_Base
    {
        /// <summary>
        /// UnityEvent オブジェクトに触れたときに呼ばれる
        /// </summary>
        /// <param name="collision">当たり判定の情報</param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            //当たり判定を設定しているものと触れた瞬間に、フラグをtrueにする
            if (Find_ObjectTag(collision.gameObject.tag)) m_isCollision = true;
        }

        /// <summary>
        /// UnityEvent オブジェクトから離れたときに呼ばれる
        /// </summary>
        /// <param name="collision">当たり判定の情報</param>
        private void OnCollisionExit2D(Collision2D collision)
        {
            //当たり判定を設定しているものと離れた瞬間に、フラグをfalseにする
            if (Find_ObjectTag(collision.gameObject.tag)) m_isCollision = false;
        }
    }
}