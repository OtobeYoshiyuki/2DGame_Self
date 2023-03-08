using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtobeLib
{
    /// <summary>
    /// 当たり判定の衝突を検知するクラス
    /// ※isTriggerがtrueのものにアタッチする
    /// </summary>
    public class HitChecker_Collider : HitChecker_Base
    {
        /// <summary>
        /// UnityEvent オブジェクトに触れたときに呼ばれる
        /// </summary>
        /// <param name="collision">当たり判定の情報</param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //当たり判定を設定しているものと触れた瞬間に、フラグをtrueにする
            if (Find_ObjectTag(collision.gameObject.tag)) m_isCollision = true;
        }

        /// <summary>
        /// UnityEvent オブジェクトから離れたときに呼ばれる
        /// </summary>
        /// <param name="collision">当たり判定の情報</param>
        private void OnTriggerExit2D(Collider2D collision)
        {
            //当たり判定を設定しているものと触れた瞬間に、フラグをtrueにする
            if (Find_ObjectTag(collision.gameObject.tag)) m_isCollision = false;
        }
    }
}