using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtobeLib
{
    /// <summary>
    /// 当たり判定の衝突を検知するクラス
    /// ※isTriggerがtrueのものにアタッチする
    /// </summary>
    public class HitCheckerCollider : HitChecker
    {
        /// <summary>
        /// UnityEvent オブジェクトに触れたときに呼ばれる
        /// </summary>
        /// <param name="collision">当たり判定の情報</param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //当たり判定を設定しているものと当たった場合
            if (FindObjectTag(collision.gameObject.tag)) 
            {
                m_isCollision = true;

                // 当たったオブジェクトがリストにない場合
                if (!m_hitObjects.Contains(collision.gameObject))
                {
                    // 当たったオブジェクトを追加する
                    m_hitObjects.Add(collision.gameObject);
                }
            }
        }

        /// <summary>
        /// UnityEvent オブジェクトから離れたときに呼ばれる
        /// </summary>
        /// <param name="collision">当たり判定の情報</param>
        private void OnTriggerExit2D(Collider2D collision)
        {
            //当たり判定を設定しているものと離れた場合
            if (FindObjectTag(collision.gameObject.tag))
            {
                m_isCollision = false;

                // 当たったオブジェクトがリストにある場合
                if (m_hitObjects.Contains(collision.gameObject))
                {
                    // 当たったオブジェクトを削除する
                    m_hitObjects.Remove(collision.gameObject);
                }
            }
        }
    }
}