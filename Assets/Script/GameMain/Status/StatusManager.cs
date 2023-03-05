using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeGame;

/// <summary>
/// ステータスの管理クラス
/// </summary>
[System.Serializable]
public class StatusManager : MonoBehaviour
{
    //ゲームオブジェクトの名前
    public const string OBJECT_NAME = "StatusManager";

    // ステータスのデータベース
    [SerializeField]
    private StatusDataBase m_dataBase = new StatusDataBase();

    // ステータスのリスト
    [SerializeField]
    private List<CharacterStatus> m_statusList = new List<CharacterStatus>();

    // キャラクターごとのステータスの連想配列
    private Dictionary<string, List<StatusInfo>> m_charStatusList = new Dictionary<string, List<StatusInfo>>();

    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Init()
    {
        //データベースの初期化を行う
        m_dataBase.InitDataBase();

        foreach(CharacterStatus status in m_statusList)
        {
            //Inspecterで設定したステータスを連想配列に登録する
            m_charStatusList.Add(status.owner, status.statusList);
        }
    }

    public void OnDamageStatus(StatusController self,StatusController other)
    {
        //ダメージ計算を行う
        float newHP = CaclHpStatus(
            self.GetTargetStatus(Character.HP),//自身のHPを第1引数に設定
            other.GetTargetStatus(Character.ATACK),//相手の攻撃力を第2引数に
            dataBase.MaxStatus(Character.DEFENCE)//相手の守備力を第3引数に
            );

        //新たなHPを再設定する
        self.SetTargetStatus(Character.HP, newHP);

    }

    /// <summary>
    /// ステータスにダメージを与える
    /// </summary>
    /// <param name="hp">体力</param>
    /// <param name="atack">攻撃力</param>
    /// <param name="defence">防御力</param>
    /// <returns>新たな体力</returns>
    public float CaclHpStatus(float hp,float atack,float defence)
    {
        //パラメーターから新たなHPを算出する
        return hp - (atack - defence);
    }

    /// <summary>
    /// ステータス用のデータベースを取得
    /// ゲッター
    /// </summary>
    public StatusDataBase dataBase { get { return m_dataBase; } }

    /// <summary>
    /// ステータスの配列を取得する
    /// </summary>
    /// <param name="key">ステータスの所有者のタグ</param>
    /// <returns>該当するステータスの配列</returns>
    public List<StatusInfo> GetStatusInfoArray(string key) { return m_charStatusList[key]; }
}
