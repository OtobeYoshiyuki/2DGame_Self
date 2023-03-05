using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatusInfo
{
    /// <summary>
    /// ステータスの名前
    /// </summary>
    [SerializeField]
    private string m_tag = string.Empty;

    /// <summary>
    /// ステータスの値
    /// Inspecterから編集できるようにする
    /// </summary>
    [SerializeField]
    private float m_status = 0.0f;

    /// <summary>
    /// タグを取得する
    /// ゲッター
    /// </summary>
    public string tag { get { return m_tag; } }

    /// <summary>
    /// ステータスの配列を取得する
    /// ゲッター
    /// </summary>
    public float status { get { return m_status; } }
}

/// <summary>
/// スターのステータス
/// </summary>
[System.Serializable]
public class CharacterStatus
{
    /// <summary>
    /// ステータスの所有者のタグ
    /// Inspecterから編集できるようにする
    /// </summary>
    [SerializeField]
    private string m_ownerTag = string.Empty;

    /// <summary>
    /// ステータスのリスト
    /// Inspecterから編集できるようにする
    /// </summary>
    [SerializeField]
    private List<StatusInfo> m_statusList = new List<StatusInfo>();

    /// <summary>
    /// 所有者のタグを取得する
    /// ゲッター
    /// </summary>
    public string owner { get { return m_ownerTag; } }

    /// <summary>
    /// ステータスのリストを取得する
    /// ゲッター
    /// </summary>
    public List<StatusInfo> statusList { get { return m_statusList; } }
}
