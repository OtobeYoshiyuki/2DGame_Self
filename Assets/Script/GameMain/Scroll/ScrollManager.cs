using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtobeLib
{
    /// <summary>
    /// 画面スクロールの管理クラス
    /// </summary>
    public class ScrollManager
    {
        // 上下のスクロール時に指定する値
        public const int UP_MOVE = 1;
        public const int DOWN_MOVE = -1;

        // メニュー一覧の親
        private GameObject m_menyuOwner = null;

        // メニューの一覧
        private List<GameObject> m_menyus = new List<GameObject>();

        // 頂点座標
        private Vector3 m_topPosition = Vector3.zero;

        // 終点座標
        private Vector3 m_botomPosition = Vector3.zero;

        // 上スクロールの基準値
        private int m_topIndex = -1;

        // 下スクロールの基準値
        private int m_botomIndex = -1;

        // 1スクロールの移動量
        private float m_oneScroll = 0.0f;

        // スクロール可能範囲
        private int m_scrollScope = -1;

        /// <summary>
        /// スクロールの初期化処理
        /// </summary>
        /// <param name="top">頂点座標</param>
        /// <param name="botom">終点座標</param>
        /// <param name="topIndex">上スクロールのインデックス</param>
        /// <param name="botomIndex">下スクロールのインデックス</param>
        /// <param name="menyuOwner">メニューの親</param>
        /// <param name="menyus">メニュー一覧</param>
        /// <param name="oneScroll">1スクロールの移動量</param>
        /// <param name="scrollScope">スクロール可能範囲</param>
        public void InitScroll(Vector3 top, Vector3 botom, int topIndex, int botomIndex,
            GameObject menyuOwner, List<GameObject> menyus, float oneScroll, int scrollScope)
        {
            m_topPosition = top;
            m_botomPosition = botom;
            m_topIndex = topIndex;
            m_botomIndex = botomIndex;
            m_menyuOwner = menyuOwner;
            m_menyus = menyus;
            m_oneScroll = oneScroll;
            m_scrollScope = scrollScope;
        }

        /// <summary>
        /// 上キーを押したときに呼ばれる処理
        /// </summary>
        /// <param name="index">対象のインデックス</param>
        /// <param name="maxScope">移動可能範囲の最大値</param>
        /// <param name="minScope">移動可能範囲の最小値</param>
        public void UpMove(ref int index, int maxScope, int minScope)
        {
            // メニューが空のとき、何もしない
            if (m_menyus.Count == 0) return;

            // 上スクロールのインデックスが参照外のとき、何もしない
            if (m_topIndex == -1) return;

            // スクロールの可動範囲が範囲外のとき、何もしない
            if (m_scrollScope == -1) return;

            index--;
            // 一番上の項目で上キーを押されたとき
            if (index < 0)
            {
                // リセットをかけて、画面下部まで移動する
                ResetUp(ref index, maxScope);
            }
            // 下スクロールが可能なとき
            else if (m_scrollScope != minScope && index <= m_topIndex)
            {
                // 下画面にスクロールする
                OnScrollMove(DOWN_MOVE);
            }
        }

        /// <summary>
        /// 画面下部まで移動するときに呼ばれる処理
        /// </summary>
        /// <param name="index">対象のインデックス</param>
        /// <param name="maxScope">移動可能範囲の最大値</param>
        private void ResetUp(ref int index, int maxScope)
        {
            index = m_menyus.Count - 1;
            m_scrollScope = maxScope;
            m_menyuOwner.transform.localPosition = m_botomPosition;
        }

        /// <summary>
        /// 下キーを押したときに呼ばれる処理
        /// </summary>
        /// <param name="index">対象のインデックス</param>
        /// <param name="maxScope">移動可能範囲の最大値</param>
        /// <param name="minScope">移動可能範囲の最小値</param>
        public void DownMove(ref int index, int maxScope, int minScope)
        {
            // メニューが空のとき、何もしない
            if (m_menyus.Count == 0) return;

            // 上スクロールのインデックスが参照外のとき、何もしない
            if (m_botomIndex == -1) return;

            // スクロールの可動範囲が範囲外のとき、何もしない
            if (m_scrollScope == -1) return;

            index++;
            // 一番下の項目で下キーを押されたとき
            if (index > m_menyus.Count - 1)
            {
                // リセットをかけて、画面上部まで移動する
                ResetDown(ref index, minScope);
            }
            // 上スクロールが可能なとき
            else if (m_scrollScope != maxScope && index >= m_botomIndex)
            {
                // 上画面へスクロールする
                OnScrollMove(UP_MOVE);
            }
        }

        /// <summary>
        /// 画面上部まで移動するときに呼ばれる処理
        /// </summary>
        /// <param name="index">対象のインデックス</param>
        /// <param name="minScope">移動可能範囲の最小値</param>
        private void ResetDown(ref int index, int minScope)
        {
            index = 0;
            m_scrollScope = minScope;
            m_menyuOwner.transform.localPosition = m_topPosition;
        }

        /// <summary>
        /// 画面のスクロール時に呼ばれる処理
        /// </summary>
        /// <param name="move">1:Up -1:Down</param>
        private void OnScrollMove(int move)
        {
            m_scrollScope += move;
            m_menyuOwner.transform.position += new Vector3(0.0f, m_oneScroll * move, 0.0f);
        }
    }
}
