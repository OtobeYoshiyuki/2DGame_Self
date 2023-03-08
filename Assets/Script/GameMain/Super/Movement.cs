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
        [SerializeField]
        protected Vector3 m_rotAngle = Vector3.zero;
        public Vector3 rotAngle { get { return m_rotAngle; } set { m_rotAngle = value; } }

        //オブジェクトを移動させるスピード
        [SerializeField]
        protected Vector3 m_moveSpeed = Vector3.zero;
        public Vector3 moveSpeed { get { return m_moveSpeed; } set { m_moveSpeed = value; } }

        //オブジェクトの拡大・縮小値
        [SerializeField]
        protected Vector3 m_scale = Vector3.one;
        public Vector3 scale { get { return m_scale; } set { m_scale = value; } }

        //2Dの物理演算
        protected Rigidbody2D m_rigidBody2D = null;
        public Rigidbody2D rigidBody2D { get { return m_rigidBody2D; } }
    }
}

