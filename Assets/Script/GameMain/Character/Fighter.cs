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
public class Fighter : Character
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
        SPRITE          //画像
    }

    // ステートの管理クラス
    [SerializeField]
    private FighterStateManager m_stateManager = new FighterStateManager();
    public override IStateManager stateManager => m_stateManager;

    // 当たり判定の管理クラス
    private FighterCollisionManager m_collisionManager = new FighterCollisionManager();
    public override ICollisionManager collisionManager => m_collisionManager;

    // ダッシュエフェクトの制御クラス
    DashAnimeController m_dashAnimeCs = null;
    public DashAnimeController dashAnimeCs => m_dashAnimeCs;

    /// <summary>
    /// キャラクターの初期化
    /// </summary>
    public override void InitCharacter(IControl controller) 
    {
        // キャラクターの初期化処理
        base.InitCharacter(controller);

        // 処理の順番を設定する
        m_order = CharaManager.ORDER_CHARACTER.FIGHTER;

        // ステートの管理クラスを初期化する
        m_stateManager.InitState(this);

        // 当たり判定の管理クラスを初期化する
        m_collisionManager.InitCollision(this);

        // ダッシュアニメーションの制御クラスを取得する
        m_dashAnimeCs = m_collisionManager.footCollider.transform.GetChild(0).gameObject.GetComponent<DashAnimeController>();
        m_dashAnimeCs.StopAnimation();
    }

    /// <summary>
    /// キャラクターの更新
    /// </summary>
    public override void UpdateCharacter()
    {
        // ステートの管理クラスの更新処理
        m_stateManager.UpdateState();

        //キャラクターの更新処理
        base.UpdateCharacter();
    }

    /// <summary>
    /// キャラクターの更新
    /// ※物理演算を伴う更新
    /// </summary>
    public override void FixedUpdateCharacter()
    {
        // ステートの管理クラスの更新処理
        m_stateManager.FixedUpdateState();

        //キャラクターの更新処理
        base.FixedUpdateCharacter();
    }

    /// <summary>
    /// キャラクターの更新
    /// Updateの後に呼ばれる処理
    /// </summary>
    public override void LateUpdateCharacter()
    {
        // ステートの管理クラスの更新処理
        m_stateManager.LateUpdateState();

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
