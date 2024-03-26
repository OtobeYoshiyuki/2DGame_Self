using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeLib;

namespace OtobeGame
{
    /// <summary>
    /// 爆発するブロックの管理クラス
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

        // 爆発ブロックのタグ
        public const string EXPROSION_TAG = "Explosion";

        // 通常ブロックのタグ
        public const string BLOCK_TAG = "Block";

        // 爆発時に最初に壊されるブロックのインデックス
        private Vector2Int BREAK_BLOCK_INDEX = new Vector2Int(0, 0);

        // ブロックの縦の長さ
        [SerializeField]
        private int m_colmunSize = 0;

        // ブロックの横の長さ
        [SerializeField]
        private int m_rowSize = 0;

        // 爆発ブロック
        private GameObject m_exBlock = null;
        public GameObject exBlock { get { return m_exBlock; } }

        // 通常ブロック(爆発アニメーション付き)
        private List<GameObject> m_normalBlocks = new List<GameObject>();

        // ブロックの配列
        private List<List<GameObject>> m_blocks = new List<List<GameObject>>();

        public void InitBlocks()
        {
            // 子供オブジェクト参照用インデックス
            int index = 0;

            for (int i = 0; i < m_colmunSize;i++)
            {
                // 1列分のブロック
                List<GameObject> blocks = new List<GameObject>();

                for (int j = 0; j < m_rowSize;j++)
                {
                    // 子供オブジェクトを取得する
                    GameObject child = transform.GetChild(index).gameObject;

                    // 爆発ブロックの場合
                    if (child.gameObject.tag.Equals(EXPROSION_TAG))
                    {
                        m_exBlock = child;
                    }
                    // 通常ブロックの場合
                    else if (child.gameObject.tag.Equals(BLOCK_TAG))
                    {
                        m_normalBlocks.Add(child);
                    }

                    // 各ブロックを設定する
                    ExBlockController controller = child.GetComponent<ExBlockController>();
                    blocks.Add(child);

                    // 参照用インデックスを更新する
                    index++;
                }
                // 1列分のブロックを二次元配列にマージする
                m_blocks.Add(blocks);
            }
        }

        public IEnumerator ExplosionAllBlocks()
        {
            // ブロックの左端と上端から斜め直線のブロックを算出する
            int loopNum = (m_colmunSize - 1) + (m_rowSize - 1);

            // 算出用のインデックス
            Vector2Int index = BREAK_BLOCK_INDEX;

            for (int i = 0; i < loopNum;i++)
            {
                // 天井のときは(1,0)、天井まで到達していないときは(0,1)を返す
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
            // 斜め直線に存在するゲームオブジェクトのリスト
            List<GameObject> obliqueLineObjects = new List<GameObject>();
            obliqueLineObjects.Add(m_blocks[index.y][index.x]);

            // 右端まで到達していないかつ底まで到達していないとき
            while (!IsBlockRightEdge(index) && !IsBlockFloor(index))
            {
                index += new Vector2Int((int)DIRECT.RIGHT, (int)DIRECT.DOWN);
                obliqueLineObjects.Add(m_blocks[index.y][index.x]);
            }

            return obliqueLineObjects;
        }

        private Vector2Int BlockCeilingIndex(Vector2Int index)
        {
            // インデックスが天井のとき
            if (index.y >= m_colmunSize - 1)
            {
                // 右に移動させる
                return new Vector2Int((int)DIRECT.RIGHT, (int)DIRECT.NONE);
            }
            // インデックスが天井まで到達していないとき
            else
            {
                // 上に移動させる
                return new Vector2Int((int)DIRECT.NONE, (int)DIRECT.UP);
            }
        }

        private bool IsBlockRightEdge(Vector2Int index)
        {
            // インデックスが右端のとき
            if (index.x >= m_rowSize - 1)
            {
                return true;
            }
            // インデックスが右端まで到達していないとき
            else
            {
                return false;
            }
        }

        private bool IsBlockFloor(Vector2Int index)
        {
            // インデックスが底のとき
            if (index.y <= 0)
            {
                return true;
            }
            // インデックスが底まで到達していないとき
            else
            {
                return false;
            }
        }
    }

}
