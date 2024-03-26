using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// �t�F�[�h�C���A�t�F�[�h�A�E�g�̊Ǘ��N���X
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

    // �t�F�[�h�̐���N���X
    private Fade m_fadeCanvas = null;

    // �t�F�[�h�̕`��N���X
    private FadeImage m_fadeImage = null;

    // �t�F�[�h����(�b)
    [SerializeField]
    private float m_fadeTime = 0.0f;
    public float fadeTime
    {
        set { m_fadeTime = value; }
        get { return m_fadeTime; }
    }

    // ���[���摜�̔z��
    [SerializeField]
    private List<Texture> m_ruleTextures = new List<Texture>();

    private int m_ruleIndex = 0;

    private FADESTATE m_fadeState = FADESTATE.NONE;

    /// <summary>
    /// �t�F�[�h�̏���������
    /// </summary>
    public void InitFadeCanvas(FADESTATE fadeState)
    {
        // �t�F�[�h��Component���擾����
        m_fadeCanvas = GameObject.Find("FadeCanvas").GetComponent<Fade>();
        m_fadeImage = GameObject.Find("FadeCanvas").GetComponent<FadeImage>();

        // �t�F�[�h�̃X�e�[�g��ݒ肷��
        m_fadeState = fadeState;

        m_ruleIndex = 0;

        m_fadeImage.mask = m_ruleTextures[m_ruleIndex];

        m_fadeImage.UpdateMaskTexture();
    }

    /// <summary>
    /// �t�F�[�h�C�����s��
    /// </summary>
    public void FadeIn(Action action = null)
    {
        // �t�F�[�h�C���J�n��
        m_fadeState = FADESTATE.FADEIN;
        Debug.Log("FadeIn");

        // �t�F�[�h�C�����s�J�n(�R���[�`��)
        m_fadeCanvas.FadeIn(m_fadeTime, () =>
         {
             // �t�F�[�h�C���I����
             m_fadeState = FADESTATE.RESULT;
             Debug.Log("Result");
             action?.Invoke();
         });
    }

    /// <summary>
    /// �t�F�[�h�A�E�g���s��
    /// </summary>
    public void FadeOut(Action action = null)
    {
        // �t�F�[�h�A�E�g�J�n
        m_fadeState = FADESTATE.FADEOUT;
        Debug.Log("FadeOut");

        // �t�F�[�h�A�E�g���s��
        m_fadeCanvas.FadeOut(m_fadeTime, () =>
        {
            // �t�F�[�h�A�E�g�I����
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
