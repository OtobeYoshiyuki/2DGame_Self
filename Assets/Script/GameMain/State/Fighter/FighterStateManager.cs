using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeLib;


namespace OtobeGame
{
    /// <summary>
    /// FighterのStateの管理クラス
    /// </summary>
    [System.Serializable]
    public class FighterStateManager : IStateManager 
    {
        //状態遷移を管理するステートマシーン
        private StateMachine<Fighter> m_stateMachine = null;
        public StateMachine<Fighter> stateMachine => m_stateMachine;

        //ステート（呼吸の状態）
        [SerializeField]
        private FighterIdele m_ideleState = new FighterIdele();
        public FighterIdele ideleState { get { return m_ideleState; } }

        //ステート（歩く状態）
        [SerializeField]
        private FighterWalk m_walkState = new FighterWalk();
        public FighterWalk walkState { get { return m_walkState; } }

        //ステート（ジャンプ状態）
        [SerializeField]
        private FighterJump m_jumpState = new FighterJump();
        public FighterJump jumpState { get { return m_jumpState; } }

        //ステート（落下状態）
        [SerializeField]
        private FighterFall m_fallState = new FighterFall();
        public FighterFall fallState { get { return m_fallState; } }

        //ステート（しゃがむ状態）
        [SerializeField]
        private FighterCrounch m_crounchState = new FighterCrounch();
        public FighterCrounch crounchState { get { return m_crounchState; } }

        //ステート（パンチ状態）
        [SerializeField]
        private FighterPunch m_punchState = new FighterPunch();
        public FighterPunch punchState { get { return m_punchState; } }

        //ステート（キック状態）
        [SerializeField]
        private FighterKick m_kickState = new FighterKick();
        public FighterKick kickState { get { return m_kickState; } }

        //ステート（しゃがみ蹴り状態）
        [SerializeField]
        private FighterCrouchKick m_crouchKickState = new FighterCrouchKick();
        public FighterCrouchKick crouchKickState { get { return m_crouchKickState; } }

        //ステート（空中蹴り状態）
        [SerializeField]
        private FighterFlyingKick m_flyingKickState = new FighterFlyingKick();
        public FighterFlyingKick flyingKickState { get { return m_flyingKickState; } }

        public void InitState(ICharacter owner)
        {
            //ステートマシーンを生成する
            m_stateMachine = new StateMachine<Fighter>(owner as Fighter, m_ideleState);
        }

        /// <summary>
        /// 更新時に呼ばれる処理
        /// </summary>
        public void UpdateState()
        {
            //ステートマシーンの更新処理
            m_stateMachine.UpdateState();
        }

        /// <summary>
        /// 物理演算の更新時に呼ばれる処理
        /// </summary>
        public void FixedUpdateState()
        {
            //ステートマシーンの更新処理
            m_stateMachine.FixedUpdateState();
        }

        /// <summary>
        /// 更新後に呼ばれる処理
        /// </summary>
        public void LateUpdateState()
        {
            //ステートマシーンの更新処理
            m_stateMachine.LateUpdateState();
        }
    }

}
