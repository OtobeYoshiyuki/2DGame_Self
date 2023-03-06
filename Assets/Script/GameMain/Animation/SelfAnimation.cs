using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtobeLib
{
    /// <summary>
    /// �A�j���[�V�����̏��
    /// </summary>
    [System.Serializable]
    public class AnimationInfo
    {
        //�A�j���[�V�����̃^�O
        [SerializeField]
        private string m_animeTag = string.Empty;
        public string animeTag { get { return m_animeTag; } }

        //�A�j���[�V�����N���b�v
        [SerializeField]
        private AnimationClip m_animationClip = null;
        public AnimationClip clip { get { return m_animationClip; } }
    }

    //����̃A�j���[�V�����N���X
    public class SelfAnimation : MonoBehaviour
    {
        //�A�j���[�V�����N���b�v�̏��
        //���z��ŊǗ��iInspecter����ҏW�ł���悤�ɂ���j
        [SerializeField]
        private List<AnimationInfo> m_animationInfos = new List<AnimationInfo>();

        //�A�j���[�V�����N���b�v�̏��
        //�A�z�z��ŊǗ��iInspecter�̏������Ƃɐݒ肷��j
        private Dictionary<string, AnimationClip> m_animationDictionary = null;

        //�A�j���[�V������Component
        private Animation m_animation = null;

        public void InitAnimations()
        {
            //�A�j���[�V�����N���b�v�̘A�z�z��̏��𐶐�����
            m_animationDictionary = new Dictionary<string, AnimationClip>();

            //�A�j���[�V������Component���擾����
            m_animation = gameObject.GetComponent<Animation>();

            foreach(AnimationInfo info in m_animationInfos)
            {
                //�A�j���[�V�����̃^�O�ƃA�j���[�V�����N���b�v��o�^����
                m_animationDictionary.Add(info.animeTag, info.clip);
            }

            //m_animation.clip = m_animationDictionary["Walk"];
            Debug.Log(m_animation.clip.name);

        }

    }
}

