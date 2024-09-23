using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステータスのコントローラー
/// </summary>
public class StatusController 
{
    /// ステータスの連想配列
    /// 変動するステータスのみアクセス可能(変動しないものはデータベースが管理している)
    private Dictionary<string, float> m_status = new Dictionary<string, float>();

    /// <summary>
    /// 設定するステータスを登録する
    /// </summary>
    /// <param name="infos">ステータスの情報を持った配列</param>
    public void AddStatuses(List<StatusInfo> infos)
    {
        foreach(StatusInfo info in infos)
        {
            //該当するステータスを設定する
            m_status.Add(info.tag, info.status);
        }
    }

    /// <summary>
    /// 対象のステータスを設定する
    /// </summary>
    /// <param name="key">ステータスのkey</param>
    /// <param name="value">ステータスのvalue</param>
    public void SetTargetStatus(string key,float value) { m_status[key] = value; }

    /// <summary>
    /// 対象のステータスを取得する
    /// </summary>
    /// <param name="key">ステータスのkey</param>
    /// <returns>ステータスを返す</returns>
    public float GetTargetStatus(string key) { return m_status[key]; }
}
