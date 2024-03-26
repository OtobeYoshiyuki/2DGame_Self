using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DashAnimeController : MonoBehaviour
{
    public enum DASHANIME: int
    {
        NONE,
        START,
        STOP,
        MAX
    }

    private DASHANIME m_dashInfo = DASHANIME.NONE;

    /// <summary>
    /// アニメーションのループ開始時に呼ばれる処理
    /// </summary>
    /// <param name="layer">レイヤー番号</param>
    /// <param name="index">インデックス番号</param>
    /// <param name="loop">ループ再生</param>
    public void SetAnimeLoop(int layer, int index, bool loop)
    {
        // アニメーションのループを開始する
        Animator animator = gameObject.GetComponent<Animator>();
        AnimationClip clip = animator.GetCurrentAnimatorClipInfo(layer)[index].clip;
        AnimationClipSettings settings = AnimationUtility.GetAnimationClipSettings(clip);
        settings.loopTime = loop;
    }

    public void StopAnimation()
    {
        if (m_dashInfo == DASHANIME.STOP) return;

        // アニメーションを停止させる
        Animator animator = gameObject.GetComponent<Animator>();
        animator.SetFloat("animeScale", 0.0f);

        // アニメーションを非表示にする
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        m_dashInfo = DASHANIME.STOP;
    }

    public void StartAnimation()
    {
        if (m_dashInfo == DASHANIME.START) return;

        // アニメーションを開始させる
        Animator animator = gameObject.GetComponent<Animator>();
        animator.SetFloat("animeScale", 1.0f);
        animator.Play("Dash", 0, 0.0f);

        // アニメーションを表示させる
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.color = Color.white;

        m_dashInfo = DASHANIME.START;
    }
}
