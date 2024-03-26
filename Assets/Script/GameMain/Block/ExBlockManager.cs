using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeLib;

namespace OtobeGame
{
    /// <summary>
    /// ��������u���b�N�̊Ǘ��N���X
    /// </summary>
    public class ExBlockManager : MonoBehaviour
    {
        enum DIRECT : int
        {
            NONE = 0,
            UP = 1,
            DOWN = -1,
            LEFT = -1,
            RIGHT = 1
        }

        // �����u���b�N�̃^�O
        public const string EXPROSION_TAG = "Explosion";

        // �ʏ�u���b�N�̃^�O
        public const string BLOCK_TAG = "Block";

        // �������ɍŏ��ɉ󂳂��u���b�N�̃C���f�b�N�X
        private Vector2Int BREAK_BLOCK_INDEX = new Vector2Int(0, 0);

        // �u���b�N�̏c�̒���
        [SerializeField]
        private int m_colmunSize = 0;

        // �u���b�N�̉��̒���
        [SerializeField]
        private int m_rowSize = 0;

        // �����u���b�N
        private GameObject m_exBlock = null;
        public GameObject exBlock { get { return m_exBlock; } }

        // �ʏ�u���b�N(�����A�j���[�V�����t��)
        private List<GameObject> m_normalBlocks = new List<GameObject>();

        // �u���b�N�̔z��
        private List<List<GameObject>> m_blocks = new List<List<GameObject>>();

        public void InitBlocks()
        {
            // �q���I�u�W�F�N�g�Q�Ɨp�C���f�b�N�X
            int index = 0;

            for (int i = 0; i < m_colmunSize;i++)
            {
                // 1�񕪂̃u���b�N
                List<GameObject> blocks = new List<GameObject>();

                for (int j = 0; j < m_rowSize;j++)
                {
                    // �q���I�u�W�F�N�g���擾����
                    GameObject child = transform.GetChild(index).gameObject;

                    // �����u���b�N�̏ꍇ
                    if (child.gameObject.tag.Equals(EXPROSION_TAG))
                    {
                        m_exBlock = child;
                    }
                    // �ʏ�u���b�N�̏ꍇ
                    else if (child.gameObject.tag.Equals(BLOCK_TAG))
                    {
                        m_normalBlocks.Add(child);
                    }

                    // �e�u���b�N��ݒ肷��
                    ExBlockController controller = child.GetComponent<ExBlockController>();
                    blocks.Add(child);

                    // �Q�Ɨp�C���f�b�N�X���X�V����
                    index++;
                }
                // 1�񕪂̃u���b�N��񎟌��z��Ƀ}�[�W����
                m_blocks.Add(blocks);
            }
        }

        public IEnumerator ExplosionAllBlocks()
        {
            // �u���b�N�̍��[�Ə�[����΂ߒ����̃u���b�N���Z�o����
            int loopNum = (m_colmunSize - 1) + (m_rowSize - 1);

            // �Z�o�p�̃C���f�b�N�X
            Vector2Int index = BREAK_BLOCK_INDEX;

            for (int i = 0; i < loopNum;i++)
            {
                // �V��̂Ƃ���(1,0)�A�V��܂œ��B���Ă��Ȃ��Ƃ���(0,1)��Ԃ�
                index += BlockCeilingIndex(index);
                List<GameObject> breakBlocks = ObliqueLine(index);
                foreach(GameObject block in breakBlocks)
                {
                    Animator blockAnimator = block.GetComponent<Animator>();
                    blockAnimator.SetInteger("BlockState", 1);
                }
                yield return new WaitForSeconds(0.2f);
            }
        }

        private List<GameObject> ObliqueLine(Vector2Int index)
        {
            // �΂ߒ����ɑ��݂���Q�[���I�u�W�F�N�g�̃��X�g
            List<GameObject> obliqueLineObjects = new List<GameObject>();
            obliqueLineObjects.Add(m_blocks[index.y][index.x]);

            // �E�[�܂œ��B���Ă��Ȃ�����܂œ��B���Ă��Ȃ��Ƃ�
            while (!IsBlockRightEdge(index) && !IsBlockFloor(index))
            {
                index += new Vector2Int((int)DIRECT.RIGHT, (int)DIRECT.DOWN);
                obliqueLineObjects.Add(m_blocks[index.y][index.x]);
            }

            return obliqueLineObjects;
        }

        private Vector2Int BlockCeilingIndex(Vector2Int index)
        {
            // �C���f�b�N�X���V��̂Ƃ�
            if (index.y >= m_colmunSize - 1)
            {
                // �E�Ɉړ�������
                return new Vector2Int((int)DIRECT.RIGHT, (int)DIRECT.NONE);
            }
            // �C���f�b�N�X���V��܂œ��B���Ă��Ȃ��Ƃ�
            else
            {
                // ��Ɉړ�������
                return new Vector2Int((int)DIRECT.NONE, (int)DIRECT.UP);
            }
        }

        private bool IsBlockRightEdge(Vector2Int index)
        {
            // �C���f�b�N�X���E�[�̂Ƃ�
            if (index.x >= m_rowSize - 1)
            {
                return true;
            }
            // �C���f�b�N�X���E�[�܂œ��B���Ă��Ȃ��Ƃ�
            else
            {
                return false;
            }
        }

        private bool IsBlockFloor(Vector2Int index)
        {
            // �C���f�b�N�X����̂Ƃ�
            if (index.y <= 0)
            {
                return true;
            }
            // �C���f�b�N�X����܂œ��B���Ă��Ȃ��Ƃ�
            else
            {
                return false;
            }
        }
    }

}
