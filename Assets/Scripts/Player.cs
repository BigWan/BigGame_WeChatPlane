using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigPlane {

    /// <summary>
    /// 玩家控制的飞机
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Collider2D))]
    public class Player : MonoBehaviour {

        [Header("Config")]

        public Transform emitter;

        [SerializeField]  Bullet bullet;

        /// <summary>
        /// 移动速度
        /// </summary>
        public float moveSpeed;
        /// <summary>
        /// 射击间隔
        /// </summary>
        [SerializeField] float shootInterval = 0.05f;


        [Header("Info Show")]

        public Background background;
        [SerializeField]  Bomb Bomb;


        [Header("Components")]

        private Collider2D m_collider;
        private Animator m_animator;

        private bool isDead = false;

        /// <summary>
        /// 设置子弹
        /// </summary>
        public void SetBullet(Bullet bullet) {
            this.bullet = bullet;
        }


        private void Awake() {
            m_collider = GetComponent<PolygonCollider2D>();
            m_animator = GetComponent<Animator>();
        }


        private void Shoot() {
            SpawnBullet();
        }



        void SpawnBullet() {
            Bullet sb = Instantiate<Bullet>(bullet);
            sb.transform.localPosition = emitter.position;
            
            sb.SetMoveDir(Vector3.up);
        }



        /// <summary>
        /// 碰撞处理
        /// 碰到
        /// 子弹
        /// 道具
        /// 敌机
        /// </summary>
        /// <param name="collision"></param>
        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.CompareTag("Enermy")) 
                OnTriggerEnermy(collision.GetComponent<Enermy>());
            
            if(collision.CompareTag("Bullet"))
                OnTriggerBullet(collision.GetComponent<Bullet>());

            if (collision.CompareTag("Item"))
                OnTriggerItem(collision.GetComponent<Item>());

        }


        void OnTriggerEnermy(Enermy enermy) {

        }

        void OnTriggerBullet(Bullet bullet) {
            isDead = true;
            m_animator.SetBool("b_Dead", true);
        }

        void OnTriggerItem(Item item) {

        }

        private float shootTime;
        private void Update() {
            if (isDead) return;

            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            Vector3 speed = new Vector3(x, y, 0);

            Vector3 MoveTarget = transform.localPosition + speed * Time.deltaTime * moveSpeed;

            transform.localPosition = MoveTarget;
            shootTime += Time.deltaTime;
            if (shootTime > shootInterval) {
                shootTime -= shootInterval;

                Shoot();
            }

        }


    }
}
