using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigPlane {

    public class Background : MonoBehaviour {

        [Header("Config")]
        public float tileHeight = 8.52f;

        /// <summary>
        /// 场景的可视范围,以高为限定,宽随屏幕比例调整
        /// </summary>
        public float bgHeight = 8.5f;

        public GameObject tileA;
        public GameObject tileB;


        [Header("Info Show")]
        [SerializeField]
        private float moveSpeed = 5f;


        private void Update() {
            BGMove();
        }

        private void Awake() {
            SetTilePosition();
        }


        void SetTilePosition() {
            tileA.transform.localPosition = Vector3.zero;
            tileB.transform.localPosition = Vector3.up * tileHeight;

        }

        void BGMove() {

            Vector3 offset = Vector3.down * moveSpeed * Time.deltaTime;

            tileA.transform.Translate(offset, Space.World);
            tileB.transform.Translate(offset, Space.World);
            CheckSwitchTile(tileA);
            CheckSwitchTile(tileB);

        }


        void CheckSwitchTile(GameObject tile) {
            if (tile.transform.localPosition.y <= - bgHeight)
                tile.transform.Translate(Vector3.up * tileHeight * 2f);
        }




#if UNITY_EDITOR
        private void OnDrawGizmos() {

            float width = bgHeight/16f*9f;

            Vector2 size = new Vector2(width*0.5f, bgHeight*0.5f);
            Vector3 p00 = new Vector3( - size.x, - size.y);
            Vector3 p10 = new Vector3( + size.x, - size.y);
            Vector3 p01 = new Vector3( - size.x,  + size.y);
            Vector3 p11 = new Vector3( + size.x,  + size.y);



            Gizmos.color = Color.green;


            Gizmos.DrawLine(p00, p10);
            Gizmos.DrawLine(p10, p11);
            Gizmos.DrawLine(p11, p01);
            Gizmos.DrawLine(p01, p00);

        }
#endif

    }
}