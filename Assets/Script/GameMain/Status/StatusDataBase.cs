using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtobeLib
{
    /// <summary>
    /// ステータスのデータベース
    /// </summary>
    [System.Serializable]
    public class StatusDataBase
    {
        // ステータスのデータベース
        [SerializeField]
        private List<StatusInfo> m_infoList = new List<StatusInfo>();

        // ステータスの最大値の連想配列
        private Dictionary<string, float> m_maxStatusList = new Dictionary<string, float>();

        /// <summary>
        /// ステータスのデータベースの初期化
        /// </summary>
        public void InitDataBase()
        {
            foreach (StatusInfo info in m_infoList)
            {
                //データベースの配列の中身を連想配列に設定する
                m_maxStatusList.Add(info.tag, info.status);
            }
        }

        /// <summary>
        /// 指定したkeyのステータスを取得する
        /// </summary>
        /// <param name="key">連想配列のkey</param>
        /// <returns>指定したステータス</returns>
        public float MaxStatus(string key) { return m_maxStatusList[key]; }
    }
}
