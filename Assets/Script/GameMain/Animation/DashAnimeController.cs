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
    /// �A�j���[�V�����̃��[�v�J�n���ɌĂ΂�鏈��
    /// </summary>
    /// <param name="layer">���C���[�ԍ�</param>
    /// <param name="index">�C���f�b�N�X�ԍ�</param>
    /// <param name="loop">���[�v�Đ�</param>
    public void SetAnimeLoop(int layer, int index, bool loop)
    {
        // �A�j���[�V�����̃��[�v���J�n����
        Animator animator = gameObject.GetComponent<Animator>();
        AnimationClip clip = animator.GetCurrentAnimatorClipInfo(layer)[index].clip;
        AnimationClipSettings settings = AnimationUtility.GetAnimationClipSettings(clip);
        settings.loopTime = loop;
    }

    public void StopAnimation()
    {
        if (m_dashInfo == DASHANIME.STOP) return;

        // �A�j���[�V�������~������
        Animator animator = gameObject.GetComponent<Animator>();
        animator.SetFloat("animeScale", 0.0f);

        // �A�j���[�V�������\���ɂ���
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        m_dashInfo = DASHANIME.STOP;
    }

    public void StartAnimation()
    {
        if (m_dashInfo == DASHANIME.START) return;

        // �A�j���[�V�������J�n������
        Animator animator = gameObject.GetComponent<Animator>();
        animator.SetFloat("animeScale", 1.0f);
        animator.Play("Dash", 0, 0.0f);

        // �A�j���[�V������\��������
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.color = Color.white;

        m_dashInfo = DASHANIME.START;
    }
}
