using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtobeGame
{
    /// <summary>
    /// ステージの管理クラス
    /// </summary>
    public class StageManager : MonoBehaviour
    {
        // 次のステージへ移動させるフラグ
        private bool m_isNext = false;
        public bool isNext
        {
            set { m_isNext = value; }
            get { return m_isNext; }
        }

        // ゴール完了のフラグ
        private bool m_isGoal = false;
        public bool isGoal
        {
            set { m_isGoal = value; }
            get { return m_isGoal; }
        }

    }

}
