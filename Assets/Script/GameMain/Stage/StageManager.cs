using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using OtobeLib;

namespace OtobeGame
{
    /// <summary>
    /// �X�e�[�W�̊Ǘ��N���X
    /// </summary>
    public class StageManager : MonoBehaviour
    {
        // GameObject�̖��O
        public const string OBJECT_NAME = "StageManager";

        // ���I�u�W�F�N�g�̃^�O
        public const string FLOOR_TAG = "Floor";

        // �����u���b�N�̃^�O
        public const string EXBLOCK_TAG = "Explosion";

        // Box�I�u�W�F�N�g�̃^�O
        public const string BOX_TAG = "Box";

        // �ǂݍ��ރX�e�[�W�̏��
        public const string LOAD_STAGE_INFO = "Stage{0}-{1}";

        // �����X�e�[�W�̘A��
        public const int INIT_STAGE_INDEX = 1;

        // ����ǂݍ��݂�
        private bool m_init = false;

        // �ǂݍ��񂾃X�e�[�W�̑O��l
        private string m_prevStageInfo = "";

        // Active�ȃX�e�[�W
        private GameObject m_activeStage = null;
        public GameObject activeStage => m_activeStage;

        // Block�̊Ǘ��N���X
        private BlockManager m_blockManager = new BlockManager();

        /// <summary>
        /// �X�e�[�W����ǂݍ���
        /// </summary>
        /// <param name="stageNum">�ǂݍ��ރX�e�[�W��</param>
        /// <param name="stageIndex">�X�e�[�W�̘A��</param>
        /// <returns>����=true ���s=false</returns>
        public void Load(int stageNum, int stageIndex = 1)
        {
            // ResourceManagaer���擾����
            ResourceManager resourceManager = Locater.Get<ResourceManager>();

            // ����ǂݍ��ݎ��̂Ƃ�
            if (!m_init)
            {
                // �A�Ԃ������I�ɏ����X�e�[�W�ɂ���
                stageIndex = INIT_STAGE_INDEX;

                // ����ǂݍ��݂��ς݂ɂ���
                m_init = true;
            }
            else
            {
                // �ǂݍ��܂�Ă���X�e�[�W���폜����
                GameObject prevStage = GameObject.Find(m_prevStageInfo);
                Destroy(prevStage);
            }

            // �X�e�[�W�������ƂɃX�e�[�W�𐶐�����
            string stageInfo = string.Format(LOAD_STAGE_INFO, stageNum, /*stageIndex*/ 2);
            GameObject gameObject = resourceManager.GetResource(stageInfo);
            m_activeStage = Instantiate(gameObject);
            SceneManager.MoveObjectNowScene(PlayScene.SCENE_NAME, m_activeStage);

            // �u���b�N������������
            m_blockManager.InitBlocks(this);

            // �O��l���X�V
            m_prevStageInfo = stageInfo;
        }

        public void UpdateStage()
        {
            // �L�����N�^�[�̊Ǘ��N���X���擾����
            CharaManager charaManager = Locater.Get<CharaManager>();

            // �u���b�N�̍X�V���s��
            m_blockManager.UpdateBlocks(charaManager.GetHero());
        }

        /// <summary>
        /// ���������X�e�[�W���̔����u���b�N�̊Ǘ��N���X���擾����
        /// </summary>
        /// <returns>�����u���b�N�̊Ǘ��N���X�̃��X�g</returns>
        public List<ExBlockManager> FindExManagers()
        {
            // �����u���b�N�̊Ǘ��N���X�̃��X�g�𐶐�����
            List<ExBlockManager> exBlocks = new List<ExBlockManager>();

            // ���������X�e�[�W�̎q���I�u�W�F�N�g���擾����
            foreach (Transform child in m_activeStage.transform)
            {
                // ��������GameObject�ł͂Ȃ��Ƃ��̓X�L�b�v����
                if (!child.gameObject.name.Contains(ExBlockManager.EXBLOCK_SEARCH)) continue;

                // Component��ǉ�����
                exBlocks.Add(child.gameObject.GetComponent<ExBlockManager>()); 
            }

            return exBlocks;
        }

        /// <summary>
        /// ���������X�e�[�W���̒ʏ�u���b�N�N���X���擾����
        /// </summary>
        /// <returns></returns>
        public List<Block> FindBlocks()
        {
            // �ʏ�u���b�N�̃��X�g�𐶐�����
            List<Block> blocks = new List<Block>();

            // ���������X�e�[�W�̏��I�u�W�F�N�g�̎q�����擾����
            foreach (Transform child in m_activeStage.transform)
            {
                // ��������GameObject�ł͂Ȃ��Ƃ��̓X�L�b�v����
                if (!child.gameObject.name.Contains("NormalBlocks")) continue;

                foreach (Transform box in child.transform)
                {
                    // ��������GameObject�ł͂Ȃ��Ƃ��̓X�L�b�v����
                    if (!box.gameObject.name.Contains(BOX_TAG)) continue;

                    // Component��ǉ�����
                    blocks.Add(box.gameObject.GetComponent<Block>());
                }
            }

            return blocks;
        }
    }

}
