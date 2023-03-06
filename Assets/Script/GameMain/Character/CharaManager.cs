using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtobeGame
{
    /// <summary>
    /// キャラクターの管理クラス
    /// </summary>
    public class CharaManager : MonoBehaviour
    {
        //処理の順番の列挙体
        public enum ORDER_CHARACTER : int
        {
            NONE = 0,           //未設定
            FIGHTER = 1,        //格闘家
        }

        //操作するキャラクター
        private Hero m_heroCharacter = null;

        //ステージ上にいる敵キャラクター
        private List<Enemy> m_enemyCharacters = null;

        //ステージ上にいる全てのキャラクター
        private List<Character> m_allCharacters = null;

        /// <summary>
        /// CharacterManagerの初期化処理
        /// </summary>
        public void InitCharaManager()
        {
            //操作キャラを生成する
            GameObject gameObject = (GameObject)Resources.Load("Fighter");
            m_heroCharacter = Instantiate(gameObject).GetComponent<Fighter>();
            m_heroCharacter.InitCharacter();

            //敵キャラのコンテナを生成する
            m_enemyCharacters = new List<Enemy>();

            //全キャラクターのコンテナを生成する
            m_allCharacters = new List<Character>();

            //敵キャラをコンテナに追加する

            //操作キャラをコンテナに追加する
            m_allCharacters.Add(m_heroCharacter);
        }

        /// <summary>
        /// 全キャラクターの更新処理をする
        /// </summary>
        public void UpdateCharacters()
        {
            //処理順にソートする（昇順）
            m_allCharacters.Sort((x, y) => x.orderBy - y.orderBy);
            m_enemyCharacters.Sort((x, y) => x.orderBy - y.orderBy);

            foreach (Character ch in m_allCharacters)
            {
                //全キャラクターの更新をする
                ch.UpdateCharacter();
            }
        }

        /// <summary>
        /// 全キャラクターの更新処理をする
        /// ※物理演算を伴う更新
        /// </summary>
        public void FixedUpdateCharacters()
        {
            foreach (Character ch in m_allCharacters)
            {
                //全キャラクターの更新をする
                ch.FixedUpdateCharacter();
            }
        }

        /// <summary>
        /// 全キャラクターの更新処理をする
        /// ※Update後に呼ばれる処理
        /// </summary>
        public void LateUpdateCharacters()
        {
            foreach (Character ch in m_allCharacters)
            {
                //全キャラクターの更新をする
                ch.LateUpdateCharacter();
            }
        }

        /// <summary>
        /// Heroを継承した操作キャラのスクリプトを探す
        /// </summary>
        /// <typeparam name="T">Heroを継承したクラス</typeparam>
        /// <param name="tag">インスペクタ上のオブジェクトの名前</param>
        /// <returns>Heroを継承したスクリプト</returns>
        private T FindHeroCharacter<T>(string tag) where T : Hero
        {
            //Heroを継承したスクリプトを返す
            return GameObject.Find(tag).GetComponent<T>();
        }

        public Hero GetHero() { return m_heroCharacter; }
    }

}
