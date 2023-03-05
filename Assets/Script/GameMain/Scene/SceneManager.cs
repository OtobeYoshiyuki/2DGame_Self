using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeLib;
using OtobeGame;
using System;
using UnityEngine.SceneManagement;

namespace OtobeLib
{
    /// <summary>
    /// �V�[���̊Ǘ��N���X
    /// </summary>
    public class SceneManager
    {
        //�V�[���̃��X�g
        private LinkedList<ISceneBase> m_sceneList = null;

        //�V�[���̐������s���t�@�N�g���[
        private Dictionary<string, Func<ISceneBase>> m_sceneFactory = null;

        //�V�[���̏��
        private LinkedList<string> m_sceneInfo = null;

        //�V�[���̏���������
        private LinkedList<string> m_unloadInfo = null;

        //�V�[�����폜���鐔
        private int m_popCount = GameMain.NULL;

        /// <summary>
        /// SceneManager�̏���������
        /// </summary>
        public void Init()
        {
            //�V�[���̃��X�g�𐶐�����
            m_sceneList = new LinkedList<ISceneBase>();

            //�V�[���̐������s���t�@�N�g���[�𐶐�����
            m_sceneFactory = new Dictionary<string, Func<ISceneBase>>();

            //�V�[���̏��𐶐�����
            m_sceneInfo = new LinkedList<string>();

            //�V�[���̏���������𐶐�����
            m_unloadInfo = new LinkedList<string>();

            //�V�[�����폜���鐔������������
            m_popCount = GameMain.NULL;
        }

        /// <summary>
        /// SceneManager�̍X�V����
        /// </summary>
        public void Update()
        {
            //�V�[���̍폜����яI������
            for (int i = GameMain.NULL; i < m_popCount; i++)
            {
                m_sceneList.Last.Value.Final();
                m_sceneList.RemoveLast();
                //�ғ����Ă���V�[������������
                UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(m_unloadInfo.Last.Value);
                //�A�Z�b�g������
                Resources.UnloadUnusedAssets();
                m_unloadInfo.RemoveLast();
            }

            //�폜�J�E���g������������
            m_popCount = GameMain.NULL;

            //�V�[���̐������s��
            foreach (string info in m_sceneInfo)
            {
                //�V�[�������[�h����
                UnityEngine.SceneManagement.SceneManager.LoadScene(info, LoadSceneMode.Additive);   
                //���̃V�[���𐶐�����
                m_sceneList.AddLast(m_sceneFactory[info]());
                //���������V�[���̏��������s��
                m_sceneList.Last.Value.Init();
            }

            //�V�[���̏����폜����
            m_sceneInfo.Clear();

            //�V�[�������݂���Ƃ�
            if (m_sceneList.Count > 0)
            {
                //��Ԍ��̃V�[�����X�V����
                m_sceneList.Last.Value.Update();
            }
        }

        /// <summary>
        /// SceneManager�̍X�V����
        /// ���������Z�𔺂��X�V
        /// </summary>
        public void FixedUpdate()
        {
            //�V�[�������݂���Ƃ�
            if (m_sceneList.Count > 0)
            {
                //��Ԍ��̃V�[�����X�V����
                m_sceneList.Last.Value.FixedUpdate();
            }
        }

        public void LateUpdate()
        {
            //�V�[�������݂���Ƃ�
            if (m_sceneList.Count > 0)
            {
                //��Ԍ��̃V�[�����X�V����
                m_sceneList.Last.Value.LateUpdate();
            }
        }

        /// <summary>
        /// �V�[���̐����p�̊֐���o�^����
        /// </summary>
        /// <param name="name">�V�[���̖��O</param>
        /// <param name="func">new�֐�</param>
        public void AddScene(string name, Func<ISceneBase> func) { m_sceneFactory[name] = func; }

        /// <summary>
        /// �V�[����؂�ւ���
        /// ���ғ����Ă���V�[�������ׂď���
        /// </summary>
        /// <param name="next">���̃V�[���̏��</param>
        public void ChangeScene(string next)
        {
            //�V�[����ǉ���������
            PushScene(next);

            //�폜�����ғ����̃V�[���̐��ɂ���
            m_popCount = m_sceneList.Count;
        }

        /// <summary>
        /// ���ɐ�������Scene�̏���ǉ�����
        /// </summary>
        /// <param name="next">���̃V�[���̏��</param>
        public void PushScene(string next)
        {
            m_sceneInfo.AddLast(next);
            m_unloadInfo.AddLast(next);
        }

        /// <summary>
        /// �V�[���̏������鐔��ݒ肷��
        /// </summary>
        /// <param name="count">�V�[���̏�����</param>
        public void PopScene(int count = 1)
        {
            //�ϐ��̏�����
            int Inspection = GameMain.NULL;

            //�ǉ��������̂������āA�����������炷
            if (m_sceneInfo.Count > 0)
            {
                m_sceneInfo.RemoveLast();
                m_unloadInfo.RemoveLast();
                count--;
            }

            //�V�[���̏�������␳����
            Inspection = Mathf.Max(count, GameMain.NULL);
            Inspection = Mathf.Min(Inspection, m_sceneList.Count - 1);

            //��������ݒ肷��
            m_popCount = Inspection;
        }

        public IEnumerator Start(string sceneName)
        {
            // �񓯊��Ń��[�h���s��
            var asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Additive);

            // ���[�h���������Ă��Ă��C�V�[���̃A�N�e�B�u���͋����Ȃ�
            asyncLoad.allowSceneActivation = false;

            // �t�F�[�h�A�E�g���̉�������̏���
            //yield return new WaitForSeconds(10);

            // ���[�h�����������Ƃ��ɃV�[���̃A�N�e�B�u����������
            asyncLoad.allowSceneActivation = true;

            //���̃V�[���𐶐�����
            m_sceneList.AddLast(m_sceneFactory[sceneName]());
            //���������V�[���̏��������s��
            m_sceneList.Last.Value.Init();

            // ���[�h����������܂ő҂�
            yield return asyncLoad;
        }
    }
}

