using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using OtobeLib;

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
        private ICharacter m_heroCharacter = null;

        //ステージ上にいる敵キャラクター
        private List<ICharacter> m_enemyCharacters = null;

        //ステージ上にいる全てのキャラクター
        private List<ICharacter> m_allCharacters = null;

        /// <summary>
        /// CharacterManagerの初期化処理
        /// </summary>
        public void InitCharaManager()
        {
            //操作キャラを生成する
            ResourceManager resourceManager = Locater.Get<ResourceManager>();
            GameObject fighter = Instantiate(resourceManager.GetResource("Fighter"));
            m_heroCharacter = fighter.GetComponent<Fighter>();
            m_heroCharacter.InitCharacter(new UserController());
            SceneManager.MoveObjectNowScene(PlayScene.SCENE_NAME, fighter);

            //敵キャラのコンテナを生成する
            m_enemyCharacters = new List<ICharacter>();

            //全キャラクターのコンテナを生成する
            m_allCharacters = new List<ICharacter>();

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
            m_allCharacters.Sort((x, y) => x.order - y.order);
            m_enemyCharacters.Sort((x, y) => x.order - y.order);

            foreach (ICharacter ch in m_allCharacters)
            {
                //全キャラクターの更新をする
                ch?.UpdateCharacter();
            }
        }

        /// <summary>
        /// 全キャラクターの更新処理をする
        /// ※物理演算を伴う更新
        /// </summary>
        public void FixedUpdateCharacters()
        {
            foreach (ICharacter ch in m_allCharacters)
            {
                //全キャラクターの更新をする
                ch?.FixedUpdateCharacter();
            }
        }

        /// <summary>
        /// 全キャラクターの更新処理をする
        /// ※Update後に呼ばれる処理
        /// </summary>
        public void LateUpdateCharacters()
        {
            foreach (ICharacter ch in m_allCharacters)
            {
                //全キャラクターの更新をする
                ch?.LateUpdateCharacter();
            }
        }

        /// <summary>
        /// 操作キャラを取得する
        /// </summary>
        /// <returns>操作キャラ</returns>
        public ICharacter GetHero() { return m_heroCharacter; }
    }

}
