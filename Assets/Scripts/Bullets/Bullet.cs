using System;
using UnityEngine;


namespace BigPlane {



    /// <summary>
    /// N 弹
    /// </summary>
    public class Bullet : MonoBehaviour {

        /// <summary>
        /// 子弹类型
        /// </summary>
        [SerializeField] private BulletType bulletType = BulletType.N;


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




    }
}