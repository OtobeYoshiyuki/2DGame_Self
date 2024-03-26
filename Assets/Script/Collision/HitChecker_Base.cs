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

        protected bool m_isCollision = false;
        public bool isCollision { get { return m_isCollision; } }

        // 当たったオブジェクトの配列
        protected List<GameObject> m_hitObjects = new List<GameObject>();

        public bool CheckHitObject(string tag)
        {
            foreach (GameObject hitObject in m_hitObjects)
            {
                // 探していたオブジェクトが見つかった場合は、trueを返す
                if (hitObject.CompareTag(tag)) return true;
            }

            // 見つからなかった場合は、falseを返す
            return false;
        }

        public GameObject FindHitObject(string tag)
        {
            foreach (GameObject hitObject in m_hitObjects)
            {
                // 探していたオブジェクトが見つかった場合は、そのオブジェクトを返す
                if (hitObject.CompareTag(tag)) return hitObject;
            }

            // 見つからなかった場合は、nullを返す
            return null;
        }
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
