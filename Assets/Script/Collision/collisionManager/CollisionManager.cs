using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeGame;
using System;

namespace OtobeLib
{
    /// <summary>
    /// 当たり判定の管理クラスのインターフェース
    /// </summary>
    public interface ICollisionManager
    {
        // 体の当たり判定
        public IHitChecker bodyCollider { get; }

        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="owner">キャラクター</param>
        public void InitCollision(Character owner);

        /// <summary>
        /// 当たり判定の一覧を取得する
        /// </summary>
        /// <returns>当たり判定の一覧</returns>
        public List<ActionHitchecker> GetHitCheckers(string key);
    }

    // 当たり判定のインターフェースと実行関数を持つクラス
    public class ActionHitchecker
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="hitChecker">当たり判定のインターフェース</param>
        /// <param name="func">当たり判定の実行判定の関数オブジェクト</param>
        public ActionHitchecker(IHitChecker hitChecker, Func<bool> hitCondition)
        {
            // 当たり判定のインターフェースを設定する
            m_hitChecker = hitChecker;

            // 当たり判定の実行判定の関数オブジェクトを設定する
            m_hitCondition = hitCondition;
        }

        // 当たり判定のインターフェース
        private IHitChecker m_hitChecker = null;
        public IHitChecker hitChecker => m_hitChecker;

        // 当たり判定実行判定の関数オブジェクト
        private Func<bool> m_hitCondition = null;
        public Func<bool> hitCondition => m_hitCondition;
    }

    public abstract class CollisionManager : ICollisionManager
    {
        // 当たり判定のインターフェースと実行関数を持つ連想配列
        private Dictionary<string, List<ActionHitchecker>> m_hitcheckers = new Dictionary<string, List<ActionHitchecker>>();

        // 体の当たり判定
        public abstract IHitChecker bodyCollider { get; } 
        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="owner">キャラクター</param>
        public abstract void InitCollision(Character owner);

        /// <summary>
        /// 当たり判定を追加する
        /// </summary>
        /// <param name="key">連想配列のKey</param>
        /// <param name="hitChecker">当たり判定検出用のインターフェース</param>
        /// <param name="hitCondition">当たり判定の実行判定の関数オブジェクト</param>
        protected void AddHitChecker(string key, IHitChecker hitChecker, Func<bool> hitCondition = null)
        {
            // 当たり判定の実行判定を設定しない場合
            if (hitCondition == null)
            {
                // 常時許可状態にする
                hitCondition = () => true;
            }

            // 指定したキーのリスト情報が存在しないとき
            if (!m_hitcheckers.ContainsKey(key))
            {
                // 指定したキーのリストを生成する
                m_hitcheckers.Add(key, new List<ActionHitchecker>());
            }

            // 当たり判定の一覧に追加
            m_hitcheckers[key].Add(new ActionHitchecker(hitChecker, hitCondition));
        }

        /// <summary>
        /// 当たり判定の一覧を取得する
        /// </summary>
        /// <returns></returns>
        public List<ActionHitchecker> GetHitCheckers(string key)
        {
            // 当たり判定の一覧を返す
            return m_hitcheckers[key];
        }
    }
}
