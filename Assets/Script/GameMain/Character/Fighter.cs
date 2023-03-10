using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeGame;
using OtobeLib;
using UnityEngine.InputSystem;

/// <summary>
/// 格闘家のキャラクター
/// Heroクラスを継承
/// </summary>
public class Fighter : Hero
{
    //アニメーションの列挙体を用意する
    public enum FITER_ANIMATION
    {
        NONE = 0,       //無
        IDLE,           //呼吸
        WALK,           //歩く
        JUMP,           //ジャンプ
        FALL,           //落下
        CROUCH,         //しゃがむ
        PUNCH,          //パンチ
        KICK,           //キック
        CROUCHKICK,     //しゃがみ蹴り
        FLYINGKICK,     //とび膝蹴り
        HURT,           //ダメージを受ける＆死亡
    }

    //Fighterの子供の列挙体を定義
    public enum CHILD_OBJECT
    {
        BODY = 0,       //体の判定
        ARM,            //腕の攻撃判定
        FOOT,           //着地用の判定
        KICK,           //足の攻撃判定
    }

    //子供（足）の当たり判定を検出するクラス
    HitChecker_Collider m_footCollider = null;
    public HitChecker_Collider footCollider { get { return m_footCollider; } }

    //子供（足の攻撃）の当たり判定を検出するクラス
    HitChecker_Collider m_kickCollider = null;
    public HitChecker_Collider kickCollider { get { return m_kickCollider; } }

    //状態遷移を管理するステートマシーン
    private StateMachine<Fighter> m_stateMachine = null;
    public StateMachine<Fighter> stateMachine { get { return m_stateMachine; } }

    //ステート（呼吸の状態）
    [SerializeField]
    private FighterIdele m_ideleState = new FighterIdele();
    public FighterIdele ideleState { get { return m_ideleState; } }

    //ステート（歩く状態）
    [SerializeField]
    private FighterWalk m_walkState = new FighterWalk();
    public FighterWalk walkState { get { return m_walkState; } }

    //ステート（ジャンプ状態）
    [SerializeField]
    private FighterJump m_jumpState = new FighterJump();
    public FighterJump jumpState { get { return m_jumpState; } }

    //ステート（落下状態）
    [SerializeField]
    private FighterFall m_fallState = new FighterFall();
    public FighterFall fallState { get { return m_fallState; } }

    //ステート（しゃがむ状態）
    [SerializeField]
    private FighterCrounch m_crounchState = new FighterCrounch();
    public FighterCrounch crounchState { get { return m_crounchState; } }

    //ステート（パンチ状態）
    [SerializeField]
    private FighterPunch m_punchState = new FighterPunch();
    public FighterPunch punchState { get { return m_punchState; } }

    //ステート（キック状態）
    [SerializeField]
    private FighterKick m_kickState = new FighterKick();
    public FighterKick kickState { get { return m_kickState; } }

    //ステート（しゃがみ蹴り状態）
    [SerializeField]
    private FighterCrouchKick m_crouchKickState = new FighterCrouchKick();
    public FighterCrouchKick crouchKickState { get { return m_crouchKickState; } }

    //ステート（空中蹴り状態）
    [SerializeField]
    private FighterFlyingKick m_flyingKickState = new FighterFlyingKick();
    public FighterFlyingKick flyingKickState { get { return m_flyingKickState; } }


    /// <summary>
    /// キャラクターの初期化
    /// </summary>
    public override void InitCharacter() 
    {
        //キャラクターの初期化処理
        base.InitCharacter();

        //処理の順番を設定する
        orderBy = CharaManager.ORDER_CHARACTER.FIGHTER;

        //ステートマシーンを生成する
        m_stateMachine = new StateMachine<Fighter>(this, m_ideleState);

        //子供にアタッチされている当たり判定用のスクリプトを取得する
        m_footCollider = transform.GetChild((int)CHILD_OBJECT.FOOT).gameObject.GetComponent<HitChecker_Collider>();
        m_kickCollider = transform.GetChild((int)CHILD_OBJECT.KICK).gameObject.GetComponent<HitChecker_Collider>();

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
