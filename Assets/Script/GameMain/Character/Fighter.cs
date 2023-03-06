using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeGame;
using OtobeLib;

/// <summary>
/// 格闘家のキャラクター
/// Heroクラスを継承
/// </summary>
public class Fighter : Hero
{
    public enum FITER_ANIMATION
    {
        NONE = 0,       //無
        IDLE,           //呼吸
        WALK,           //歩く
    }

    //状態遷移を管理するステートマシーン
    private StateMachine<Fighter> m_stateMachine = null;
    public StateMachine<Fighter> stateMachine { get { return m_stateMachine; } }

    //ステート（呼吸の状態）
    private FighterIdel m_idelState = null;
    public FighterIdel idelState { get { return m_idelState; } }

    //ステート（歩く状態）
    private FighterWalk m_walkState = null;
    public FighterWalk walkState { get { return m_walkState; } }


    /// <summary>
    /// キャラクターの初期化
    /// </summary>
    public override void InitCharacter() 
    {
        //キャラクターの初期化処理
        base.InitCharacter();

        //処理の順番を設定する
        orderBy = CharaManager.ORDER_CHARACTER.FIGHTER;

        //各ステートを生成する
        m_idelState = new FighterIdel();
        m_walkState = new FighterWalk();

        //ステートマシーンを生成する
        m_stateMachine = new StateMachine<Fighter>(this, m_idelState);
    }

    /// <summary>
    /// キャラクターの更新
    /// </summary>
    public override void UpdateCharacter()
    {
        //ステートマシーンの更新処理
        m_stateMachine.UpdateState();

        //キャラクターの更新処理
        base.UpdateCharacter();
    }

    /// <summary>
    /// キャラクターの更新
    /// ※物理演算を伴う更新
    /// </summary>
    public override void FixedUpdateCharacter()
    {
        //ステートマシーンの更新処理
        m_stateMachine.FixedUpdateState();

        //キャラクターの更新処理
        base.FixedUpdateCharacter();
    }

    /// <summary>
    /// キャラクターの更新
    /// Updateの後に呼ばれる処理
    /// </summary>
    public override void LateUpdateCharacter()
    {
        //ステートマシーンの更新処理
        m_stateMachine.LateUpdateState();

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
