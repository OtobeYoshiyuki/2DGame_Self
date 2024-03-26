using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// フェードイン、フェードアウトの管理クラス
/// </summary>
public class FadeManager : MonoBehaviour
{
    public enum FADESTATE : int
    {
        NONE = 0,
        FADEIN,
        GAME,
        FADEOUT,
        RESULT,
        MAX
    }

    // フェードの制御クラス
    private Fade m_fadeCanvas = null;

    // フェードの描画クラス
    private FadeImage m_fadeImage = null;

    // フェード時間(秒)
    [SerializeField]
    private float m_fadeTime = 0.0f;
    public float fadeTime
    {
        set { m_fadeTime = value; }
        get { return m_fadeTime; }
    }

    // ルール画像の配列
    [SerializeField]
    private List<Texture> m_ruleTextures = new List<Texture>();

    private int m_ruleIndex = 0;

    private FADESTATE m_fadeState = FADESTATE.NONE;

    /// <summary>
    /// フェードの初期化処理
    /// </summary>
    public void InitFadeCanvas(FADESTATE fadeState)
    {
        // フェードのComponentを取得する
        m_fadeCanvas = GameObject.Find("FadeCanvas").GetComponent<Fade>();
        m_fadeImage = GameObject.Find("FadeCanvas").GetComponent<FadeImage>();

        // フェードのステートを設定する
        m_fadeState = fadeState;

        m_ruleIndex = 0;

        m_fadeImage.mask = m_ruleTextures[m_ruleIndex];

        m_fadeImage.UpdateMaskTexture();
    }

    /// <summary>
    /// フェードインを行う
    /// </summary>
    public void FadeIn(Action action = null)
    {
        // フェードイン開始時
        m_fadeState = FADESTATE.FADEIN;
        Debug.Log("FadeIn");

        // フェードイン実行開始(コルーチン)
        m_fadeCanvas.FadeIn(m_fadeTime, () =>
         {
             // フェードイン終了時
             m_fadeState = FADESTATE.RESULT;
             Debug.Log("Result");
             action?.Invoke();
         });
    }

    /// <summary>
    /// フェードアウトを行う
    /// </summary>
    public void FadeOut(Action action = null)
    {
        // フェードアウト開始
        m_fadeState = FADESTATE.FADEOUT;
        Debug.Log("FadeOut");

        // フェードアウト実行時
        m_fadeCanvas.FadeOut(m_fadeTime, () =>
        {
            // フェードアウト終了時
            m_fadeState = FADESTATE.GAME;
            Debug.Log("Game");
            action?.Invoke();
        });
    }

    public void FadeInFadeOut()
    {
        FadeIn(() => FadeOut(() =>
        {
            m_ruleIndex++;

            m_fadeImage.mask = m_ruleTextures[m_ruleIndex];

            m_fadeImage.UpdateMaskTexture();

        }));
    }

}
