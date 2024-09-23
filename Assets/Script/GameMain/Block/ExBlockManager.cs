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
        // 方向
        enum DIRECT : int
        {
            NONE = 0,       // 特になし
            UP = 1,         // 上
            DOWN = -1,      // 下
            LEFT = -1,      // 左
            RIGHT = 1       // 右
        }

        // GameObjectの検索キーワード
        public const string EXBLOCK_SEARCH = "ExBlocks";

        // 爆発時に最初に壊されるブロックのインデックス
        private Vector2Int BREAK_BLOCK_INDEX = new Vector2Int(0, 0);

        // ブロックの縦の長さ
        [SerializeField]
        private int m_colmunSize = 0;

        // ブロックの横の長さ
        [SerializeField]
        private int m_rowSize = 0;

        // ブロックの配列
        private List<List<Block>> m_blocks = new List<List<Block>>();

        public void InitBlocks()
        {
            // 子供オブジェクト参照用インデックス
            int index = 0;

            for (int i = 0; i < m_colmunSize;i++)
            {
                // 1列分のブロック
                List<Block> blocks = new List<Block>();

                for (int j = 0; j < m_rowSize;j++)
                {
                    // 子供のGameObjectを取得する
                    GameObject gameObject = transform.GetChild(index).gameObject;

                    // 障害物を取得し、初期化する
                    IObstacle block = gameObject.GetComponent<IObstacle>();
                    block.InitObstacle();

                    // 各ブロックを設定する
                    blocks.Add(block as Block);

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

                // 斜め直線上のオブジェクトを取得する
                List<Block> breakBlocks = ObliqueLine(index);
                foreach(IObstacle block in breakBlocks)
                {
                    block.DestoryAnimeStart();
                }
                yield return new WaitForSeconds(0.2f);
            }
        }

        public void CharacterBreakeBlock(ICharacter character, List<IHitChecker> hitCheckers)
        {
            foreach (IHitChecker hit in hitCheckers)
            {
                // 入力制御がプレイヤーの場合
                if (character.control is UserController)
                {
                    // 操作可能なキャラクターが爆発ブロックを壊したとき
                    if (hit.CheckHitObject(ExplosionBlock.EXPROSION_TAG))
                    {
                        // 破壊対象の障害物を取得する
                        Obstacle obstacle = hit.FindHitObject(ExplosionBlock.EXPROSION_TAG).GetComponent<ExplosionBlock>();

                        // ブロックを連鎖的に破壊する
                        character.OnDestoryObstacle(obstacle);
                    }
                }
            }
        }

        private List<Block> ObliqueLine(Vector2Int index)
        {
            // 斜め直線に存在するゲームオブジェクトのリスト
            List<Block> obliqueLineObjects = new List<Block>();

            // 右端まで到達していないかつ底まで到達していないとき
            while (!IsBlockRightEdge(index) && !IsBlockFloor(index))
            {
                obliqueLineObjects.Add(m_blocks[index.y][index.x]);
                index += new Vector2Int((int)DIRECT.RIGHT, (int)DIRECT.DOWN);
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
                // 天井に移動させる
                return new Vector2Int((int)DIRECT.NONE, (int)DIRECT.UP);
            }
        }

        private bool IsBlockRightEdge(Vector2Int index)
        {
            // インデックスが右端のとき
            if (index.x > m_rowSize - 1)
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
            if (index.y < 0)
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
