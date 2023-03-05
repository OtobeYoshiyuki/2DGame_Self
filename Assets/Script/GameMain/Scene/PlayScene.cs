using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeLib;

namespace OtobeGame
{
    /// <summary>
    /// プレイシーン
    /// </summary>
    public class PlayScene : ISceneBase
    {
        //シーンの名前
        public const string SCENE_NAME = "PlayScene";

        /// <summary>
        /// 初期化処理
        /// </summary>
        public void Init()
        {
            //CharacterManagerを更新する
            CharaManager charaManager = Locater.Get<CharaManager>();
            charaManager.InitCharaManager();
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        public void Update()
        {
            //CharacterManagerを更新する
            CharaManager charaManager = Locater.Get<CharaManager>();
            charaManager.UpdateCharacters();
        }

        /// <summary>
        /// 更新処理
        /// ※物理演算を伴う更新
        /// </summary>
        public void FixedUpdate()
        {
            //CharacterManagerを更新する
            CharaManager charaManager = Locater.Get<CharaManager>();
            charaManager.FixedUpdateCharacters();
        }

        /// <summary>
        /// 更新処理
        /// カメラの処理などを記述
        /// </summary>
        public void LateUpdate()
        {
            //CharacterManagerを更新する
            CharaManager charaManager = Locater.Get<CharaManager>();
            charaManager.LateUpdateCharacters();
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Final()
        {

        }
    }
}
