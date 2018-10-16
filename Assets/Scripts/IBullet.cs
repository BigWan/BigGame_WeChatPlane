using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigPlane {

    /// <summary>
    /// 子弹类型ID
    /// </summary>
    public enum BulletType : byte {
        N = 0,
        S = 1,
        L = 2
    }



    /// <summary>
    /// 子弹接口
    /// </summary>
    public interface IBullet {

        /// <summary>
        /// 子弹类型
        /// </summary>
        BulletType GetBulletType();

        /// <summary>
        /// Update移动
        /// </summary>
        void Update();

        /// <summary>
        /// 射中目标后
        /// </summary>
        void OnShoot();

    }
}
