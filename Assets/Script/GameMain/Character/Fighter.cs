using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeGame;

/// <summary>
/// 格闘家のキャラクター
/// Heroクラスを継承
/// </summary>
public class Fighter : Hero
{
    public enum FITER_ANIMATION
    {
        IDLE = 0,       //呼吸
        WALK,           //歩く
    }
    /// <summary>
    /// キャラクターの初期化
    /// </summary>
    public override void InitCharacter() 
    {
        //キャラクターの初期化処理
        base.InitCharacter();

        //処理の順番を設定する
        orderBy = CharaManager.ORDER_CHARACTER.FIGHTER;
    }

    /// <summary>
    /// キャラクターの更新
    /// </summary>
    public override void UpdateCharacter()
    {
        //キャラクターの更新処理
        base.UpdateCharacter();
    }

    /// <summary>
    /// キャラクターの更新
    /// ※物理演算を伴う更新
    /// </summary>
    public override void FixedUpdateCharacter()
    {
        //キャラクターの更新処理
        base.FixedUpdateCharacter();
    }

    /// <summary>
    /// キャラクターの更新
    /// Updateの後に呼ばれる処理
    /// </summary>
    public override void LateUpdateCharacter()
    {
        //キャラクターの更新処理
        base.LateUpdateCharacter();
    }

    /// <summary>
    /// 死亡時に呼ばれる処理
    /// </summary>
    public override void FinalCharacter()
    {
        //キャラクターの終了処理
        base.FinalCharacter();
    }
}
