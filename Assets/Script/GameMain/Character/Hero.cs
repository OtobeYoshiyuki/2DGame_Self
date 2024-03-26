using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtobeLib;
using UnityEngine.InputSystem;

namespace OtobeGame
{
    /// <summary>
    /// 操作キャラ
    /// </summary>
    public abstract class Hero : Character
    {
        /// <summary>
        /// 操作キャラの初期化
        /// </summary>
        public override void InitCharacter()
        {
            //操作キャラの初期化処理
            base.InitCharacter();
        }

        /// <summary>
        /// 操作キャラの更新
        /// </summary>
        public override void UpdateCharacter()
        {
            // InputSystemManagerを取得する
            InputSystemManager inputSystemManager = Locater.Get<InputSystemManager>();

            //ゲームパッドが接続されていないときは、キーボード操作に切り替える
            if (Gamepad.current == null)
            {
                inputSystemManager.playerInput.SwitchCurrentControlScheme("Keybord", Keyboard.current);
            }
            //ゲームパッドが接続されているときは、ゲームパッド操作に切り替える
            else
            {
                inputSystemManager.playerInput.SwitchCurrentControlScheme("Gamepad", Gamepad.current);
            }

            //操作キャラの更新処理
            base.UpdateCharacter();
        }

        /// <summary>
        /// 操作キャラの更新
        /// ※物理演算を伴う更新
        /// </summary>
        public override void FixedUpdateCharacter()
        {
            //操作キャラの更新処理
            base.FixedUpdateCharacter();
        }

        /// <summary>
        /// 操作キャラの更新
        /// Updateの後に呼ばれる処理
        /// </summary>
        public override void LateUpdateCharacter()
        {
            //操作キャラの更新処理
            base.LateUpdateCharacter();
        }

        /// <summary>
        /// 死亡時に呼ばれる処理
        /// </summary>
        public override void FinalCharacter()
        {
            //操作キャラの終了処理
            base.FinalCharacter();
        }

        public void ExBlockBrakeStart(HitChecker_Base hitChecker)
        {
            ExBlockManager exBlockManager = hitChecker.FindHitObject(ExBlockManager.EXPROSION_TAG)
                .transform.parent.gameObject.GetComponent<ExBlockManager>();
            CoroutineHandler.instance.StartCoroutine(exBlockManager.ExplosionAllBlocks());
            ExBlockController blockController = hitChecker.FindHitObject(ExBlockManager.EXPROSION_TAG)
                .GetComponent<ExBlockController>();
            blockController.FinishAnimation();
        }
    }

}
