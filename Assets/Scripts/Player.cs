 using System.Collections.Generic;
using UnityEngine;

namespace BigPlane {

    /// <summary>
    /// 玩家控制的飞机
    /// </summary>

    public class Player : Plane {

        [Header("Config")]

        public Vector2 moveSize; // TODO:需要和摄像机设定这个值

        protected override void Update() {
            base.Update();
                        
        }

        private void FixedUpdate() {
            if (m_isDead) return;

            Move();
        }


        private void Move() {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            Vector3 moveDir = new Vector3(x, y, 0);
            m_rigidbody2D.velocity = moveDir * m_moveSpeed;

            m_rigidbody2D.position = new Vector3(
                Mathf.Clamp( m_rigidbody2D.position.x, -moveSize.x, moveSize.x),
                Mathf.Clamp( m_rigidbody2D.position.y, -moveSize.y, moveSize.y),
                0
                );
        }

        /// <summary>
        /// 碰到子弹
        /// </summary>
        /// <param name="bullet"></param>
        protected override void OnTriggerBullet(Bullet bullet) {
            if (!CompareRelation(bullet.relationType)) {
                Die();
            }
        }

        /// <summary>
        /// 碰到飞机
        /// </summary>
        /// <param name="enermy"></param>
        protected override void OnTriggerPlane(Plane enermy) {
            Debug.Break();
            if (!enermy.CompareRelation(RelationType.Player)) {
                Die();
            }
        }

    }
}
