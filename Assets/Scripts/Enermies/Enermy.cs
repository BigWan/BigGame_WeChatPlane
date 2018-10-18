using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigPlane {

    /// <summary>
    /// 敌机
    /// </summary>

    public class Enermy :Plane {

        [Header("Enermy")]

        [SerializeField] private int m_hp;

        public int killScore;

        public bool canChangeMoveDir;

        private Vector3 m_moveDir = Vector3.down;

        private Player m_player;

        
        protected override void Awake() {
            base.Awake();
            m_moveDir = Vector3.down;
            m_player = FindObjectOfType<Player>();
        }

        /// <summary>
        /// 碰到子弹
        /// </summary>
        /// <param name="bullet"></param>
        protected override void OnTriggerBullet(Bullet bullet) {
            if (!CompareRelation(bullet.relationType)) {
                m_hp -= 1;
                if (m_hp <= 0) {
                    Die();
                } else {
                    Hit();
                }
            }

        }


        protected override void Update() {
            base.Update();
            if (m_isDead) return;
            lastTurnTime += Time.deltaTime;
            Move();
        }

        private void Move() {
            if (canChangeMoveDir && lastTurnTime>=aimInterval)
                ChangeMoveDir();
            m_rigidbody2D.velocity = m_moveDir * m_moveSpeed;

        }


        [SerializeField]float aimInterval = 4f;
        float lastTurnTime = 2f;
        /// <summary>
        /// 改变朝向
        /// </summary>
        private void ChangeMoveDir() {
            if (m_player == null)
                m_moveDir = Vector3.down;
            else
                m_moveDir = m_player.transform.position - transform.position;
            lastTurnTime = 0;
        }

        private void Hit() {
            m_animator.SetTrigger("t_Hit");
        }

        public override void Die() {
            base.Die();
            GameManager.instance.AddScore(killScore);
        }

        protected override void OnTriggerBoundary() {
            Destroy(gameObject);
        }
    }
}
