using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeLib;
using OtobeGame;
using UnityEngine.InputSystem;

namespace OtobeGame
{
    /// <summary>
    /// Characterクラスで実装するインターフェース
    /// </summary>
    public interface ICharacter
    {
        // 表示順のプロパティ
        CharaManager.ORDER_CHARACTER order { get; }

        // ステータス制御のプロパティ
        StatusController statusCs { get; }

        // ステート管理のプロパティ
        IStateManager stateManager { get; }

        // 当たり判定管理のプロパティ
        ICollisionManager collisionManager { get; }

        // アニメーション制御のプロパティ
        Animator animator { get; }

        // 物理エンジンのプロパティ
        Rigidbody2D rigidBody2D { get; }

        // 入力制御のプロパティ
        IControl control { get; }

        /// <summary>
        /// キャラクターの初期化
        /// </summary>
        void InitCharacter(IControl control);

        /// <summary>
        /// キャラクターの更新
        /// </summary>
        void UpdateCharacter();

        /// <summary>
        /// キャラクターの更新
        /// ※物理演算を伴う更新
        /// </summary>
        void FixedUpdateCharacter();

        /// <summary>
        /// キャラクターの更新
        /// Updateの後に呼ばれる処理
        /// </summary>
        void LateUpdateCharacter();

        /// <summary>
        /// 死亡時に呼ばれる処理
        /// </summary>
        void FinalCharacter();

        /// <summary>
        /// 障害物を破壊するときに呼ばれる処理
        /// </summary>        
        /// /// <param name="obstacle">障害物</param>
        void OnDestoryObstacle(IObstacle obstacle);

        /// <summary>
        /// キャラクターが床のゆえに乗っているか
        /// </summary>
        /// <param name="footHit">キャラクターの着地判定</param>
        /// <returns>床の上ならtrue それ以外の場合はfalse</returns>
        bool IsFloor(IHitChecker footHit);
    }


    /// <summary>
    /// ICharacterで宣言したCharacterを実装したクラス
    /// </summary>
    [System.Serializable]
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
        public float time { get { return m_time; } set { m_time = value; } }

        //処理の順番
        protected CharaManager.ORDER_CHARACTER m_order = CharaManager.ORDER_CHARACTER.NONE;
        public CharaManager.ORDER_CHARACTER order => m_order;

        // ステータスのコントローラー
        private StatusController m_statusCs = null;
        public StatusController statusCs => m_statusCs;

        // ステートの管理クラス
        public abstract IStateManager stateManager { get; }

        // 当たり判定の管理クラス
        public abstract ICollisionManager collisionManager { get; }

        //アニメーター
        private Animator m_animator = null;
        public Animator animator => m_animator;

        // 物理エンジン
        private Rigidbody2D m_rigidbody2D = null;
        public Rigidbody2D rigidBody2D => m_rigidbody2D;

        // 入力の制御クラス
        private IControl m_controller = null;
        public IControl control => m_controller;

        /// <summary>
        /// キャラクターの初期化
        /// </summary>
        public virtual void InitCharacter(IControl controller)
        {
            //StatusManagerを取得する
            StatusManager statusManager = Locater.Get<StatusManager>();

            //自身のステータスの情報を取得する
            List<StatusInfo> infos = statusManager.GetStatusInfoArray(gameObject.tag);

            //ステータスのコントローラーを生成する
            m_statusCs = new StatusController();

            //ステータスをコントローラーに渡す
            m_statusCs.AddStatuses(infos);

            //アニメーターを取得する
            m_animator = gameObject.GetComponent<Animator>();

            //RigidBody2Dを取得する
            m_rigidbody2D = gameObject.GetComponent<Rigidbody2D>();

            // 入力の制御クラスを設定する
            m_controller = controller;
        }

        /// <summary>
        /// キャラクターの更新
        /// </summary>
        public virtual void UpdateCharacter()
        {
            // 入力の制御を更新する
            m_controller.OnExecute();
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

        /// <summary>
        /// 障害物を破壊するときに呼ばれる処理
        /// </summary>        
        /// /// <param name="obstacle">障害物</param>
        public void OnDestoryObstacle(IObstacle obstacle) => obstacle.DestoryAnimeStart();

        public bool IsFloor(IHitChecker footHit)
        {
            // Boxか床の上に乗っているとき
            if (footHit.CheckHitObject(StageManager.BOX_TAG) || footHit.CheckHitObject(StageManager.FLOOR_TAG))
            {
                return true;
            }

            return false;
        }
    }
}
