using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace BigPlane {

    public class UIPanel : MonoBehaviour {

        protected RectTransform rectTrans;

        public UnityEvent onShow;
        public UnityEvent onHide;
        public UnityEvent onClose;
        public UnityEvent onChange;

        protected virtual void Awake() {
            rectTrans = GetComponent<RectTransform>(); // 缓存效率高
        }


        public virtual void Init() { }
        public virtual void Show() { this.gameObject.SetActive(true); onShow?.Invoke(); }
        public virtual void Hide() { this.gameObject.SetActive(false);onHide?.Invoke(); }
        public virtual void Refresh() { } // 显示数据
        public virtual void Change() { onChange?.Invoke(); }  // 回传数据
        public virtual void Close() { Destroy(this.gameObject);onClose?.Invoke(); }
    }
}
