using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeLib;

namespace OtobeGame
{
    public class ExBlockController : MonoBehaviour
    {
        // アニメーションの終了を判定するフラグ
        private bool m_finishAnime = false;
        public bool finishAnime { get { return m_finishAnime; } }

        /// <summary>
        /// アニメーションの終了時に呼ばれる処理
        /// </summary>
        public void FinishAnimation()
        {
            m_finishAnime = true;
            Destroy(gameObject);
        }
    }
}
