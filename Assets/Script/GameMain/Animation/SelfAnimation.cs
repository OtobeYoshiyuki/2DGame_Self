using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtobeLib
{
    /// <summary>
    /// アニメーションの情報
    /// </summary>
    [System.Serializable]
    public class AnimationInfo
    {
        //アニメーションのタグ
        [SerializeField]
        private string m_animeTag = string.Empty;
        public string animeTag { get { return m_animeTag; } }

        //アニメーションクリップ
        [SerializeField]
        private AnimationClip m_animationClip = null;
        public AnimationClip clip { get { return m_animationClip; } }
    }

    //自作のアニメーションクラス
    public class SelfAnimation : MonoBehaviour
    {
        //アニメーションクリップの情報
        //※配列で管理（Inspecterから編集できるようにする）
        [SerializeField]
        private List<AnimationInfo> m_animationInfos = new List<AnimationInfo>();

        //アニメーションクリップの情報
        //連想配列で管理（Inspecterの情報をもとに設定する）
        private Dictionary<string, AnimationClip> m_animationDictionary = null;

        //アニメーションのComponent
        private Animation m_animation = null;

        public void InitAnimations()
        {
            //アニメーションクリップの連想配列の情報を生成する
            m_animationDictionary = new Dictionary<string, AnimationClip>();

            //アニメーションのComponentを取得する
            m_animation = gameObject.GetComponent<Animation>();

            foreach(AnimationInfo info in m_animationInfos)
            {
                //アニメーションのタグとアニメーションクリップを登録する
                m_animationDictionary.Add(info.animeTag, info.clip);
            }

            //m_animation.clip = m_animationDictionary["Walk"];
            Debug.Log(m_animation.clip.name);

        }

    }
}

