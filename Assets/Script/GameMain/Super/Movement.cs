using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtobeLib
{
    /// <summary>
    /// オブジェクトの移動を制御するスーパークラス
    /// </summary>
    public abstract class Movement : MonoBehaviour
    {
        //オブジェクトの回転値
        protected Vector3 m_rotAngle = Vector3.zero;

        //オブジェクトを移動させるスピード
        protected Vector3 m_moveSpeed = Vector3.zero;

        //オブジェクトをスケーリングさせるスピード
        protected Vector3 m_scaleSpeed = Vector3.zero;

        // RigidBody2DのComponent
        protected Rigidbody2D m_rigidBody2D = null;

        // アニメーションのステートマシーン
        protected Animator m_animator = null;

        /// <summary>
        /// 移動処理
        /// </summary>
        public void Move()
        {
            //オブジェクトを移動させる
            transform.position += m_moveSpeed;
        }

        /// <summary>
        ///回転処理
        /// </summary>
        public void Rotation()
        {
            //オブジェクトを指定した値に回転させる
            transform.rotation = Quaternion.Euler(m_rotAngle);
        }

        /// <summary>
        /// 拡大縮小
        /// </summary>
        public void Scaling()
        {

        }

        /// <summary>
        /// 回転の値
        /// </summary>
        public Vector3 rotAngle
        {
            get { return m_rotAngle; }
            set { m_rotAngle = value; }
        }

        /// <summary>
        /// 1フレームに移動させる値
        /// </summary>
        public Vector3 moveSpeed
        {
            get { return m_moveSpeed; }
            set { m_moveSpeed = value; }
        }

        /// <summary>
        /// RigidBody2Dを取得する
        /// </summary>
        public Rigidbody2D rigidBody2D { get { return m_rigidBody2D; } }

        /// <summary>
        /// アニメーターのステートマシーン
        /// </summary>
        public Animator animator { get { return m_animator; } }
    }
}

