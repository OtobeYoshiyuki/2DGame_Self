using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtobeLib
{
    /// <summary>
    /// 当たり判定の有無を検出するクラスのインターフェース
    /// </summary>
    public interface IHitChecker
    {
        /// <summary>
        /// 当たり判定を設定しているオブジェクトと当たったか調べる
        /// </summary>
        /// <param name="tag">対象のオブジェクトのタグ</param>
        /// <returns>結果：true or false</returns>
        bool CheckHitObject(string tag);

        /// <summary>
        /// 当たったオブジェクトを取得する
        /// </summary>
        /// <param name="tag">対象のオブジェクトのタグ</param>
        /// <returns>当たったオブジェクト</returns>
        GameObject FindHitObject(string tag);

        List<GameObject> FindHitObjects(string tag);
    }

    /// <summary>
    /// 当たり判定の有無を検知する抽象クラス
    /// </summary>
    public abstract class HitChecker : MonoBehaviour, IHitChecker
    {
        //当たり判定を取るべき相手オブジェクトを設定する
        [SerializeField]
        private List<string> m_objectsTag = new List<string>();

        protected bool m_isCollision = false;
        public bool isCollision { get { return m_isCollision; } }

        // 当たったオブジェクトの配列
        protected List<GameObject> m_hitObjects = new List<GameObject>();

        /// <summary>
        /// 指定したオブジェクトが当たったか確認する
        /// </summary>
        /// <param name="tag">指定したGameObjectのタグ</param>
        /// <returns>true=成功 false=失敗</returns>
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

        /// <summary>
        /// 当たったオブジェクトを取得する
        /// </summary>
        /// <param name="tag">指定したGameObjectのタグ</param>
        /// <returns>true=成功 false=失敗</returns>
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

        public List<GameObject> FindHitObjects(string tag)
        {
            // 指定したオブジェクトの当たった一覧
            List<GameObject> hitObjects = new List<GameObject>();

            foreach (GameObject hitObject in m_hitObjects)
            {
                // 探していたオブジェクトが見つかった場合
                if (hitObject.CompareTag(tag))
                {
                    // GameObjectの当たった配列に追加する
                    hitObjects.Add(hitObject);
                }
            }

            return hitObjects;
        }

        /// <summary>
        /// オブジェクトのタグ一覧から当たり判定が設定されているものかどうか検索する
        /// </summary>
        /// <param name="objectTag">オブジェクトのタグ</param>
        /// <returns>一覧にある場合はtrue、ない場合はfalse</returns>
        protected bool FindObjectTag(string objectTag)
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
