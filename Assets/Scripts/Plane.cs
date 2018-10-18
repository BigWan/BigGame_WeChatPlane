using UnityEngine;
using System.Collections;
using System;

namespace BigPlane {
    /// <summary>
    /// 飞机接口
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AudioSource))]
    public class Plane : MonoBehaviour {

        [Header("Setting")]
        [SerializeField]protected float m_moveSpeed;

        [SerializeField] protected Transform m_emitter;

        [SerializeField] protected RelationType m_relationType;

        [SerializeField] protected float m_shootInterval = 0.05f;
        public bool autoShoot = true;

        [SerializeField]protected Bullet m_bullet;

        private float m_lastShootTime;
        protected bool m_isDead;

        // components 
        protected Animator m_animator;
        protected Collider2D m_collider2D;
        protected Rigidbody2D m_rigidbody2D;
        protected AudioSource m_audioSource;

        [Header("音效")]
        [SerializeField] protected AudioClip shootSound;
        [SerializeField] protected AudioClip dieSound;


        public bool CompareRelation( RelationType target) {
            return m_relationType == target; 
        }

        protected virtual void Awake() {
            m_animator = GetComponent<Animator>();
            m_collider2D = GetComponent<Collider2D>();
            m_rigidbody2D = GetComponent<Rigidbody2D>();
            m_audioSource = GetComponent<AudioSource>();
            m_collider2D.enabled = true;
            m_lastShootTime = m_shootInterval * 0.5f;
        }

        public virtual void Die() {
            m_isDead = true;
            m_animator.SetBool("b_Dead", true);
            m_rigidbody2D.velocity = Vector3.zero;
            m_collider2D.enabled = false;
            m_audioSource.clip = dieSound;
            m_audioSource.Play();
            StartCoroutine(DestroyAfterTime());
        }


        IEnumerator DestroyAfterTime() {
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }

        protected virtual bool CanShoot() {
            return !m_isDead && m_lastShootTime > m_shootInterval ;
        }


        protected virtual void SetBullet(Bullet bullet) {
            m_bullet = bullet;            
        }

        protected virtual void Shoot() {

            if (!CanShoot()) return;

            Bullet sb = Instantiate<Bullet>(m_bullet);
            if (sb == null) throw new UnityException($"{name}Shoot 失败");
            sb.relationType = m_relationType;
            sb.transform.localPosition = m_emitter.position;
            sb.onHit.AddListener(OnBulletHit);
            sb.SetMoveDir(m_emitter.up);
            m_lastShootTime = 0;
            m_audioSource.clip = shootSound;
            m_audioSource.Play();
        }


        public void SetMoveSpeed(float value) {
            m_moveSpeed = value;
        }

        protected virtual void OnBulletHit(Enermy enermy) {}


        protected virtual void Update() {
            m_lastShootTime += Time.deltaTime;
            if (autoShoot)
                Shoot();
        }

        /// <summary>
        /// 碰撞处理
        /// 1.子弹
        /// 2.道具
        /// 3.敌机
        /// </summary>
        /// <param name="collision"></param>
        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.CompareTag("Enermy"))
                OnTriggerPlane(collision.GetComponent<Plane>());

            if (collision.CompareTag("Bullet"))
                OnTriggerBullet(collision.GetComponent<Bullet>());

            if (collision.CompareTag("Item"))
                OnTriggerItem(collision.GetComponent<Item>());

        }


        private void OnTriggerExit2D(Collider2D collision) {
            if (collision.CompareTag("Boundary"))
                OnTriggerBoundary();
        }


        protected virtual void OnTriggerBoundary() { }


        /// <summary>
        /// 碰到飞机
        /// </summary>
        /// <param name="enermy"></param>
        protected virtual void OnTriggerPlane(Plane enermy) {}

        /// <summary>
        /// 碰到子弹
        /// </summary>
        /// <param name="bullet"></param>
        protected virtual void OnTriggerBullet(Bullet bullet) {}

        /// <summary>
        /// 碰到道具
        /// </summary>
        /// <param name="item"></param>
        protected virtual void OnTriggerItem(Item item) {}




    }
}