using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeLib;

namespace OtobeGame
{
    public class ExBlockController : MonoBehaviour
    {
        // �A�j���[�V�����̏I���𔻒肷��t���O
        private bool m_finishAnime = false;
        public bool finishAnime { get { return m_finishAnime; } }

        /// <summary>
        /// �A�j���[�V�����̏I�����ɌĂ΂�鏈��
        /// </summary>
        public void FinishAnimation()
        {
            m_finishAnime = true;
            Destroy(gameObject);
        }
    }
}
