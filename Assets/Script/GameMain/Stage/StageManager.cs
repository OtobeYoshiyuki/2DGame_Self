using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtobeGame
{
    /// <summary>
    /// �X�e�[�W�̊Ǘ��N���X
    /// </summary>
    public class StageManager : MonoBehaviour
    {
        // ���̃X�e�[�W�ֈړ�������t���O
        private bool m_isNext = false;
        public bool isNext
        {
            set { m_isNext = value; }
            get { return m_isNext; }
        }

        // �S�[�������̃t���O
        private bool m_isGoal = false;
        public bool isGoal
        {
            set { m_isGoal = value; }
            get { return m_isGoal; }
        }

    }

}
