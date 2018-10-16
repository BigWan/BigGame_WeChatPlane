using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigPlane {

    public class Background : MonoBehaviour {

        [Header("Config")]
        public float bgHeight = 8.52f;

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
            tileB.transform.localPosition = Vector3.up * bgHeight;

        }

        void BGMove() {

            Vector3 offset = Vector3.down * moveSpeed * Time.deltaTime;

            tileA.transform.Translate(offset, Space.World);
            tileB.transform.Translate(offset, Space.World);
            CheckSwitchTile(tileA);
            CheckSwitchTile(tileB);

        }


        void CheckSwitchTile(GameObject tile) {
            if (tile.transform.localPosition.y <= -bgHeight)
                tile.transform.Translate(Vector3.up * bgHeight * 2f);
        }

        /// <summary>
        /// 消除子弹
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerExit2D (Collider2D other) {
            if (other.CompareTag("Bullet")) {
                Destroy(other.gameObject);
            }
        }
    }
}