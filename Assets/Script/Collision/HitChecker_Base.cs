using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtobeLib
{
    /// <summary>
    /// 当たり判定の有無を検知するクラス
    /// ※抽象クラス
    /// </summary>
    public abstract class HitChecker_Base : MonoBehaviour
    {
        //当たり判定を取るべき相手オブジェクトを設定する
        [SerializeField]
        private List<string> m_objectsTag = new List<string>();

        //オブジェクトに当たったかどうか検知するフラグ
        //※衝突したらぶつかるものを検知する
        protected bool m_isCollision = false;
        public bool isCollision { get { return m_isCollision; } }

        /// <summary>
        /// オブジェクトのタグ一覧から当たり判定が設定されているものかどうか検索する
        /// </summary>
        /// <param name="objectTag">オブジェクトのタグ</param>
        /// <returns>一覧にある場合はtrue、ない場合はfalse</returns>
        protected bool Find_ObjectTag(string objectTag)
        {
            foreach (string tag in m_objectsTag)
            {
                //探していたものが見つかった場合はtrueを返す
                if (tag == objectTag) return true;
            }
            //見つからなかった場合はfalseを返す
            return false;
        }
    }
}
