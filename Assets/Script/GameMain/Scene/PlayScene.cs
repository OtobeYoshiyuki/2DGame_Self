using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeLib;
using Cinemachine;

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
        public CharaManager characterManager { get { return m_characterManager; } }

        // �w�i�̃X�N���[���N���X
        private BackGroundMover m_backMover = null;
        public BackGroundMover backMover { get { return m_backMover; } }

        // �����u���b�N�̊Ǘ��N���X
        private ExBlockManager m_exBlockManager = null;

        private CinemachineVirtualCamera m_virtualCamera = null;

        // �V�[���̐؂�ւ����Ǘ�����t���O
        private bool m_playChange = false;
        public bool playChange
        {
            set { m_playChange = value; }
            get { return m_playChange; }
        }

        /// <summary>
        /// ����������
        /// </summary>
        public void Init()
        {
            // �w�i�̃X�N���[���N���X���擾����
            m_backMover = GameObject.Find("PlayUI").transform.GetChild(0).GetComponent<BackGroundMover>();
            m_backMover.InitBackGrond();

            // �T�[�r�X���P�[�^�[��PlayScene��o�^����
            Locater.Bind(this);

            // �T�[�r�X���P�[�^�[��BackMover��o�^����
            Locater.Bind(m_backMover);

            // CharacterManager������������
            m_characterManager = new CharaManager();
            m_characterManager.InitCharaManager();

            // �T�[�r�X���P�[�^�[��charaManager��o�^����
            Locater.Bind(m_characterManager);

            m_exBlockManager = GameObject.Find("Blocks1").GetComponent<ExBlockManager>();
            m_exBlockManager.InitBlocks();

            m_virtualCamera = GameObject.Find("CMCamera").GetComponent<CinemachineVirtualCamera>();
            m_virtualCamera.Follow = m_characterManager.GetHero().transform;

            CompositeCollider2D compositeCollider2D = GameObject.Find("Area").GetComponent<CompositeCollider2D>();
            m_virtualCamera.gameObject.GetComponent<CinemachineConfiner>().m_BoundingShape2D = compositeCollider2D;

            //FadeManager fadeManager = Locater.Get<FadeManager>();
            //fadeManager.FadeOut();

        }

        /// <summary>
        /// �X�V����
        /// </summary>
        public void Update()
        {
            // SceneManager���擾����
            SceneManager sceneManager = Locater.Get<SceneManager>();

            // InputSystemManager���擾����
            InputSystemManager inputSystemManager = Locater.Get<InputSystemManager>();

            // CharacterManager���X�V����
            m_characterManager.UpdateCharacters();

            // BackMover���X�V����
            m_backMover.Move();

            // �I�v�V�����{�^���������ꂽ��
            if (inputSystemManager.playerInput.currentActionMap["Open"].WasPressedThisFrame() && m_playChange)
            {
                // �V�[����؂�ւ���
                sceneManager.PushScene(OptionScene.SCENE_NAME);
            }
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
            m_backMover.Destory();
        }
    }
}
