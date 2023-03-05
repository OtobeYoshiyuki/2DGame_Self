using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeLib;

namespace OtobeGame
{
    /// <summary>
    /// �v���C�V�[��
    /// </summary>
    public class PlayScene : ISceneBase
    {
        //�V�[���̖��O
        public const string SCENE_NAME = "PlayScene";

        //�L�����N�^�[�̊Ǘ��N���X
        private CharaManager m_characterManager = null;

        /// <summary>
        /// ����������
        /// </summary>
        public void Init()
        {
            //CharacterManager������������
            m_characterManager = new CharaManager();
            m_characterManager.InitCharaManager();

            //�T�[�r�X���P�[�^�[��charaManager��o�^����
            Locater.Bind(m_characterManager);
        }

        /// <summary>
        /// �X�V����
        /// </summary>
        public void Update()
        {
            //CharacterManager���X�V����
            m_characterManager.UpdateCharacters();
        }

        /// <summary>
        /// �X�V����
        /// ���������Z�𔺂��X�V
        /// </summary>
        public void FixedUpdate()
        {
            //CharacterManager���X�V����
            m_characterManager.FixedUpdateCharacters();
        }

        /// <summary>
        /// �X�V����
        /// �J�����̏����Ȃǂ��L�q
        /// </summary>
        public void LateUpdate()
        {
            //CharacterManager���X�V����
            m_characterManager.LateUpdateCharacters();
        }

        /// <summary>
        /// �I������
        /// </summary>
        public void Final()
        {

        }
    }
}
