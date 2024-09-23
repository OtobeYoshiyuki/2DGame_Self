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
        // �V�[���̖��O
        public const string SCENE_NAME = "PlayScene";

        // �L�����N�^�[�̊Ǘ��N���X
        private CharaManager m_characterManager = null;
        public CharaManager characterManager => m_characterManager;

        // �X�e�[�W�̊Ǘ��N���X
        private StageManager m_stageManager = null;
        public StageManager stageManager => m_stageManager;

        // �w�i�̃X�N���[���N���X
        private BackGroundMover m_backMover = null;
        public BackGroundMover backMover => m_backMover;

        // ChineMachine�̃J�����N���X
        private CinemachineVirtualCamera m_virtualCamera = null;
        public CinemachineVirtualCamera virtualCamera => m_virtualCamera;

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
            // �X�e�[�W�̊Ǘ��N���X���擾����
            m_stageManager = GameObject.Find(StageManager.OBJECT_NAME).GetComponent<StageManager>();
            m_stageManager.Load(1);

            // �w�i�̃X�N���[���N���X���擾����
            m_backMover = GameObject.Find("PlayUI").transform.GetChild(0).GetComponent<BackGroundMover>();
            m_backMover.InitBackGrond();

            // �T�[�r�X���P�[�^�[��StageManager��o�^����
            Locater.Bind(m_stageManager);

            // �T�[�r�X���P�[�^�[��PlayScene��o�^����
            Locater.Bind(this);

            // �T�[�r�X���P�[�^�[��BackMover��o�^����
            Locater.Bind(m_backMover);

            // CharacterManager������������
            m_characterManager = new CharaManager();
            m_characterManager.InitCharaManager();

            // �T�[�r�X���P�[�^�[��charaManager��o�^����
            Locater.Bind(m_characterManager);

            m_virtualCamera = GameObject.Find("CMCamera").GetComponent<CinemachineVirtualCamera>();
            m_virtualCamera.Follow = (m_characterManager.GetHero() as Character).transform;

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
            //FadeManager fadeManager = Locater.Get<FadeManager>();
            //if (fadeManager.fadeState != FadeManager.FADESTATE.GAME) return;

            // SceneManager���擾����
            SceneManager sceneManager = Locater.Get<SceneManager>();

            // InputSystemManager���擾����
            InputSystemManager inputSystemManager = Locater.Get<InputSystemManager>();

            // StageManager���X�V����
            m_stageManager.UpdateStage();

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
