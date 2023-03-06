using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeLib;
using OtobeGame;

namespace OtobeGame
{
    /// <summary>
    /// Characterクラスで実装するインターフェース
    /// </summary>
    public interface ICharacter
    {
        /// <summary>
        /// キャラクターの初期化
        /// </summary>
        public void InitCharacter();

        /// <summary>
        /// キャラクターの更新
        /// </summary>
        public void UpdateCharacter();

        /// <summary>
        /// キャラクターの更新
        /// ※物理演算を伴う更新
        /// </summary>
        public void FixedUpdateCharacter();

        /// <summary>
        /// キャラクターの更新
        /// Updateの後に呼ばれる処理
        /// </summary>
        public void LateUpdateCharacter();

        /// <summary>
        /// 死亡時に呼ばれる処理
        /// </summary>
        public void FinalCharacter();
    }


    /// <summary>
    /// ICharacterで宣言したCharacterを実装したクラス
    /// </summary>
    public abstract class Character : Movement, ICharacter
    {
        // HPステータスのkey
        public const string HP = "Hp";

        // Atackステータスのkey
        public const string ATACK = "Atack";

        // Defenceステータスのkey
        public const string DEFENCE = "Defence";

        // Stateの時間を計測する時間
        private float m_time = 0.0f;

        // Starの衝突をチェックするフラグ
        private bool m_damageCheck = false;

        // キャラがやられたかをチェックするフラグ
        private bool m_dethCheck = false;

        //処理の順番
        private CharaManager.ORDER_CHARACTER m_orderBy = GameMain.NULL;
        public CharaManager.ORDER_CHARACTER orderBy { get { return m_orderBy; } set { m_orderBy = value; } }

        // ステータスのコントローラー
        private StatusController m_statusCs = new StatusController();
        public StatusController statusCs { get { return m_statusCs; } }

        /// <summary>
        /// キャラクターの初期化
        /// </summary>
        public virtual void InitCharacter()
        {
            //StatusManagerを取得する
            StatusManager statusManager = Locater.Get<StatusManager>();

            //自身のステータスの情報を取得する
            List<StatusInfo> infos = statusManager.GetStatusInfoArray(gameObject.tag);

            //ステータスをコントローラーに渡す
            m_statusCs.AddStatuses(infos);

        }

        /// <summary>
        /// キャラクターの更新
        /// </summary>
        public virtual void UpdateCharacter()
        {
            //キャラクターを移動させる
            Move();

            //キャラクターを回転させる
            Rotation();

            //キャラクターを回転
            Scaling();
        }

        /// <summary>
        /// キャラクターの更新
        /// ※物理演算を伴う更新
        /// </summary>
        public virtual void FixedUpdateCharacter()
        {

        }

        /// <summary>
        /// キャラクターの更新
        /// Updateの後に呼ばれる処理
        /// </summary>
        public virtual void LateUpdateCharacter()
        {

        }

        /// <summary>
        /// 死亡時に呼ばれる処理
        /// </summary>
        public virtual void FinalCharacter()
        {

        }
    }
}
