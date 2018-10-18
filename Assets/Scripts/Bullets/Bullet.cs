using System;
using UnityEngine;
using UnityEngine.Events;

namespace BigPlane {

    /// <summary>
    /// 子弹类型ID
    /// </summary>
    public enum BulletType : byte {
        N = 0,
        S = 1,
        L = 2
    }

    public enum RelationType : byte {
        Player = 0,
        Enermy = 1,
        Other = 2
    }


    public class BulletHitEvent : UnityEvent<Enermy> {    }

    /// <summary>
    /// N 弹
    /// </summary>
    public class Bullet : MonoBehaviour {

        /// <summary>
        /// 子弹类型
        /// </summary>
        [SerializeField] private BulletType bulletType = BulletType.N;

        public BulletHitEvent onHit = new BulletHitEvent();

        /// <summary>
        /// 子弹速度
        /// </summary>
        public float moveSpeed;
        /// <summary>
        /// 移动朝向
        /// </summary>
        private Vector3 moveDir;

        public RelationType relationType { get; set; }
        /// <summary>
        /// 子弹的威力
        /// </summary>
        public int power;

        public void SetMoveDir(Vector3 moveDir) {
            this.moveDir = moveDir;
        }

        

        public BulletType GetBulletType() {
            return bulletType;
        }

        public void Update() {
            transform.Translate(moveDir * moveSpeed * Time.deltaTime);
        }

        private void OnTriggerExit2D(Collider2D collision) {
            if (collision.CompareTag("Boundary")) {
                Destroy(this.gameObject);
            }
            if (collision.CompareTag("Enermy")) {
                if (this.relationType == RelationType.Player) {
                    Destroy(this.gameObject);
                    onHit?.Invoke(collision.GetComponent<Enermy>());
                }
            }
        }


        private void OnDestroy() {
            onHit.RemoveAllListeners();
        }


    }
}