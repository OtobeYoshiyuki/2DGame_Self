using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtobeLib
{
    /// <summary>
    /// �V�[���̃x�[�X�ƂȂ�C���^�[�t�F�[�X
    /// </summary>
    public interface ISceneBase
    {
        /// <summary>
        /// ����������
        /// </summary>
        public void Init();

        /// <summary>
        /// �X�V����
        /// </summary>
        public void Update();

        /// <summary>
        /// �X�V����
        /// ���������Z�𔺂��X�V
        /// </summary>
        public void FixedUpdate();

        /// <summary>
        /// �X�V����
        /// �J�����̏����Ȃǂ��L�q
        /// </summary>
        public void LateUpdate();

        /// <summary>
        /// �I������
        /// </summary>
        public void Final();
    }

}
