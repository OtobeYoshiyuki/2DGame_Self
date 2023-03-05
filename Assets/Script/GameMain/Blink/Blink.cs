using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 点滅制御クラス
/// </summary>
[System.Serializable]
public class Blink
{
    /// <summary>
    /// 点滅中のフラグ
    /// Inspecterから編集できるようにする
    /// </summary>
    [SerializeField]
    private bool m_blink = false;

    /// <summary>
    /// 表示中のフラグ
    /// </summary>
    private bool m_draw = true;

    /// <summary>
    /// 点滅のループの設定用のフラグ
    /// Inspecterから編集できるようにする
    /// </summary>
    [SerializeField]
    private bool m_loop = false;

    /// <summary>
    /// 点滅時間の計測用の変数
    /// </summary>
    private float m_blinkTime = 0.0f;

    /// <summary>
    /// 点滅中のフレーム数(DeltaTime基準)
    /// </summary>
    private float m_frameCount = 0.0f;

    /// <summary>
    /// 点滅の間隔用の変数
    /// Inspecterから編集できるようにする
    /// </summary>
    [SerializeField]
    private float m_blinkInterval = 0.0f;

    /// <summary>
    /// 点滅の最大数(ループ設定がfalseになっているときのみ有効
    /// Inspecterから編集できるようにする)
    /// </summary>
    [SerializeField]
    private float m_blinkLimit = 0.0f;

    /// <summary>
    /// 表示中に呼ばれる
    /// 関数デリゲート
    /// </summary>
    private event Action m_drawEnable = null;

    /// <summary>
    /// 非表示中に呼ばれる
    /// 関数デリゲート
    /// </summary>
    private event Action m_drawDisable = null;

    /// <summary>
    /// 初期化時に呼ばれる
    /// 関数デリゲート
    /// </summary>
    private event Action m_resetAction = null;

    public void Update()
    {
        //点滅が無効の場合
        if (!m_blink) return;

        //点滅時間が経過したとき
        if (m_blinkTime >= m_blinkInterval)
        {
            //ビットを反転させる
            m_draw = !m_draw;

            //点滅時間を初期化する
            m_blinkTime = 0.0f;
        }

        //ループ設定がfalseの時
        if(!m_loop)
        {
            //点滅時間の終了の時
            if(m_frameCount >= m_blinkLimit)
            {
                //点滅を初期化する
                OnReset();

                //以降の処理は飛ばす
                return;
            }
        }

        //状況に応じて関数を呼ぶ
        Action draw = m_draw ? m_drawEnable : m_drawDisable;
        draw?.Invoke();

        //点滅時間を更新する
        m_blinkTime += Time.deltaTime;

        //フレーム数を更新する
        m_frameCount += Time.deltaTime;
    }

    /// <summary>
    /// 点滅を初期化する
    /// </summary>
    public void OnReset()
    {
        //点滅を無効にする
        m_blink = false;

        //表示をオンにする
        m_draw = true;

        //点滅時間を初期化する
        m_blinkTime = 0.0f;

        //フレーム数を初期化する
        m_frameCount = 0.0f;

        //イベント関数を呼ぶ
        m_resetAction?.Invoke();
    }

    /// <summary>
    /// 点滅中のフラグ
    /// ゲッター
    /// </summary>
    public bool blink 
    { 
        get { return m_blink; } 
        set { m_blink = value; }
    }

    /// <summary>
    /// 表示中のフラグ
    /// ゲッター
    /// </summary>
    public bool draw { get { return m_draw; } }

    /// <summary>
    /// イベント関数
    /// セッター
    /// </summary>
    public Action drawEnable { set { m_drawEnable = value; } }

    /// <summary>
    /// イベント関数
    /// セッター
    /// </summary>
    public Action drawDisable { set { m_drawDisable = value; } }

    /// <summary>
    /// イベント関数
    /// セッター
    /// </summary>
    public Action resetAction { set { m_resetAction = value; } }
}
