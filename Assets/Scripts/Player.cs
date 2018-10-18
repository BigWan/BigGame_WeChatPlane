 using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace BigPlane {

    /// <summary>
    /// 玩家控制的飞机
    /// </summary>

    public class Player : Plane {

        [Header("Config")]

        public Vector2 moveSize; // TODO:需要和摄像机设定这个值

        protected override void Update() {
            base.Update();
            if (m_isDead) return;
            if (GameManager.instance.IsPlaying())
                SnapMove();
        }


        /// <summary>
        /// 设置射击速度
        /// </summary>
        public void SetShootSpeed() {
            m_shootInterval = float.Parse(GameSetting.GetSetting("ShootSpeed"));
        }

        /// <summary>
        /// 操作移动
        /// </summary>
        private void Move() {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            Vector3 moveDir = new Vector3(x, y, 0);
            transform.Translate(moveDir * m_moveSpeed * Time.deltaTime);

            transform.localPosition = new Vector3(
                Mathf.Clamp( transform.localPosition.x, -moveSize.x, moveSize.x),
                Mathf.Clamp(transform.localPosition.y, -moveSize.y, moveSize.y),
                0
                );
        }


        void SnapMove() {

            if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject()) {
                Vector3 moveDir = (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position));
                Vector2 moveDir2 = new Vector2(moveDir.x, moveDir.y);
                if (moveDir2.magnitude < 50f) return;
                transform.Translate(moveDir2.normalized * m_moveSpeed *2f * Time.deltaTime);

                transform.localPosition = new Vector3(
                    Mathf.Clamp(transform.localPosition.x, -moveSize.x, moveSize.x),
                    Mathf.Clamp(transform.localPosition.y, -moveSize.y, moveSize.y),
                    0
                    );
            }

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

        ///// <summary>
        ///// 子弹射中敌机事件
        ///// </summary>
        ///// <param name="enermy"></param>
        //protected override void OnBulletHit(Enermy enermy) {
            
        //}

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
