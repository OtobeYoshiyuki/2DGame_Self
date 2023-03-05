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

        /// <summary>
        /// ����������
        /// </summary>
        public void Init()
        {
            //CharacterManager���X�V����
            CharaManager charaManager = Locater.Get<CharaManager>();
            charaManager.InitCharaManager();
        }

        /// <summary>
        /// �X�V����
        /// </summary>
        public void Update()
        {
            //CharacterManager���X�V����
            CharaManager charaManager = Locater.Get<CharaManager>();
            charaManager.UpdateCharacters();
        }

        /// <summary>
        /// �X�V����
        /// ���������Z�𔺂��X�V
        /// </summary>
        public void FixedUpdate()
        {
            //CharacterManager���X�V����
            CharaManager charaManager = Locater.Get<CharaManager>();
            charaManager.FixedUpdateCharacters();
        }

        /// <summary>
        /// �X�V����
        /// �J�����̏����Ȃǂ��L�q
        /// </summary>
        public void LateUpdate()
        {
            //CharacterManager���X�V����
            CharaManager charaManager = Locater.Get<CharaManager>();
            charaManager.LateUpdateCharacters();
        }

        /// <summary>
        /// �I������
        /// </summary>
        public void Final()
        {

        }
    }
}
